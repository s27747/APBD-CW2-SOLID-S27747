using System.Collections.Generic;
using System.Linq;
using APBD_CW2_SOLID_S27747.Models.Loans;
using APBD_CW2_SOLID_S27747.Repositories.IdGenerators;
using APBD_CW2_SOLID_S27747.Repositories.Interfaces;

namespace APBD_CW2_SOLID_S27747.Repositories;

public class InMemoryLoanRepository : ILoanRepository
{
    private readonly List<Loan> _loans = [];
    private readonly LoanIdGenerator _idGenerator;

    public InMemoryLoanRepository(LoanIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public Loan Add(Loan loan)
    {
        loan.SetId(_idGenerator.NextId());
        _loans.Add(loan);
        return loan;
    }

    public Loan? GetById(int id)
    {
        return _loans.FirstOrDefault(l => l.Id == id);
    }

    public IReadOnlyList<Loan> GetAll()
    {
        return _loans.AsReadOnly();
    }

    public IReadOnlyList<Loan> GetActive()
    {
        return _loans
            .Where(l => l.IsActive)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<Loan> GetActiveByUserId(int userId)
    {
        return _loans
            .Where(l => l.IsActive && l.User.Id == userId)
            .ToList()
            .AsReadOnly();
    }
}