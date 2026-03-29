using APBD_CW2_SOLID_S27747.Models.Equipments;
using APBD_CW2_SOLID_S27747.Models.Loans;
using APBD_CW2_SOLID_S27747.Repositories.Interfaces;
using APBD_CW2_SOLID_S27747.Services.Policies;
using APBD_CW2_SOLID_S27747.Services.Results;

namespace APBD_CW2_SOLID_S27747.Services;

public class RentalService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IUserLoanLimitPolicy _userLoanLimitPolicy;
    private readonly ILateReturnPenaltyPolicy _lateReturnPenaltyPolicy;

    public RentalService(
        ILoanRepository loanRepository,
        IUserRepository userRepository,
        IEquipmentRepository equipmentRepository,
        IUserLoanLimitPolicy userLoanLimitPolicy,
        ILateReturnPenaltyPolicy lateReturnPenaltyPolicy)
    {
        _loanRepository = loanRepository;
        _userRepository = userRepository;
        _equipmentRepository = equipmentRepository;
        _userLoanLimitPolicy = userLoanLimitPolicy;
        _lateReturnPenaltyPolicy = lateReturnPenaltyPolicy;
    }

    public OperationResult<Loan> BorrowEquipment(int userId, int equipmentId, DateTime borrowedAt, int days)
    {
        if (days <= 0)
            return OperationResult<Loan>.Fail("Loan period must be greater than zero.");

        var user = _userRepository.GetById(userId);
        if (user is null)
            return OperationResult<Loan>.Fail("User not found.");

        var equipment = _equipmentRepository.GetById(equipmentId);
        if (equipment is null)
            return OperationResult<Loan>.Fail("Equipment not found.");

        if (equipment.Status != EquipmentStatus.Available)
            return OperationResult<Loan>.Fail("Equipment is not available.");

        var activeLoansCount = _loanRepository.GetActiveByUserId(userId).Count;
        var userLimit = _userLoanLimitPolicy.GetLimit(user);

        if (activeLoansCount >= userLimit)
            return OperationResult<Loan>.Fail($"User has reached the active loan limit: {userLimit}.");

        var loan = new Loan(user, equipment, borrowedAt, days);
        _loanRepository.Add(loan);
        equipment.MarkAsBorrowed();

        return OperationResult<Loan>.Ok(
            $"Equipment {equipment.Name} was borrowed by {user.FullName}.",
            loan);
    }

    public IReadOnlyList<Loan> GetActiveLoansForUser(int userId)
    {
        return _loanRepository.GetActiveByUserId(userId);
    }
    
    public OperationResult<Loan> ReturnEquipment(int loanId, DateTime returnDate)
    {
        var loan = _loanRepository.GetById(loanId);

        if (loan is null)
            return OperationResult<Loan>.Fail("Loan not found.");

        if (!loan.IsActive)
            return OperationResult<Loan>.Fail("This loan is already closed.");

        if (returnDate.Date < loan.BorrowedAt.Date)
            return OperationResult<Loan>.Fail("Return date cannot be earlier than borrow date.");

        var penalty = _lateReturnPenaltyPolicy.CalculatePenalty(loan.DueDate, returnDate);

        loan.CompleteReturn(returnDate, penalty);
        loan.Equipment.MarkAsAvailable();

        return OperationResult<Loan>.Ok(
            $"Equipment {loan.Equipment.Name} was returned. Penalty: {penalty:C}.",
            loan);
    }

    public IReadOnlyList<Loan> GetOverdueLoans(DateTime currentDate)
    {
        return _loanRepository
            .GetActive()
            .Where(l => l.IsOverdue(currentDate))
            .OrderBy(l => l.DueDate)
            .ToList()
            .AsReadOnly();
    }
    
}