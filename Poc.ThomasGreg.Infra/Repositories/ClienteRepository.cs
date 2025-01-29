using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Poc.ThomasGreg.Domain.Entities;
using Poc.ThomasGreg.Domain.Interfaces;
using System.Data;

namespace Poc.ThomasGreg.Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string? _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CriarClienteAsync(Cliente cliente)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                cliente.Nome,
                cliente.Email,
                cliente.Logotipo,
            };

            var result = await db.ExecuteAsync("sp_CriarCliente", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
        public async Task AtualizarClienteAsync(Cliente cliente)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                cliente.Id,
                cliente.Nome,
                cliente.Email,
                cliente.Logotipo,
            };

            await db.ExecuteAsync("sp_AtualizarCliente", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<Cliente?> ObterClientePorIdAsync(Guid id)
        {
            var sql = @"SELECT * FROM Cliente WHERE ID = @ID";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    id
                };
                var cliente = await db.QueryFirstOrDefaultAsync<Cliente>(sql, parameters);

                return cliente;
            }
        }
        public async Task RemoverClienteAsync(Guid id)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                id
            };

            await db.ExecuteAsync("sp_RemoverCliente", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
