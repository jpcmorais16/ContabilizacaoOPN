using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contabilizacao.Data;

public class SupermarketRepository
{
    private readonly ApplicationContext _context;

    public SupermarketRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Supermarket>> GetSupermarkets()
    {
        return await _context.Supermarkets.ToListAsync();
    }
}