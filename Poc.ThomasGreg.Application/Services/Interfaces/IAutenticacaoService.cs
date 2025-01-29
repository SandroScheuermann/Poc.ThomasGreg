using Poc.ThomasGreg.Application.DTOs;

namespace Poc.ThomasGreg.Application.Services.Interfaces
{
    public interface IAutenticacaoService
    { 
        public Task RegistrarUsuarioAsync(UsuarioDTO usuarioDTO);

    }
}
