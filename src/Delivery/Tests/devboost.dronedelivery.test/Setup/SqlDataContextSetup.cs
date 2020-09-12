using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace devboost.dronedelivery.test.Setup
{
    class SqlDataContextSetup : ISqlDataContext
    {

        private readonly SqlConnection _connection;

        public SqlDataContextSetup(string connectionString)
        {
            _connection = CreateConnection(connectionString);
        }

        public IDataReader ExecuteReader(string storedProcedureName, ICollection<SqlParameter> parameters)
        {
            throw new NotImplementedException();
        }

        private SqlConnection CreateConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            return new SqlConnection(connectionString);
        }
    }

    interface ISqlDataContext
    {
        IDataReader ExecuteReader(string storedProcedureName, ICollection<SqlParameter> parameters);
    }
}
