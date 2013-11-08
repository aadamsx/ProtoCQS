using System;
using Core.Helper;

namespace Core
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
}