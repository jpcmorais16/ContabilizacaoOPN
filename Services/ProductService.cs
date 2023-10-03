using Contabilizacao.Data;
using Contabilizacao.Entities;
using Contabilizacao.Requests;

namespace Contabilizacao.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly UserRepository _userRepository;
    
    public ProductService(ProductRepository productRepository, UserRepository userRepository)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task RegisterProduct(RegisterProductRequest request)
    {
        var product = new Product
        {
            Code = request.Code,
            Name = request.Name,
            Price = request.Price,
            Unit = request.Unit,
            Measurement = request.Measurement
        };

        await _productRepository.Register(product);
        await _userRepository.AddEvent(request.AuthorIdn, EventType.Registration, request.Code);
    }

    public async Task<ProductToSupermarketToShift> AddToProduct(AddToProductRequest request)
    {
        var entity = await _productRepository.GetEntityByCodeSupermarketAndShift(request.ProductCode,
            request.SupermarketId, request.ShiftId);

        if (entity != null)
        {
            entity.Add(request.Amount);

            await _productRepository.UpdateAsync();
            
            await _userRepository.AddEvent(request.AuthorIdn, EventType.Addition, request.ProductCode, request.ShiftId, request.SupermarketId, request.Amount);

            return entity;
        }

        await _userRepository.AddEvent(request.AuthorIdn, EventType.Addition, request.ProductCode, request.ShiftId, request.SupermarketId, request.Amount);
        
        return await _productRepository.CreateEntity(request.ProductCode,
            request.SupermarketId, request.ShiftId, request.Amount);
    }

    public async Task<Product?> EditProduct(EditProductRequest request)
    {
        var product = await _productRepository.GetByCode(request.ProductCode);

        if (product == null)
            throw new Exception("Esse código de barras não está registrado.");

        product.Name = request.Name;
        product.Measurement = request.Measurement;
        product.Brand = request.Brand;
        product.Price = request.Price;
        product.Unit = request.Unit;

        await _productRepository.UpdateAsync();
        await _userRepository.AddEvent(request.AuthorIdn, EventType.Edition, request.ProductCode);

        return product;
    }
}