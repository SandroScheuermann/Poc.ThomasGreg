using Poc.ThomasGreg.Domain.Entities;

namespace Poc.ThomasGreg.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        public Task<int> InserirUsuarioAsync(Usuario usuario);
        public Task<Usuario> ObterUsuarioPorEmailAsync(string email);
    }
}
