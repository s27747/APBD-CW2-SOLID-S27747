using System.Collections.Generic;
using APBD_CW2_SOLID_S27747.Models.Users;

namespace APBD_CW2_SOLID_S27747.Repositories.Interfaces;

public interface IUserRepository
{
    User Add(User user);
    User? GetById(int id);
    IReadOnlyList<User> GetAll();
}