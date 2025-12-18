using projectApiAngular.DTO;

namespace projectApiAngular.Services
{
    public interface IDonnerService
    {
        Task<DonnerDto.ReadDonnerDto> AddDonner(DonnerDto.CreateDonnerDto dto);
        Task<DonnerDto.ReadDonnerDto?> DeleteDonner(int id);
        Task<IEnumerable<DonnerDto.ReadDonnerDto>> GetAllDonners();
        Task<DonnerDto.ReadDonnerDto?> GetDonnerByEmail(string email);
        Task<DonnerDto.ReadDonnerDto?> GetDonnerByGiftId(int giftId);
        Task<DonnerDto.ReadDonnerDto?> GetDonnerById(int id);
        Task<DonnerDto.ReadDonnerDto?> GetDonnerByName(string name);
        Task<DonnerDto.ReadDonnerDto?> UpdateDonner(int id, DonnerDto.CreateDonnerDto dto);
    }
}