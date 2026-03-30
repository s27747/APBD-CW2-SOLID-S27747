using System.Collections.Generic;
using System.Linq;
using APBD_CW2_SOLID_S27747.Models.Users;
using APBD_CW2_SOLID_S27747.Repositories.IdGenerators;
using APBD_CW2_SOLID_S27747.Repositories.Interfaces;

namespace APBD_CW2_SOLID_S27747.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = [];
    private readonly UserIdGenerator _idGenerator;

    public InMemoryUserRepository(UserIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public User Add(User user)
    {
        user.SetId(_idGenerator.NextId());
        _users.Add(user);
        return user;
    }

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public IReadOnlyList<User> GetAll()
    {
        return _users.AsReadOnly();
    }
}