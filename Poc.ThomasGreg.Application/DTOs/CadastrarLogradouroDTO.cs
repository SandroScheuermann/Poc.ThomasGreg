namespace Poc.ThomasGreg.Application.DTOs
{
    public class CadastrarLogradouroDTO
    {  
        public Guid ClienteId { get; set; }
        public required string Endereco { get; set; }  
    }
}
