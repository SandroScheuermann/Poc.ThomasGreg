using Poc.ThomasGreg.Domain.Entities;

namespace Poc.ThomasGreg.Domain.Interfaces
{
    public interface IClienteRepository
    {
        public Task<int> CriarClienteAsync(Cliente cliente);
        public Task<Cliente?> ObterClientePorIdAsync(Guid id);
        public Task<IEnumerable<Cliente>> ObterClientesAsync();
        public Task<int> AtualizarClienteAsync(Cliente cliente);
        public Task RemoverClienteAsync(Guid id);
    }
}
