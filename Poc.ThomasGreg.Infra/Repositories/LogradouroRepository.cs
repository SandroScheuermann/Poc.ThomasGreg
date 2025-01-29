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

        public async Task<int> CriarLogradouroAsync(Logradouro logradouro)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                logradouro.Endereco,
                logradouro.ClienteId,
            };

            return await db.ExecuteAsync("sp_AdicionarLogradouro", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<int> AtualizarLogradouroAsync(Logradouro logradouro)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                logradouro.Endereco,
                logradouro.Id,
            };

            return await db.ExecuteAsync("sp_AtualizarLogradouro", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<Logradouro>> ObterLogradourosAsync()
        {
            var sql = @"SELECT * FROM Logradouro";

            using IDbConnection db = new SqlConnection(_connectionString);

            return await db.QueryAsync<Logradouro>(sql);
        }
        public async Task<IEnumerable<Logradouro>> ObterLogradourosPorClienteIdAsync(Guid clienteId)
        {
            var sql = @"SELECT * FROM Logradouro WHERE ClienteId = @CLIENTEID";

            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                clienteId
            };

            return await db.QueryAsync<Logradouro>(sql, parameters);
        }
        public async Task<Logradouro?> ObterLogradourosPorIdAsync(Guid id)
        {
            var sql = @"SELECT * FROM Logradouro WHERE Id = @ID";

            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                id
            };

            return await db.QueryFirstOrDefaultAsync<Logradouro>(sql, parameters);
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
