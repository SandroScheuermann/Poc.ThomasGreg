using System.ComponentModel.DataAnnotations.Schema;

namespace Poc.ThomasGreg.MVC.DTOs
{
    public class ClienteDTO
    {
        public required Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required byte[] Logotipo { get; set; }

        [NotMapped]
        public IFormFile? LogotipoFile { get; set; }
    }
}
