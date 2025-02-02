﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poc.ThomasGreg.MVC.DTOs
{
    public class ClienteDTO
    {
        public required Guid Id { get; set; }
         
		[StringLength(100, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
		public required string Nome { get; set; }
         
		[EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
		public required string Email { get; set; }

		public byte[]? Logotipo { get; set; }

        [NotMapped] 
        public IFormFile? LogotipoFile { get; set; }
    }
}
