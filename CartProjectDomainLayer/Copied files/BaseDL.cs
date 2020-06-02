using Deloitte.ACE.Core;
using Deloitte.ACE.Core.ConfigurationWrapper;
using QueryEngine.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.ACE.DataLayer
{
    public class BaseDL
    {
        protected ConfigurationWrapper _configurationWrapper;
        protected bool IsDBParametersOverwritten;
        public BaseDL()
        {
            _configurationWrapper = new ConfigurationWrapper();
        }

        #region Helper Methods

        /// <summary>
        /// This Method Will Execute Your Stored Procedure in Generic Database
        /// </summary>
        /// <param name="objStoredProcedure"></param>
        public void DBPerformAction(StoredProcedure objStoredProcedure)
        {
            string connectionString = _configurationWrapper.GetConnectionString("DataConnection").ConnectionString;

            DBHelper.ExecuteNonQuery(objStoredProcedure, connectionString);
        }

        /// <summary>
        /// This Method Will Execute Your Stored Procedure in Input Process
        /// </summary>
        /// <param name="objStoredProcedure"></param>
        /// <param name="processId"></param>
        public void DBPerformAction(StoredProcedure objStoredProcedure, Guid processId)
        {
            string connectionString = _configurationWrapper.GetConnectionStringBasedOnProcess(processId);

            DBHelper.ExecuteNonQuery(objStoredProcedure, connectionString);
        }

        public void DBPerformAction(StoredProcedure objStoredProcedure, string connectionString)
        {
            DBHelper.ExecuteNonQuery(objStoredProcedure, connectionString);
        }

        public IDataReader DBReadData(StoredProcedure objStoredProcedure)
        {
            string connectionString = _configurationWrapper.GetConnectionString("DataConnection").ConnectionString;

            return DBHelper.ExecuteReader(objStoredProcedure, connectionString);
        }

        public IDataReader DBReadData(StoredProcedure objStoredProcedure, Guid processId)
        {
            string connectionString = _configurationWrapper.GetConnectionStringBasedOnProcess(processId);

            return DBHelper.ExecuteReader(objStoredProcedure, connectionString);
        }

        public IDataReader DBReadData(StoredProcedure objStoredProcedure, string connectionString)
        {
            return DBHelper.ExecuteReader(objStoredProcedure, connectionString);
        }

        public DataSet DBLoadData(StoredProcedure objStoredProcedure)
        {
            string connectionString = _configurationWrapper.GetConnectionString("DataConnection").ConnectionString;

            return DBHelper.ExecuteProcedure(objStoredProcedure, connectionString);
        }

        public DataSet DBLoadData(StoredProcedure objStoredProcedure, Guid processId)
        {
            string connectionString = _configurationWrapper.GetConnectionStringBasedOnProcess(processId);

            return DBHelper.ExecuteProcedure(objStoredProcedure, connectionString);
        }

        public DataSet DBLoadData(StoredProcedure objStoredProcedure, string connectionString)
        {
            return DBHelper.ExecuteProcedure(objStoredProcedure, connectionString);
        }

        public object DBGetValue(StoredProcedure objStoredProcedure)
        {
            string connectionString = _configurationWrapper.GetConnectionString("DataConnection").ConnectionString;

            return DBHelper.ExecuteScalar(objStoredProcedure, connectionString);
        }

        public object DBGetValue(StoredProcedure objStoredProcedure, Guid processId)
        {
            string connectionString = _configurationWrapper.GetConnectionStringBasedOnProcess(processId);

            return DBHelper.ExecuteScalar(objStoredProcedure, connectionString);
        }

        public object DBGetValue(StoredProcedure objStoredProcedure, string connectionString)
        {
            return DBHelper.ExecuteScalar(objStoredProcedure, connectionString);
        }

        #endregion
    }
}
