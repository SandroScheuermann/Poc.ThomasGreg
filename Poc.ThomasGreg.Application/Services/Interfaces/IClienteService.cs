using Poc.ThomasGreg.Application.DTOs;

namespace Poc.ThomasGreg.Application.Services.Interfaces
{
    public interface IClienteService
    {
        public Task<int> CriarClienteAsync(ClienteDTO clienteDTO); 
        public Task<ClienteDTO> ObterClientePorIdAsync(Guid id);
        public Task AtualizarClienteAsync(ClienteDTO clienteDTO);
        public Task RemoverClienteAsync(Guid id);
    }
}
