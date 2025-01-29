using System.ComponentModel.DataAnnotations;

namespace Poc.ThomasGreg.MVC.DTOs
{
    public class UsuarioDTO
    { 
         
		[StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
		public required string Nome { get; set; }
         
		[EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
		public required string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public required string Senha { get; set; } 
    }
}
