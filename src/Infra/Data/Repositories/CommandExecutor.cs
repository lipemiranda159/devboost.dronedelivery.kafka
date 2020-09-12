using Dapper;
using devboost.dronedelivery.domain.Constants;
using devboost.dronedelivery.domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace devboost.dronedelivery.felipe.EF.Repositories
{
    [ExcludeFromCodeCoverage]
    public class CommandExecutor<T> : ICommandExecutor<T> where T : class
    {
        private readonly string _connectionString;

        public CommandExecutor(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(ProjectConsts.CONNECTION_STRING_CONFIG);

        }

        public IEnumerable<T> ExcecuteCommand(string command)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                return conexao.Query<T>(command);
            }
        }

        public async Task ExecuteCommandAsync(string command)
        {
            using (SqlConnection conexao = new SqlConnection(_connectionString))
            {
                await conexao.ExecuteAsync(command);
            }
        }
    }
}
