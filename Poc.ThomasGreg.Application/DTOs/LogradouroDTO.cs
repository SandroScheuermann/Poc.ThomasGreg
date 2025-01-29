namespace Poc.ThomasGreg.Application.DTOs
{
    public class LogradouroDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClienteId { get; set; }
        public required string Endereco { get; set; }  
    }
}
