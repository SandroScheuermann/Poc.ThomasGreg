using System.ComponentModel.DataAnnotations;

namespace Poc.ThomasGreg.MVC.DTOs
{
    public class LogradouroDTO
    {
        public required Guid Id { get; set; }
        public required Guid ClienteId { get; set; }

        [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Endereço deve ter no máximo 100 caracteres.")]
        public required string Endereco { get; set; }  
    }
}
