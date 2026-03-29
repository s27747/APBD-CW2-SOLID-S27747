using APBD_CW2_SOLID_S27747.Models.Loans;

namespace APBD_CW2_SOLID_S27747.Repositories.Interfaces;

public interface ILoanRepository
{
    Loan Add(Loan loan);
    Loan? GetById(int id);
    IReadOnlyList<Loan> GetAll();
    IReadOnlyList<Loan> GetActive();
    IReadOnlyList<Loan> GetActiveByUserId(int userId);
}