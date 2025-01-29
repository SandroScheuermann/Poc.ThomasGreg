namespace Poc.ThomasGreg.Application.DTOs
{
    public class AtualizarLogradouroDTO
    { 
        public Guid Id { get; set; }
        public Guid? ClienteId { get; set; }
        public string? Endereco { get; set; }  
    }
}
