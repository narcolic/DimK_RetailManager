﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DKRDataManager.Library.Internal.DataAccess
{
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SqlDataAccess> _logger;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public SqlDataAccess(IConfiguration config, ILogger<SqlDataAccess> logger)
        {
            _config = config;
            _logger = logger;
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
        }

        public void Dispose()
        {
            if (_connection?.State != ConnectionState.Closed)
            {
                try
                {
                    CommitTransaction();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Commit txn failed in the dispose method");
                }
            }

            _transaction = null;
            _connection = null;
        }

        public string GetConnectionString(string name) => _config.GetConnectionString(name);

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                return connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters) =>
            _connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

        public void RollBackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void SaveDataInTransaction<T>(string storedProcedure, T parameters) =>
            _connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

        public int SaveDataScalar<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                return connection.ExecuteScalar<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public int SaveDataScalarInTransaction<T>(string storedProcedure, T parameters) =>
            _connection.ExecuteScalar<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

        public void StartTransaction(string connectionStringName)
        {
            _connection = new SqlConnection(GetConnectionString(connectionStringName));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
    }
}