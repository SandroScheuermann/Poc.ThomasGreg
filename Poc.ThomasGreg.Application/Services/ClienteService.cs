using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Application.Services.Interfaces;
using Poc.ThomasGreg.Domain.Entities;
using Poc.ThomasGreg.Domain.Interfaces;

namespace Poc.ThomasGreg.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task AtualizarClienteAsync(ClienteDTO clienteDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CriarClienteAsync(ClienteDTO clienteDTO)
        {
            Cliente cliente = new()
            { 
                Nome = clienteDTO.Nome ?? string.Empty,
                Email = clienteDTO.Email ?? string.Empty,
                Logotipo = clienteDTO.Logotipo ?? [],
            };

            return await _clienteRepository.CriarClienteAsync(cliente);
        }

        public Task<ClienteDTO> ObterClientePorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task RemoverClienteAsync(Guid id)
        {
            await _clienteRepository.RemoverClienteAsync(id);
        }
    }
}
