using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Application.Services.Interfaces;
using Poc.ThomasGreg.Domain.Entities;
using Poc.ThomasGreg.Domain.Interfaces;

namespace Poc.ThomasGreg.Application.Services
{
    public class LogradouroService : ILogradouroService
    {
        private readonly ILogradouroRepository _logradouroRepository; 
        public LogradouroService(ILogradouroRepository logradouroRepository)
        {
            _logradouroRepository = logradouroRepository;
        }


        public async Task<int> CriarLogradouroAsync(CadastrarLogradouroDTO logradouroDTO)
        {
            Logradouro logradouro = new()
            {
                Endereco = logradouroDTO.Endereco,
                ClienteId = logradouroDTO.ClienteId,
            };

            return await _logradouroRepository.CriarLogradouroAsync(logradouro);
        }
        public async Task<int> AtualizarLogradouroAsync(AtualizarLogradouroDTO logradouroDTO)
        {
            var logradouro = await _logradouroRepository.ObterLogradourosPorIdAsync(logradouroDTO.Id);

            if(logradouro is null)
            {
                return 0;
            }

            logradouro.Endereco = logradouroDTO.Endereco ?? logradouro.Endereco;
            logradouro.ClienteId = logradouroDTO.ClienteId ?? logradouro.ClienteId;

            return await _logradouroRepository.AtualizarLogradouroAsync(logradouro);
        }

        public async Task<IEnumerable<Logradouro>> ObterLogradourosAsync()
        { 
            return await _logradouroRepository.ObterLogradourosAsync();
        } 
        public async Task<IEnumerable<Logradouro>> ObterLogradourosPorClienteIdAsync(Guid clienteId)
        {
            return await _logradouroRepository.ObterLogradourosPorClienteIdAsync(clienteId);
        }

        public async Task RemoverLogradouroAsync(Guid id)
        {
            await _logradouroRepository.RemoverLogradouroAsync(id);
        }

        public async Task<Logradouro?> ObterLogradouroPorIdAsync(Guid id)
        {
            return await _logradouroRepository.ObterLogradourosPorIdAsync(id);
        }
    }
}
