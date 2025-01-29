namespace Poc.ThomasGreg.Application.DTOs
{
    public class AtualizarClienteDTO
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public byte[]? Logotipo { get; set; } 
    }
}
