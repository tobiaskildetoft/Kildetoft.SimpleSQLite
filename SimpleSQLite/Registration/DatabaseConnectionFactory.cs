﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DataAccess.Registration
{
    internal static class DatabaseConnectionFactory
    {
        private static SQLiteAsyncConnection? _connection;

        internal static SQLiteAsyncConnection GetConnection()
        {
            return _connection ?? throw new NotImplementedException();
            // TODO: Custom exception here
        }

        internal static void Initialize(string connectionString)
        {
            _connection = new SQLiteAsyncConnection(connectionString);
        }

        internal static void AddTables(IEnumerable<Type> entityTypes)
        {
            if (_connection == null)
            {
                throw new InvalidOperationException("Cannot add tables before initializing the connection");
            }
            foreach (var type in entityTypes)
            {
                _connection.CreateTableAsync(type);
            }
        }
    }
}
