using projectApiAngular.DTO;
using projectApiAngular.Models;
using projectApiAngular.Repositories;
using static projectApiAngular.DTO.PurcheseDto;

namespace projectApiAngular.Services
{
    public class PurcheseServicecs : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        public PurcheseServicecs(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        private static ReadPurcheseDto Map(Purchase p) =>
         new ReadPurcheseDto
         {
             Id = p.Id,
             CustomerId = p.CustomerId,
             CustomerName = p.Castomer?.Name,
             CustomerEmail = p.Castomer?.Email,
             GiftId = p.GiftId,
             GiftName = p.Gift?.Name,
             GiftPrice = p.Gift?.Price,
             PurchDate = p.PurchaseDate
         };
        //add purchase
        public async Task<ReadPurcheseDto> AddPurchaseAsync(CreatePurcheseDto dto)
        {
            try
            {

                if (dto.PurchDate > DateTime.Now)
                    throw new ArgumentException("Purchase date cannot be in the future.");

                var entity = new Purchase
                {
                    CustomerId = dto.CustomerId,
                    GiftId = dto.GiftId,
                    PurchaseDate = dto.PurchDate
                };

                var saved = await _purchaseRepository.AddPurchase(entity);
                return Map(saved);


            }
            catch (Exception ex)
            {
                throw new Exception( ex.Message);
            }


        }
        //get buyers details
        public async Task<IEnumerable<ReadPurcheseDto>> GetBuyersDetails()
        {
            var items = await _purchaseRepository.GetBuyersDetails();
            return items.Select(Map);
        }
        //get by gift
        public async Task<IEnumerable<ReadPurcheseDto>> GetPurchasesByGiftAsync(string name)
        {
            var items = await _purchaseRepository.GetPurchasesByGiftAsync(name);
            return items.Select(Map);
        }
        //sort by sellings
        public async Task<IEnumerable<ReadPurcheseDto>> GetGiftsSortedBySalesAsync()
        {
            var items = await _purchaseRepository.GetGiftsSortedBySalesAsync();
            return items.Select(Map);
        }
        //get purchases ordered by price
        public async Task<IEnumerable<ReadPurcheseDto>> GetPurchasesOrderedByPriceAsync()
        {
            var items = await _purchaseRepository.GetPurchasesOrderedByPriceAsync();
            return items.Select(Map);
        }
    }
}
