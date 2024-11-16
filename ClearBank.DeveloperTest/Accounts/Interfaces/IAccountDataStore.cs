using System;
using ClearBank.DeveloperTest.Accounts.Domain;

namespace ClearBank.DeveloperTest.Accounts.Interfaces;

public interface IAccountDataStore
{
    public Account? GetAccount(string accountNumber);
    public void UpdateAccount(Account account);
}
