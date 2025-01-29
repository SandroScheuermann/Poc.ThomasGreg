using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Poc.ThomasGreg.Domain.Entities;
using Poc.ThomasGreg.Domain.Interfaces;
using System.Data;

namespace Poc.ThomasGreg.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string? _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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
    }
}
