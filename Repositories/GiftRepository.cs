using Microsoft.EntityFrameworkCore;
using projectApiAngular.Data;
using projectApiAngular.Models;

namespace projectApiAngular.Repositories
{
    public class GiftRepository : IGiftRepository
    {
        private readonly Chinese_SalesDbContext _context;

        public GiftRepository(Chinese_SalesDbContext context)
        {
            _context = context;
        }

        //get
        public async Task<IEnumerable<Gift>> GetAllGifts()
        {
            return await _context.Gifts.Include(g => g.category).Include(d => d.Doner).ToListAsync();
        }
        //get by name
        public async Task<Gift?> GetGiftByName(string name)
        {
            return await _context.Gifts.Include(name => name.category).Include(d => d.Doner)
                .FirstOrDefaultAsync(p => p.Name == name);
        }
        //get by doner
        public async Task<IEnumerable<Gift?>> GetGiftByDonnerName(string name)
        {
            return await _context.Gifts.Include(d => d.Doner).Include(n=>n.category)
                .Where(p => p.Doner.Name == name).ToListAsync();
        }
        //get by num customer
        public async Task<IEnumerable<Gift?>> GetbyNumCastomer(int count)
        {
            return await _context.Gifts.Include(p => p.Purchases).Where(d => d.Purchases.Count == count).ToListAsync();
        }
        //post
   
public async Task<Gift> AddGift(Gift gift)
    {
        // Validate foreign keys to avoid FK constraint errors
        if (!await _context.Categories.AnyAsync(c => c.Id == gift.CategoryId))
            throw new ArgumentException($"Category with id {gift.CategoryId} does not exist.");

        if (!await _context.Doners.AnyAsync(d => d.Id == gift.DonerId))
            throw new ArgumentException($"Doner with id {gift.DonerId} does not exist.");

        // Prevent unique index violation on Name (you have a unique index)
        if (await _context.Gifts.AnyAsync(g => g.Name == gift.Name))
            throw new ArgumentException($"A gift with the name '{gift.Name}' already exists.");

        _context.Gifts.Add(gift);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // preserve inner exception for diagnostics
            throw new InvalidOperationException("Error saving Gift to database. See inner exception for details.", ex);
        }

        // Ensure navigation properties are loaded before returning
        await _context.Entry(gift).Reference(g => g.category).LoadAsync();
        await _context.Entry(gift).Reference(g => g.Doner).LoadAsync();

        return gift;
    }
    //update
    public async Task<Gift?> UpdateGift(string name, Gift gift)
        {
            var existingGift = await _context.Gifts.FindAsync(name);
            if (existingGift == null)
            {
                return null;
            }
            existingGift.Name = gift.Name;
            existingGift.Price = gift.Price;
            existingGift.CategoryId = gift.CategoryId;
            existingGift.ImagePath = gift.ImagePath;
            existingGift.Description = gift.Description;
            //תורם אי אפשר לשנות אחרי תרומה

            await _context.SaveChangesAsync();
            return existingGift;
        }

        //delete
        public async Task<Gift?> DeleteGift(int id)
        {
            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
            {
                return null;
            }
            _context.Gifts.Remove(gift);
            await _context.SaveChangesAsync();
            return gift;
        }
    }
}


