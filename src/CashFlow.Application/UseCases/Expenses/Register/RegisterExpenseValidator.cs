﻿using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidator: AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title)
            .NotEmpty().WithMessage("The title is required");
        RuleFor(expense => expense.Amount)
            .GreaterThan(0).WithMessage("The amount must be greater than 0");
        RuleFor(expense => expense.Date)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The date can not be greater than the current date");
        RuleFor(expense => expense.PaymentType)
            .IsInEnum().WithMessage("The payment type is invalid");
    }
}