using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Application.Services.Interfaces;
using Poc.ThomasGreg.Domain.Entities;
using Poc.ThomasGreg.Domain.Interfaces;

namespace Poc.ThomasGreg.Application.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticacaoService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task RegistrarUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha);

            Usuario usuario = new()
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                SenhaHash = senhaHash 
            };

            await _usuarioRepository.InserirUsuarioAsync(usuario); 
        } 

        public async Task<string> AutenticarUsuarioAsync(LoginDTO loginDTO)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorEmailAsync(loginDTO.Email);

            if(usuario is null)
            {
                return string.Empty;
            }

            var senhaValida = BCrypt.Net.BCrypt.Verify(loginDTO.Senha, usuario.SenhaHash);

            if(senhaValida is false)
            {
                return string.Empty;
            }

            return TokenService.GerarToken(loginDTO.Email); 
        }
    }
}
