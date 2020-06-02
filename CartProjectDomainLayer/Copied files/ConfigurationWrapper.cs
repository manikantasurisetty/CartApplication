using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Deloitte.ACE.Common;
using Deloitte.ACE.Domain.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Deloitte.ACE.Core.ConfigurationWrapper
{
    public class ConfigurationWrapper : IConfigurationWrapper
    {
        public string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public ConnectionStringSettings GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key];
        }
        public object GetSection(string key)
        {
            return ConfigurationManager.GetSection(key);
        }
        public string GetDecryptedConnectionString(string connectionStr)
        {
            string decConnStr = string.Empty;
            ACEEncryption objEncrption = new ACEEncryption();
            decConnStr = objEncrption.Decrypt(connectionStr);
            return decConnStr;
        }
        public string GetConnectionStringGPS()
        {
            string connectionString = string.Empty;
            string DecryptedConnectionString = string.Empty;
            ConnectionStringSettings ConnectionString = new ConnectionStringSettings();

            if (GetConnectionString("DataConnection") != null)
            {
                ConnectionString = GetConnectionString("DataConnection");
            }

            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);
            using (DbCommand objCMD = objDB.GetStoredProcCommand(SPConstants.GetConnectionStringBasedOnProcess))
            {
                string connectionStrigGPS = Process.GPS.GetDescription();
                Guid connectionStrigGPS_guid = new Guid(connectionStrigGPS);
                objDB.AddInParameter(objCMD, "@ReferenceProcessID", DbType.Guid, connectionStrigGPS_guid);

                DataSet connections = objDB.ExecuteDataSet(objCMD);
                if (connections != null && connections.Tables.Count > 0)
                {
                    DataTable dt = connections.Tables[0];
                    connectionString = dt.Rows[0].ItemArray[0].ToString();
                }

                DecryptedConnectionString = GetDecryptedConnectionString(connectionString);
                return DecryptedConnectionString;
            }
        }
        public string GetConnectionStringDEA()
        {

            string connectionString = "";
            string DecryptedConnectionString = "";
            ConnectionStringSettings ConnectionString = new ConnectionStringSettings();
            if (GetConnectionString("DataConnection") != null)
            {
                ConnectionString = GetConnectionString("DataConnection");
            }

            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);

            using (DbCommand objCMD = objDB.GetStoredProcCommand(SPConstants.GetConnectionStringBasedOnProcess))
            {
                string connectionStrigDEA = Process.DEA.GetDescription();
                Guid connectionStrigGPS_guid = new Guid(connectionStrigDEA);
                objDB.AddInParameter(objCMD, "@ReferenceProcessID", DbType.Guid, connectionStrigGPS_guid);

                DataSet connections = objDB.ExecuteDataSet(objCMD);
                if (connections != null && connections.Tables.Count > 0)
                {
                    DataTable dt = connections.Tables[0];
                    connectionString = dt.Rows[0].ItemArray[0].ToString();

                }

                DecryptedConnectionString = GetDecryptedConnectionString(connectionString);
                return DecryptedConnectionString;
            }
        }

        public string GetConnectionStringAFR()
        {

            string connectionString = "";
            string DecryptedConnectionString = "";
            ConnectionStringSettings ConnectionString = new ConnectionStringSettings();
            if (GetConnectionString("DataConnection") != null)
            {
                ConnectionString = GetConnectionString("DataConnection");
            }

            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);

            using (DbCommand objCMD = objDB.GetStoredProcCommand(SPConstants.GetConnectionStringBasedOnProcess))
            {
                string connectionStringAFR = Process.AFRUS.GetDescription();
                Guid connectionStringAFR_guid = new Guid(connectionStringAFR);
                objDB.AddInParameter(objCMD, "@ReferenceProcessID", DbType.Guid, connectionStringAFR_guid);

                DataSet connections = objDB.ExecuteDataSet(objCMD);
                if (connections != null && connections.Tables.Count > 0)
                {
                    DataTable dt = connections.Tables[0];
                    connectionString = dt.Rows[0].ItemArray[0].ToString();

                }

                DecryptedConnectionString = GetDecryptedConnectionString(connectionString);
                return DecryptedConnectionString;
            }
        }

        public string GetconnectionStringDBAL() {
            string connectionString = "";
            string DecryptedConnectionString = "";
            ConnectionStringSettings ConnectionString = new ConnectionStringSettings();
            if (GetConnectionString("DataConnection") != null)
            {
                ConnectionString = GetConnectionString("DataConnection");
            }

            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);

            using (DbCommand objCMD = objDB.GetStoredProcCommand(SPConstants.GetConnectionStringBasedOnProcess))
            {
                string connectionStrigDBAL = Process.DebitBalance.GetDescription();
                Guid connectionStrigDBAL_guid = new Guid(connectionStrigDBAL);
                objDB.AddInParameter(objCMD, "@ReferenceProcessID", DbType.Guid, connectionStrigDBAL_guid);

                DataSet connections = objDB.ExecuteDataSet(objCMD);
                if (connections != null && connections.Tables.Count > 0)
                {
                    DataTable dt = connections.Tables[0];
                    connectionString = dt.Rows[0].ItemArray[0].ToString();

                }

                DecryptedConnectionString = GetDecryptedConnectionString(connectionString);
                return DecryptedConnectionString;
            }
        }
        public string GetconnectionStringPreTravel()
        {
            string connectionString = "";
            string DecryptedConnectionString = "";
            ConnectionStringSettings ConnectionString = new ConnectionStringSettings();
            if (GetConnectionString("DataConnection") != null)
            {
                ConnectionString = GetConnectionString("DataConnection");
            }

            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);

            using (DbCommand objCMD = objDB.GetStoredProcCommand(SPConstants.GetConnectionStringBasedOnProcess))
            {
                string connectionStrigPreTravel = Process.PreTravel.GetDescription();
                Guid connectionStrigPreTravle_guid = new Guid(connectionStrigPreTravel);
                objDB.AddInParameter(objCMD, "@ReferenceProcessID", DbType.Guid, connectionStrigPreTravle_guid);

                DataSet connections = objDB.ExecuteDataSet(objCMD);
                if (connections != null && connections.Tables.Count > 0)
                {
                    DataTable dt = connections.Tables[0];
                    connectionString = dt.Rows[0].ItemArray[0].ToString();

                }

                DecryptedConnectionString = GetDecryptedConnectionString(connectionString);
                return DecryptedConnectionString;
            }
        }

        public string GetconnectionStringPreTravelHotel()
        {
            string connectionString = "";
            string DecryptedConnectionString = "";
            ConnectionStringSettings ConnectionString = new ConnectionStringSettings();
            if (GetConnectionString("DataConnection") != null)
            {
                ConnectionString = GetConnectionString("DataConnection");
            }

            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);

            using (DbCommand objCMD = objDB.GetStoredProcCommand(SPConstants.GetConnectionStringBasedOnProcess))
            {
                string connectionStrigPreTravelHotel = Process.PreTravelHotel.GetDescription();
                Guid connectionStringhotel_guid = new Guid(connectionStrigPreTravelHotel);
                objDB.AddInParameter(objCMD, "@ReferenceProcessID", DbType.Guid, connectionStringhotel_guid);

                DataSet connections = objDB.ExecuteDataSet(objCMD);
                if (connections != null && connections.Tables.Count > 0)
                {
                    DataTable dt = connections.Tables[0];
                    connectionString = dt.Rows[0].ItemArray[0].ToString();

                }

                DecryptedConnectionString = GetDecryptedConnectionString(connectionString);
                return DecryptedConnectionString;
            }
        }

        /// <summary>
        /// Get Connection string for ENR process
        /// </summary>
        /// <returns></returns>
        public string GetConnectionStringENR()
        {
            string connectionString = "";
            string DecryptedConnectionString = "";
            ConnectionStringSettings ConnectionString = new ConnectionStringSettings();
            if (GetConnectionString("DataConnection") != null)
            {
                ConnectionString = GetConnectionString("DataConnection");
            }

            Database objDB = new SqlDatabase(ConnectionString.ConnectionString);

            using (DbCommand objCMD = objDB.GetStoredProcCommand(SPConstants.GetConnectionStringBasedOnProcess))
            {
                string connectionStrigENR = Process.ENR.GetDescription();
                Guid connectionStrigGPS_guid = new Guid(connectionStrigENR);
                objDB.AddInParameter(objCMD, "@ReferenceProcessID", DbType.Guid, connectionStrigGPS_guid);

                DataSet connections = objDB.ExecuteDataSet(objCMD);
                if (connections != null && connections.Tables.Count > 0)
                {
                    DataTable dt = connections.Tables[0];
                    connectionString = dt.Rows[0].ItemArray[0].ToString();

                }

                DecryptedConnectionString = GetDecryptedConnectionString(connectionString);
                return DecryptedConnectionString;
            }
        }

        public string GetConnectionStringBasedOnProcess(Guid processId)
        {
            string connectionString = string.Empty;

            ConnectionStringSettings connectionStringSettings = GetConnectionString("DataConnection");
            Database objDB = new SqlDatabase(connectionStringSettings.ConnectionString);

            using (DbCommand objCommand = objDB.GetStoredProcCommand(SPConstants.GetConnectionStringBasedOnProcess))
            {
                objDB.AddInParameter(objCommand, "@ReferenceProcessID", DbType.Guid, processId);
                connectionString = Convert.ToString(objDB.ExecuteScalar(objCommand));
            }

            connectionString = GetDecryptedConnectionString(connectionString);

            return connectionString;
        }
    }
}
