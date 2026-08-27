using Microsoft.AspNetCore.Mvc;
using SavingsApi.Models;
using SavingsApi.Services;

namespace SavingsApi.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    // Inyección de dependencias mediante el constructor
    public AccountController(IAccountService service)
    {
        _service = service;
    }

    // GET: api/accounts/usr123/balance
    [HttpGet("{id}/balance")]
    public IActionResult GetBalance(string id)
    {
        var response = _service.GetBalance(id);
        return Ok(response);
    }

    // POST: api/accounts/usr123/deposit
    [HttpPost("{id}/deposit")]
    public IActionResult Deposit(string id, [FromBody] AmountRequest req)
    {
        try
        {
            var response = _service.Deposit(id, req.Amount);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
    }

    // POST: api/accounts/usr123/withdraw
    [HttpPost("{id}/withdraw")]
    public IActionResult Withdraw(string id, [FromBody] AmountRequest req)
    {
        try
        {
            var response = _service.Withdraw(id, req.Amount);
            return Ok(response);
        }
        catch (InvalidOperationException ex) // Captura el error de saldo insuficiente
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
        catch (ArgumentException ex) // Captura montos <= 0
        {
            return BadRequest(new ErrorResponse(ex.Message));
        }
    }
}