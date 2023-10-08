using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contabilizacao.Data;

public class ProductRepository
{
    private readonly ApplicationContext _context;
    
    public ProductRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByCode(string code)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task Register(Product product)
    {
        try
        {
            await _context.Products.AddAsync(product);
            
            await _context.SaveChangesAsync();
        }

        catch
        {
            throw new Exception("Código de barras já cadastrado");
        }
    }

    public async Task<ProductToSupermarketToShift?> GetEntityByCodeSupermarketAndShift(string productCode, int supermarketId, int shiftId)
    {
        return await _context.ProductsToSupermarketToShifts
            .Include(e => e.Product)
            .FirstOrDefaultAsync(p =>
            p.ProductCode == productCode
            && p.SupermarketId == supermarketId
            && p.ShiftId == shiftId);
    }
    
    public async Task<ProductToSupermarketToShift> CreateEntity(string productCode, int supermarketId, int shiftId, int amount)
    {
        if (!await _context.Products.AnyAsync(p => p.Code == productCode))
            throw new Exception("Código de barras não cadastrado");
        
        if(!await _context.Supermarkets.AnyAsync(s => s.Id == supermarketId))
            throw new Exception("Supermercado não cadastrado");
        
        if(!await _context.Shifts.AnyAsync(s => s.Id == shiftId))
            throw new Exception("Turno inexistente");
        
        var entity = new ProductToSupermarketToShift
        {
            ProductCode = productCode,
            SupermarketId = supermarketId,
            ShiftId = shiftId,
            Amount = amount
        };

        _context.ProductsToSupermarketToShifts.Add(entity);

        await _context.SaveChangesAsync();

        return entity;
    }
    
    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }
}