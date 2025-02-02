using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesUptateOnlyRepository
{
    Task<Expense?> GetById(long id);
    void Update(Expense expense);
}