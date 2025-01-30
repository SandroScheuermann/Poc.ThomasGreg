using Dapper;
using Microsoft.Data.SqlClient;
using Poc.ThomasGreg.Domain.Entities;
using Poc.ThomasGreg.Domain.Interfaces;
using System.Data;

namespace Poc.ThomasGreg.Infra.Repositories
{
	public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string? _connectionString;

        public UsuarioRepository()
		{
			_connectionString = Environment.GetEnvironmentVariable("ConnectionString");
		}

		public async Task<int> InserirUsuarioAsync(Usuario usuario)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                usuario.Nome,
                usuario.Email,
                usuario.SenhaHash
            };

            var result = await db.ExecuteAsync("sp_CriarUsuario", parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<Usuario?> ObterUsuarioPorEmailAsync(string email)
        {
            var sql = @"SELECT * FROM Usuario WHERE Email = @EMAIL";

            using IDbConnection db = new SqlConnection(_connectionString);
            var parameters = new
            {
                email
            };

            var result = await db.QueryFirstOrDefaultAsync<Usuario>(sql, parameters);

            return result;

        }
    }
}
