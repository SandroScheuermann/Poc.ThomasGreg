using Poc.ThomasGreg.Domain.Entities;

namespace Poc.ThomasGreg.Domain.Interfaces
{
    public interface ILogradouroRepository
    {
        public Task<int> CriarLogradouroAsync(Logradouro logradouro);
        public Task<int> AtualizarLogradouroAsync(Logradouro logradouro);
        public Task<IEnumerable<Logradouro>> ObterLogradourosAsync();
        public Task<IEnumerable<Logradouro>> ObterLogradourosPorClienteIdAsync(Guid clienteId);
        public Task<Logradouro?> ObterLogradourosPorIdAsync(Guid id);
        public Task RemoverLogradouroAsync(Guid id);
    }
}
