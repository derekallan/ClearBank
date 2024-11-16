using System.Configuration;
using ClearBank.DeveloperTest.Accounts.Data;
using ClearBank.DeveloperTest.Accounts.Interfaces;

namespace ClearBank.DeveloperTest.Accounts.Factories;

public class AccountDataStoreFactory : IAccountDataStoreFactory
{
    public IAccountDataStore Create()
    {
        // Would be nice to swap this out for IConfiguration, or even better some 
        // strongly typed configuration, but that would be a behaviour change
        // At least we've encapsulated it in the factory
        var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

        return dataStoreType switch
        {
            "Backup" => new BackupAccountDataStore(),
            _ => new AccountDataStore(),
        };
    }
}
