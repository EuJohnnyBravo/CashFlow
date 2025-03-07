﻿using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorMessages) : CashFlowException(string.Empty)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public override List<string> GetErrors()
    {
        return errorMessages;
    }
}