using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext): IExpensesRepository
{
    public async Task Add(Expense expense)
    {
        await dbContext.Expenses.AddAsync(expense);
    }

    public async Task<List<Expense>> GetAll()
    {
        return await dbContext.Expenses.ToListAsync();
    }
    
    public async Task<Expense?> GetById(long id)
    {
        return await dbContext.Expenses.AsNoTracking()
            .FirstOrDefaultAsync(expense => expense.Id == id);
    }
}