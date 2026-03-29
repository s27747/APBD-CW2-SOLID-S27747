using APBD_CW2_SOLID_S27747.Models.Users;

namespace APBD_CW2_SOLID_S27747.Services.Policies;

public interface IUserLoanLimitPolicy
{
    int GetLimit(User user);
}