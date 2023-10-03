using Contabilizacao.Data;
using Microsoft.AspNetCore.Mvc;

namespace Contabilizacao.Controllers;

[Route("User")]
public class UserController: ControllerBase
{
    private readonly UserRepository _repository;
    
    public UserController(UserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("Login")]
    public async Task<IActionResult> Login(string idn, string name)
    {
        var user = await _repository.GetUserByIdn(idn) ?? await _repository.RegisterUser(idn, name);

        return Ok(user);
    }

    [HttpGet("History")]
    public async Task<IActionResult> GetHistory(string idn)
    {
        return Ok(await _repository.GetUserHistory(idn));
    }
}