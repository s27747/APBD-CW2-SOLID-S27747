using System;

namespace APBD_CW2_SOLID_S27747.Services.Policies;

public class DailyPenaltyPolicy : ILateReturnPenaltyPolicy
{
    private readonly decimal _penaltyPerDay;

    public DailyPenaltyPolicy(decimal penaltyPerDay)
    {
        if (penaltyPerDay < 0)
            throw new ArgumentException("Penalty per day cannot be negative.");

        _penaltyPerDay = penaltyPerDay;
    }

    public decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        var lateDays = (returnDate.Date - dueDate.Date).Days;

        if (lateDays <= 0)
            return 0;

        return lateDays * _penaltyPerDay;
    }
}