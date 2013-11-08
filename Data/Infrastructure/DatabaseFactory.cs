﻿using System;
using Core;
using Core.Helper;

namespace Data.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly Type _databaseType;

        public DatabaseFactory(string databaseTypeName)
        {
            Check.Argument.IsNotEmpty(databaseTypeName, "databaseTypeName");

            _databaseType = Type.GetType(databaseTypeName, true, true);
        }

        public DatabaseFactory()
            : this(new ConfigurationManagerWrapper().AppSettings["databaseTypeName"])
        {

        }

        public IDatabase CreateInstance()
        {
            return Activator.CreateInstance(_databaseType) as IDatabase;
        }
    }

    public interface IDatabase
    {
        IDbContext ReturnContext();
    }

    public interface IDbContext
    {
    }
}
