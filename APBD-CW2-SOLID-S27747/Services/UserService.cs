using APBD_CW2_SOLID_S27747.Models.Users;
using APBD_CW2_SOLID_S27747.Repositories.Interfaces;

namespace APBD_CW2_SOLID_S27747.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User AddUser(User user)
    {
        return _userRepository.Add(user);
    }

    public IReadOnlyList<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    public User? GetUserById(int id)
    {
        return _userRepository.GetById(id);
    }
}