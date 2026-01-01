using projectApiAngular.Models;

namespace projectApiAngular.Repositories
{
    public interface IGiftRepository
    {
        Task<Gift> AddGift(Gift gift);
        Task<Gift?> DeleteGift(int id);
        Task<IEnumerable<Gift>> GetAllGifts();
        Task<IEnumerable<Gift?>> GetbyNumCastomer(int count);
        Task<IEnumerable<Gift?>> GetGiftByDonnerName(string name);
        Task<Gift?> GetGiftByName(string name);
        Task<Gift?> UpdateGift(string name, Gift gift);

        Task<User?> UpdateGiftWinner(string name, int winnerId);
    }
}