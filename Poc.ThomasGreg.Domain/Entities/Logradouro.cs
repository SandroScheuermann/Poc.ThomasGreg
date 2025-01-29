namespace Poc.ThomasGreg.Domain.Entities
{
    public class Logradouro
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClienteId { get; set; }
        public required string Endereco { get; set; }  
    }
}
