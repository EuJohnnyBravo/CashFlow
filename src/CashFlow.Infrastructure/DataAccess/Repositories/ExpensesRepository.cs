using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : 
    IExpensesWriteOnlyRepository, 
    IExpensesReadOnlyRepository,
    IExpensesUptateOnlyRepository
{
    public async Task Add(Expense expense)
    {
        await dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
        if (result is null)
        {
            return false;
        }
        dbContext.Expenses.Remove(result);

        return true;
    }

    public async Task<List<Expense>> GetAll()
    {
        return await dbContext.Expenses.ToListAsync();
    }
    
    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
        return await dbContext.Expenses.AsNoTracking()
            .FirstOrDefaultAsync(expense => expense.Id == id);
    }
    
    async Task<Expense?> IExpensesUptateOnlyRepository.GetById(long id)
    {
        return await dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
    }

    public void Update(Expense expense)
    {
        dbContext.Expenses.Update(expense);
    }
}