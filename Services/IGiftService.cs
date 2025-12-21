using projectApiAngular.DTO;

namespace projectApiAngular.Services
{
    public interface IGiftService
    {
        Task<GiftDto.ReadGiftDto> AddGift(GiftDto.CreateGiftDto gift);
        Task<GiftDto.ReadGiftDto?> DeleteGift(int id);
        Task<IEnumerable<GiftDto.ReadGiftDto>> GetAllGifts();
        Task<IEnumerable<GiftDto.ReadGiftDto?>> GetbyNumCastomer(int count);
        Task<IEnumerable<GiftDto.ReadGiftDto?>> GetGiftByDonnerName(string name);
        Task<GiftDto.ReadGiftDto?> GetGiftByName(string name);
        Task<GiftDto.ReadGiftDto?> UpdateGift(string name, GiftDto.UpdateGiftDto gift);
    }
}