using APBD_CW2_SOLID_S27747.Models.Equipments;
using APBD_CW2_SOLID_S27747.Repositories.Interfaces;

namespace APBD_CW2_SOLID_S27747.Services;

public class ReportService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public ReportService(
        IEquipmentRepository equipmentRepository,
        IUserRepository userRepository,
        ILoanRepository loanRepository)
    {
        _equipmentRepository = equipmentRepository;
        _userRepository = userRepository;
        _loanRepository = loanRepository;
    }

    public string GenerateSummaryReport(DateTime currentDate)
    {
        var allEquipment = _equipmentRepository.GetAll();
        var allLoans = _loanRepository.GetAll();
        var activeLoans = _loanRepository.GetActive();
        var overdueLoans = activeLoans.Where(l => l.IsOverdue(currentDate)).ToList();
        var returnedLoans = allLoans.Where(l => !l.IsActive).ToList();
        var totalPenalties = returnedLoans.Sum(l => l.PenaltyAmount);

        var lines = new List<string>
        {
            "=== Rental Summary Report ===",
            $"Generated at: {currentDate:yyyy-MM-dd}",
            $"Users count: {_userRepository.GetAll().Count}",
            $"Equipment count: {allEquipment.Count}",
            $"Available equipment: {allEquipment.Count(e => e.Status == EquipmentStatus.Available)}",
            $"Borrowed equipment: {allEquipment.Count(e => e.Status == EquipmentStatus.Borrowed)}",
            $"Unavailable equipment: {allEquipment.Count(e => e.Status == EquipmentStatus.Unavailable)}",
            $"Total loans: {allLoans.Count}",
            $"Active loans: {activeLoans.Count}",
            $"Overdue loans: {overdueLoans.Count}",
            $"Closed loans: {returnedLoans.Count}",
            $"Total collected penalties: {totalPenalties:C}"
        };

        return string.Join(Environment.NewLine, lines);
    }
}