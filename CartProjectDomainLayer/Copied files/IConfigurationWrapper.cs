using System;
using System.Configuration;

namespace Deloitte.ACE.Core.ConfigurationWrapper
{
    public interface IConfigurationWrapper
    {
        string GetAppSettingValue(string key);

        ConnectionStringSettings GetConnectionString(string key);

        object GetSection(string key);
        string GetDecryptedConnectionString(string connectionStr);
        string GetConnectionStringGPS();
        string GetConnectionStringDEA();
        string GetConnectionStringAFR();
        string GetConnectionStringENR();

        string GetconnectionStringDBAL();
        string GetConnectionStringBasedOnProcess(Guid processId);
       

    }
}
