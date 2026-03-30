namespace APBD_CW2_SOLID_S27747.Services.Policies;

public interface ILateReturnPenaltyPolicy
{
    decimal CalculatePenalty(DateTime dueDate, DateTime returnDate);
}