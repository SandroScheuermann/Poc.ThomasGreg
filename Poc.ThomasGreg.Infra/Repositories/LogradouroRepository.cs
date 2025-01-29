using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Poc.ThomasGreg.Domain.Entities;
using Poc.ThomasGreg.Domain.Interfaces;
using System.Data;

namespace Poc.ThomasGreg.Infra.Repositories
{
    public class LogradouroRepository : ILogradouroRepository
    {
        private readonly string? _connectionString;

        public LogradouroRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CriarLogradouroAsync(Logradouro logradouro)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                logradouro.Endereco,
                logradouro.ClienteId,
            };

            await db.ExecuteAsync("sp_AdicionarLogradouro", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<Logradouro>> ObterLogradourosAsync()
        {
            var sql = @"SELECT * FROM Logradouros";

            using IDbConnection db = new SqlConnection(_connectionString);

            return await db.QueryAsync<Logradouro>(sql, commandType: CommandType.StoredProcedure);
        }
        public async Task RemoverLogradouroAsync(Guid id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                id
            };

            await db.ExecuteAsync("sp_RemoverLogradouro", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
