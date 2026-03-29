using APBD_CW2_SOLID_S27747.Models.Users;
using APBD_CW2_SOLID_S27747.Models.Equipments;

namespace APBD_CW2_SOLID_S27747.Models.Loans;

public class Loan
{
    public int Id { get; private set; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime BorrowedAt { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnedAt { get; private set; }
    public decimal PenaltyAmount { get; private set; }
    public bool IsActive => !ReturnedAt.HasValue;

    public Loan(User user, Equipment equipment, DateTime borrowedAt, int days)
    {
        if (days <= 0)
            throw new ArgumentException("Loan period must be greater than zero.");

        User = user ?? throw new ArgumentNullException(nameof(user));
        Equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
        BorrowedAt = borrowedAt;
        DueDate = borrowedAt.Date.AddDays(days);
    }

    internal void SetId(int id)
    {
        Id = id;
    }

    public void CompleteReturn(DateTime returnDate, decimal penaltyAmount)
    {
        ReturnedAt = returnDate;
        PenaltyAmount = penaltyAmount;
    }

    public bool IsOverdue(DateTime currentDate)
    {
        return IsActive && currentDate.Date > DueDate.Date;
    }

    public bool WasReturnedOnTime()
    {
        return ReturnedAt.HasValue && ReturnedAt.Value.Date <= DueDate.Date;
    }
}