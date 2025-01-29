namespace Poc.ThomasGreg.Application.DTOs
{
    public class ClienteDTO
    {
        public Guid Id { get; set; }
        public required string? Nome { get; set; }
        public required string? Email { get; set; }
        public required byte[]? Logotipo { get; set; }
        public List<string> Logradouros { get; set; } = [];
    }
}
