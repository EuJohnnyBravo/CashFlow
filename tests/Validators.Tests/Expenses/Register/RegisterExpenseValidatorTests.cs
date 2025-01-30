﻿using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("            ")]
    [InlineData(null)]
    public void ErrorTitleEmpty(string? title)
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage
                .Equals(ResourceErrorMessages.TITLE_IS_REQUIRED));
    }
    
    [Fact]
    public void ErrorDateFuture()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage
                .Equals(ResourceErrorMessages.EXPENSE_CANNOT_BE_IN_THE_FUTURE));
    }
    
    [Fact]
    public void ErrorPaymentTypeInvalid()
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage
                .Equals(ResourceErrorMessages.INVALID_PAYMENT_TYPE));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void ErrorAmountInvalid(decimal amount)
    {
        //Arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(error => error.ErrorMessage
                .Equals(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}