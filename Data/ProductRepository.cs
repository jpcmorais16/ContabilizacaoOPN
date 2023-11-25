using System.Runtime.InteropServices;
using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Contabilizacao.Data;

public class ProductRepository
{
    private readonly ApplicationContext _context;
    private readonly IMemoryCache _cache;
    private readonly ILogger<ProductRepository> _logger;
    private List<string> _cacheKeyList;
    private int _hits = 0;
    
    public ProductRepository(ApplicationContext context, IMemoryCache cache, ILogger<ProductRepository> logger)
    {
        _cache = cache;
        _context = context;
        _logger = logger;
        _cacheKeyList = new List<string>();
    }

    public async Task<Product?> GetByCode(string code)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
    }

    public async Task Register(Product product)
    {
        if (_context.Products.Any(p => p.Code == product.Code)) throw new Exception("Código de barras já cadastrado");
        
        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();
            
        ResetCache();
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

    public async Task<List<Product>> GetByTerm(string term)
    {
        if (_cache.TryGetValue(term, out List<Product> results))
        {
            _hits += 1;
            _logger.LogInformation($"Hits: {_hits}");
            return results;
        }
        
        results = await _context.Products
            .Where(p => term.Length <= 2 ? p.Name.StartsWith(term) : p.Name.Contains(term)).ToListAsync();
        
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        _cache.Set(term, results, cacheEntryOptions);
        
        _cacheKeyList.Add(term);

        return results;
    }

    public void ResetCache()
    {
        foreach (var key in _cacheKeyList)
        {
            _cache.Remove(key);
        }

        _cacheKeyList = new List<string>();
    }
}