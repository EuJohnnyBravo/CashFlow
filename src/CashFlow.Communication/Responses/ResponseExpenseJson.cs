using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Responses;

public class ResponseExpenseJson(string title)
{
    public long Id { get; init; }
    public string Title { get; init; } = title;
    public string? Description { get; init; }
    public DateTime Date { get; init; }
    public decimal Amount { get; init; }
    public PaymentType PaymentType { get; init; }
}