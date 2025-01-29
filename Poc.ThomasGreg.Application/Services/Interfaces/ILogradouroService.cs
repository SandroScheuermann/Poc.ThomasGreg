using Poc.ThomasGreg.Application.DTOs;
using Poc.ThomasGreg.Domain.Entities;

namespace Poc.ThomasGreg.Application.Services.Interfaces
{
    public interface ILogradouroService
    {
        public Task<int> CriarLogradouroAsync(CadastrarLogradouroDTO logradouroDTO);
        public Task<IEnumerable<Logradouro>> ObterLogradourosPorClienteIdAsync(Guid id);
        public Task<IEnumerable<Logradouro>> ObterLogradourosAsync();
        public Task<int> AtualizarLogradouroAsync(AtualizarLogradouroDTO logradouroDTO);
        public Task RemoverLogradouroAsync(Guid id);

    }
}
