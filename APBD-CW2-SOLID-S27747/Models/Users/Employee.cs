using System;

namespace APBD_CW2_SOLID_S27747.Models.Users;

public class Employee : User
{
    public string EmployeeNumber { get; }
    public string Department { get; }

    public Employee(string firstName, string lastName, string employeeNumber, string department)
        : base(firstName, lastName, UserType.Employee)
    {
        if (string.IsNullOrWhiteSpace(employeeNumber))
            throw new ArgumentException("Employee number cannot be empty.");

        if (string.IsNullOrWhiteSpace(department))
            throw new ArgumentException("Department cannot be empty.");

        EmployeeNumber = employeeNumber;
        Department = department;
    }

    public override string GetSpecificInfo()
    {
        return $"Employee number: {EmployeeNumber}, Department: {Department}";
    }
}