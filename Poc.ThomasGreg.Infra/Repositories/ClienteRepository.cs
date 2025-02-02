﻿using Dapper;
using Microsoft.Data.SqlClient;
using Poc.ThomasGreg.Domain.Entities;
using Poc.ThomasGreg.Domain.Interfaces;
using System.Data;

namespace Poc.ThomasGreg.Infra.Repositories
{
	public class ClienteRepository : IClienteRepository
    {
        private readonly string? _connectionString;

        public ClienteRepository()
		{
			_connectionString = Environment.GetEnvironmentVariable("ConnectionString");
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
        public async Task<int> AtualizarClienteAsync(Cliente cliente)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                cliente.Id,
                cliente.Nome,
                cliente.Email,
                cliente.Logotipo,
            };

            return await db.ExecuteAsync("sp_AtualizarCliente", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<Cliente?> ObterClientePorIdAsync(Guid id)
        {
            var sql = @"SELECT * FROM Cliente WHERE ID = @ID";

            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                id
            };
            var cliente = await db.QueryFirstOrDefaultAsync<Cliente>(sql, parameters);

            return cliente;
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

        public async Task<IEnumerable<Cliente>> ObterClientesAsync()
        {
            var sql = @"SELECT * FROM Cliente";

            using IDbConnection db = new SqlConnection(_connectionString);
 
            var clientes = await db.QueryAsync<Cliente>(sql);

            return clientes;
        }
    }
}
