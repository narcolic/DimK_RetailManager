using System.Collections.Generic;

namespace DKRDataManager.Library.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();
        void Dispose();
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void RollBackTransaction();
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
        void SaveDataInTransaction<T>(string storedProcedure, T parameters);
        int SaveDataScalar<T>(string storedProcedure, T parameters, string connectionStringName);
        int SaveDataScalarInTransaction<T>(string storedProcedure, T parameters);
        void StartTransaction(string connectionStringName);
    }
}