using APBD_CW2_SOLID_S27747.Models.Users;

namespace APBD_CW2_SOLID_S27747.Services.Policies;

public class DefaultUserLoanLimitPolicy : IUserLoanLimitPolicy
{
    public int GetLimit(User user)
    {
        return user switch
        {
            Student => 2,
            Employee => 5,
            _ => 0
        };
    }
}