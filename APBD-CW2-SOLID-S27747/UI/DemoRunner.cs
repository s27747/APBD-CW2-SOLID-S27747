using System;
using System.Collections.Generic;
using APBD_CW2_SOLID_S27747.Models.Equipments;
using APBD_CW2_SOLID_S27747.Models.Loans;
using APBD_CW2_SOLID_S27747.Models.Users;
using APBD_CW2_SOLID_S27747.Repositories;
using APBD_CW2_SOLID_S27747.Repositories.IdGenerators;
using APBD_CW2_SOLID_S27747.Services;
using APBD_CW2_SOLID_S27747.Services.Policies;
using APBD_CW2_SOLID_S27747.Services.Results;

namespace APBD_CW2_SOLID_S27747.UI;

public class DemoRunner
{
    public void Run()
    {
        var equipmentRepository = new InMemoryEquipmentRepository(new EquipmentIdGenerator());
        var userRepository = new InMemoryUserRepository(new UserIdGenerator());
        var loanRepository = new InMemoryLoanRepository(new LoanIdGenerator());

        var equipmentService = new EquipmentService(equipmentRepository);
        var userService = new UserService(userRepository);
        var rentalService = new RentalService(
            loanRepository,
            userRepository,
            equipmentRepository,
            new DefaultUserLoanLimitPolicy(),
            new DailyPenaltyPolicy(15m));
        var reportService = new ReportService(equipmentRepository, userRepository, loanRepository);

        var laptop1 = equipmentService.AddEquipment(new Laptop("Dell Latitude 5440", "Intel i5", 16));
        var laptop2 = equipmentService.AddEquipment(new Laptop("Lenovo ThinkPad E14", "AMD Ryzen 7", 32));
        var projector1 = equipmentService.AddEquipment(new Projector("Epson EB-X49", "1024x768", 3600));
        var projector2 = equipmentService.AddEquipment(new Projector("BenQ MX560", "1280x800", 4000));
        var camera1 = equipmentService.AddEquipment(new Camera("Canon EOS R50", "APS-C", 8));

        PrintHeader("All equipment");
        PrintEquipment(equipmentService.GetAllEquipment());

        var student1 = userService.AddUser(new Student("Jan", "Kowalski", "s27747", "Computer Science"));
        var student2 = userService.AddUser(new Student("Anna", "Nowak", "s27748", "Management"));
        var employee1 = userService.AddUser(new Employee("Marek", "Wisniewski", "e1001", "IT Department"));

        PrintHeader("All users");
        PrintUsers(userService.GetAllUsers());

        PrintHeader("Mark one projector as unavailable");
        PrintResult(equipmentService.MarkAsUnavailable(projector1.Id));

        PrintHeader("Available equipment");
        PrintEquipment(equipmentService.GetAvailableEquipment());

        var borrowDate = new DateTime(2026, 4, 1);

        PrintHeader("Correct borrows");
        var loanResult1 = rentalService.BorrowEquipment(student1.Id, laptop1.Id, borrowDate, 7);
        PrintLoanResult(loanResult1);

        var loanResult2 = rentalService.BorrowEquipment(student1.Id, camera1.Id, borrowDate, 5);
        PrintLoanResult(loanResult2);

        var loanResult3 = rentalService.BorrowEquipment(employee1.Id, projector2.Id, borrowDate, 3);
        PrintLoanResult(loanResult3);

        PrintHeader("Invalid operations");
        var invalidUnavailableBorrow = rentalService.BorrowEquipment(employee1.Id, projector1.Id, borrowDate, 2);
        PrintLoanResult(invalidUnavailableBorrow);

        var invalidLimitBorrow = rentalService.BorrowEquipment(student1.Id, laptop2.Id, borrowDate, 2);
        PrintLoanResult(invalidLimitBorrow);

        if (loanResult1.Success && loanResult1.Data is not null)
        {
            PrintHeader("Return on time");
            var returnOnTimeResult = rentalService.ReturnEquipment(loanResult1.Data.Id, new DateTime(2026, 4, 5));
            PrintLoanResult(returnOnTimeResult);
        }

        if (loanResult3.Success && loanResult3.Data is not null)
        {
            PrintHeader("Late return with penalty");
            var lateReturnResult = rentalService.ReturnEquipment(loanResult3.Data.Id, new DateTime(2026, 4, 10));
            PrintLoanResult(lateReturnResult);
        }

        PrintHeader("Active loans for Jan Kowalski");
        PrintLoans(rentalService.GetActiveLoansForUser(student1.Id));

        var reportDate = new DateTime(2026, 4, 10);

        PrintHeader("Overdue loans");
        PrintLoans(rentalService.GetOverdueLoans(reportDate));

        PrintHeader("Final report");
        Console.WriteLine(reportService.GenerateSummaryReport(reportDate));
    }

    private void PrintHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 50));
        Console.WriteLine(title);
        Console.WriteLine(new string('=', 50));
    }

    private void PrintEquipment(IReadOnlyList<Equipment> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            Console.WriteLine($"Id: {equipment.Id}, Name: {equipment.Name}, Status: {equipment.Status}, {equipment.GetSpecificInfo()}");
        }
    }

    private void PrintUsers(IReadOnlyList<User> users)
    {
        foreach (var user in users)
        {
            Console.WriteLine($"Id: {user.Id}, Name: {user.FullName}, Type: {user.Type}, {user.GetSpecificInfo()}");
        }
    }

    private void PrintLoans(IReadOnlyList<Loan> loans)
    {
        if (loans.Count == 0)
        {
            Console.WriteLine("No loans found.");
            return;
        }

        foreach (var loan in loans)
        {
            var returnInfo = loan.ReturnedAt.HasValue
                ? $"Returned: {loan.ReturnedAt:yyyy-MM-dd}, Penalty: {loan.PenaltyAmount:C}"
                : "Still active";

            Console.WriteLine($"LoanId: {loan.Id}, User: {loan.User.FullName}, Equipment: {loan.Equipment.Name}, Borrowed: {loan.BorrowedAt:yyyy-MM-dd}, Due: {loan.DueDate:yyyy-MM-dd}, {returnInfo}");
        }
    }

    private void PrintResult(OperationResult result)
    {
        Console.WriteLine($"{(result.Success ? "SUCCESS" : "ERROR")}: {result.Message}");
    }

    private void PrintLoanResult(OperationResult<Loan> result)
    {
        Console.WriteLine($"{(result.Success ? "SUCCESS" : "ERROR")}: {result.Message}");
    }
}