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

        public async Task<int> CriarClienteAsync(CadastrarClienteDTO clienteDTO)
        {
            Cliente cliente = new()
            {
                Nome = clienteDTO.Nome ?? string.Empty,
                Email = clienteDTO.Email ?? string.Empty,
                Logotipo = clienteDTO.Logotipo ?? [],
            };

            return await _clienteRepository.CriarClienteAsync(cliente);
        }

        public async Task<int> AtualizarClienteAsync(AtualizarClienteDTO clienteDTO)
        {
            var cliente = await _clienteRepository.ObterClientePorIdAsync(clienteDTO.Id);

            if (cliente is null)
            {
                return 0;
            }

            cliente.Nome = clienteDTO.Nome ?? cliente.Nome;
            cliente.Email = clienteDTO.Email ?? cliente.Email;
            cliente.Logotipo = clienteDTO.Logotipo ?? cliente.Logotipo;

            return await _clienteRepository.AtualizarClienteAsync(cliente);
        }

        public async Task<Cliente?> ObterClientePorIdAsync(Guid id)
        {
            return await _clienteRepository.ObterClientePorIdAsync(id);
        }

        public async Task RemoverClienteAsync(Guid id)
        {
            await _clienteRepository.RemoverClienteAsync(id);
        }

        public async Task<IEnumerable<Cliente>> ObterClientesAsync()
        {
            return await _clienteRepository.ObterClientesAsync();
        }
    }
}
