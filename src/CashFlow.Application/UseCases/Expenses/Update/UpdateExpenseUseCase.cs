using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IExpensesUptateOnlyRepository repository
    ): IUpdateExpenseUseCase
{
    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);
        var expense = await repository.GetById(id);
        if(expense is null) 
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        
        mapper.Map(request, expense);
        
        repository.Update(expense);
        await unitOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessages);
    }
    
}