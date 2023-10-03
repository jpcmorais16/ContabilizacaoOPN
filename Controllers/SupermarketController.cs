using Contabilizacao.Data;
using Microsoft.AspNetCore.Mvc;

namespace Contabilizacao.Controllers;

[Route("Supermarket")]
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