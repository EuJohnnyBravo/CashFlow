using CashFlow.Domain.Entities.Enums;

namespace CashFlow.Domain.Entities;

public class Expense
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}