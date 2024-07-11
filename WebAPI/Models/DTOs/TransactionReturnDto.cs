using WebAPI.Models.Enums;

namespace WebAPI.Models.DTOs;

public class TransactionReturnDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public double Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
}