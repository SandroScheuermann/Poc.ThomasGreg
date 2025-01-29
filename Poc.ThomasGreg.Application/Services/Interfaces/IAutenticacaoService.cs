using Poc.ThomasGreg.Application.DTOs;

namespace Poc.ThomasGreg.Application.Services.Interfaces
{
    public interface IAutenticacaoService
    { 
        public Task RegistrarUsuarioAsync(RegistrarUsuarioDTO usuarioDTO);
        public Task<string> AutenticarUsuarioAsync(LoginDTO loginDTO);

    }
}
