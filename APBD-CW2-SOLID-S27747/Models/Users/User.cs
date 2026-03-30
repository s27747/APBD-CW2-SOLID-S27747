using System;

namespace APBD_CW2_SOLID_S27747.Models.Users;

public abstract class User
{
    public int Id { get; private set; }
    public string FirstName { get; }
    public string LastName { get; }
    public UserType Type { get; }

    protected User(string firstName, string lastName, UserType type)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.");

        FirstName = firstName;
        LastName = lastName;
        Type = type;
    }

    public string FullName => $"{FirstName} {LastName}";

    internal void SetId(int id)
    {
        Id = id;
    }

    public abstract string GetSpecificInfo();
}