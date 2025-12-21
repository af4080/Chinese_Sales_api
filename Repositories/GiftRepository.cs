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
            return await _context.Gifts
                .FirstOrDefaultAsync(p => p.Name == name);
        }
        //get by doner
        public async Task<IEnumerable<Gift?>> GetGiftByDonnerName(string name)
        {
            return await _context.Gifts.Include(d => d.Doner)
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
            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();
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


