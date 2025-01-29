namespace Poc.ThomasGreg.Application.DTOs
{
    public class CadastrarClienteDTO
    { 
        public required string? Nome { get; set; }
        public required string? Email { get; set; }
        public required byte[]? Logotipo { get; set; } 
    }
}
