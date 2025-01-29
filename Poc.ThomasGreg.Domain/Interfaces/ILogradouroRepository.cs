using Poc.ThomasGreg.Domain.Entities;

namespace Poc.ThomasGreg.Domain.Interfaces
{
    public interface ILogradouroRepository
    {
        public Task CriarLogradouroAsync(Logradouro logradouro);
        public Task<IEnumerable<Logradouro>> ObterLogradourosAsync();
        public Task RemoverLogradouroAsync(Guid id);
    }
}
