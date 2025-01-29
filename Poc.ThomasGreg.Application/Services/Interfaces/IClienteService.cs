using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Domain.Entities;

namespace Poc.ThomasGreg.Application.Services.Interfaces
{
    public interface IClienteService
    {
        public Task<int> CriarClienteAsync(CadastrarClienteDTO clienteDTO); 
        public Task<Cliente?> ObterClientePorIdAsync(Guid id);
        public Task<int> AtualizarClienteAsync(AtualizarClienteDTO clienteDTO);
        public Task RemoverClienteAsync(Guid id); 
        public Task<IEnumerable<Cliente>> ObterClientesAsync();
    }
}
