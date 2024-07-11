using WebAPI.Models.Enums;

namespace WebAPI.Models.DTOs
{
    public class TransationDTO
    {
        public int AccountId { get; set; }
        public TransactionType Type { get; set; } 
        public double Amount { get; set; }
    }
}