using Contabilizacao.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contabilizacao.Data;

public class UserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdn(string idn)
    {
        var user =  await _context.Users.FirstOrDefaultAsync(u => u.Idn == idn);
        
        return user;
    }
    
    public async Task<User> RegisterUser(string idn, string name)
    {
        var user = new User(idn, name);
        
        await _context.Users.AddAsync(user);
        
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<List<Event>> GetUserHistory(string idn)
    {
        return await _context.Events.Where(e => e.UserIdn == idn).ToListAsync();
    }

    public async Task AddEvent(string idn, EventType type, string productCode, int? shiftId = null, int? supermarketId = null, int? amount = null)
    {
        var user = await _context.Users.Include(u => u.Events).FirstOrDefaultAsync(u => u.Idn == idn);

        if (user == null)
            throw new Exception("Usuário não encontrado-");

        user.Events!.Add(new Event()
        {
            UserIdn = idn,
            Type = type,
            ProductCode = productCode,
            SupermarketId = supermarketId,
            ShiftId = shiftId, 
            Amount = amount
        });
        
        await _context.SaveChangesAsync();
    }
}