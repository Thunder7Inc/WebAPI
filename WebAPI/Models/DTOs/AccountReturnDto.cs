namespace WebAPI.Models.DTOs
{
    public class AccountReturnDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PIN {get; set;}
        
        public double Balance { get; set; } = 0.0;
    }
}
