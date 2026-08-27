using System.Collections.Concurrent;
using SavingsApi.Models;

namespace SavingsApi.Repositories;

public interface IAccountRepository
{
    Account GetById(string id);
    void Save(Account account);
}

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly ConcurrentDictionary<string, Account> _accounts = new(); AccountRepository.cs

    public Account GetById(string id)
    {
        if (!_accounts.TryGetValue(id, out var account))
        {
            // Si la cuenta no existe en memoria, se inicializa con saldo 0
            account = new Account { Id = id, Balance = 0, UpdatedAt = DateTime.UtcNow };
            _accounts[id] = account;
        }
        return account;
    }

    public void Save(Account account)
    {
        account.UpdatedAt = DateTime.UtcNow;
        _accounts[account.Id] = account;
    }
}