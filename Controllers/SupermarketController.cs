using Contabilizacao.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Contabilizacao.Controllers;

[EnableCors("corspolicy")]
[ApiController]
[Route("api/Supermarket")]
public class SupermarketController: ControllerBase
{
    private readonly SupermarketRepository _repository;
    
    public SupermarketController(SupermarketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetSupermarkets()
    {
        return Ok(await _repository.GetSupermarkets());
    }
}