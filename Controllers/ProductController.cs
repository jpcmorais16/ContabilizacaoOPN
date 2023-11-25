using Contabilizacao.Data;
using Contabilizacao.Requests;
using Contabilizacao.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Contabilizacao.Controllers;

[EnableCors("corspolicy")]
[ApiController]
[Route("api/product")]
public class ProductController: ControllerBase
{
    private readonly ProductRepository _productRepository;
    private readonly ProductService _service;
    
    public ProductController(ProductRepository productRepository, ProductService service)
    {
        _productRepository = productRepository;
        _service = service;
    }
    
    [HttpGet("Check")]
    public async Task<IActionResult> CheckProduct([FromQuery] string code)
    {
        var response = await _productRepository.GetByCode(code);

        if (response == null)
        {
            return NotFound();
        }
        
        return Ok(response);
    }
    
    [HttpPost("Add")]
    public async Task<IActionResult> AddToProduct([FromBody] AddToProductRequest request)
    {
        var result = await _service.AddToProduct(request);
        return Ok(result);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterProduct([FromBody] RegisterProductRequest request)
    {
        await _service.RegisterProduct(request);
        return Ok();
    }

    [HttpPut("Edit")]
    public async Task<IActionResult> EditProduct([FromBody] EditProductRequest request)
    {
        return Ok(await _service.EditProduct(request));
    }

    [HttpGet("Search")]
    public async Task<IActionResult> SearchProducts([FromQuery] string term)
    {
        return Ok(await _productRepository.GetByTerm(term));
    }
}