using SavingsApi.Models;
using SavingsApi.Repositories;

namespace SavingsApi.Services;

public interface IAccountService
{
    BalanceResponse GetBalance(string accountId);
    TransactionResponse Deposit(string accountId, decimal amount);
    TransactionResponse Withdraw(string accountId, decimal amount);
}

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public BalanceResponse GetBalance(string accountId)
    {
        var account = _repository.GetById(accountId);
        return new BalanceResponse(account.Id, account.Balance);
    }

    public TransactionResponse Deposit(string accountId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("El monto a depositar debe ser mayor a 0.");

        var account = _repository.GetById(accountId);
        account.Balance += amount;
        _repository.Save(account);

        return new TransactionResponse(account.Id, account.Balance, "Depósito realizado correctamente.");
    }

    public TransactionResponse Withdraw(string accountId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("El monto a retirar debe ser mayor a 0.");

        var account = _repository.GetById(accountId);

        if (amount > account.Balance)
            throw new InvalidOperationException("Saldo insuficiente para realizar el retiro.");

        account.Balance -= amount;
        _repository.Save(account);

        return new TransactionResponse(account.Id, account.Balance, "Retiro realizado correctamente.");
    }
}