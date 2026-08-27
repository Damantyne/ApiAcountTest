namespace SavingsApi.Models;

public class Account
{
    public string Id { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public record AmountRequest(decimal Amount);
public record BalanceResponse(string AccountId, decimal Balance);
public record TransactionResponse(string AccountId, decimal NewBalance, string Message);
public record ErrorResponse(string Error);