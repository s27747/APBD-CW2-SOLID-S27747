using System;

namespace APBD_CW2_SOLID_S27747.Models.Users;

public class Student : User
{
    public string StudentNumber { get; }
    public string Faculty { get; }

    public Student(string firstName, string lastName, string studentNumber, string faculty)
        : base(firstName, lastName, UserType.Student)
    {
        if (string.IsNullOrWhiteSpace(studentNumber))
            throw new ArgumentException("Student number cannot be empty.");

        if (string.IsNullOrWhiteSpace(faculty))
            throw new ArgumentException("Faculty cannot be empty.");

        StudentNumber = studentNumber;
        Faculty = faculty;
    }

    public override string GetSpecificInfo()
    {
        return $"Student number: {StudentNumber}, Faculty: {Faculty}";
    }
}