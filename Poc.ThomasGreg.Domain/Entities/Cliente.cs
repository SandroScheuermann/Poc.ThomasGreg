namespace Poc.ThomasGreg.Domain.Entities
{
    public class Cliente
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required byte[] Logotipo { get; set; } 
        public List<Logradouro> Logradouros { get; set; } = [];
    }
}
