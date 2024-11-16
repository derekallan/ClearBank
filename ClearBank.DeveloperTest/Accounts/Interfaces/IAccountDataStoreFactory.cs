using System;

namespace ClearBank.DeveloperTest.Accounts.Interfaces;

public interface IAccountDataStoreFactory
{
    IAccountDataStore Create();
}
