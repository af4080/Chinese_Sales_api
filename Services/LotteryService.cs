using projectApiAngular.Models;
using projectApiAngular.Repositories;
using static projectApiAngular.DTO.UserDto;

namespace projectApiAngular.Services
{
    public class LotteryService : ILotteryService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IGiftRepository _giftRepository;
        public LotteryService(IPurchaseRepository purchaseRepository, IGiftRepository giftRepository)
        {
            _purchaseRepository = purchaseRepository;
            _giftRepository = giftRepository;
        }
        public async Task<ReadUserDto> RunLottery(string giftName)
        {

            var gift = await _giftRepository.GetGiftByName(giftName);
            if (gift == null)
            {
                throw new ArgumentException($"Gift with name {giftName} does not exist.");
            }

            var winnerId = await GetWinnerOfGift(gift.Name);
            var winner = await _giftRepository.UpdateGiftWinner(gift.Name, winnerId.Value);
            if (winner == null)
            {
                throw new InvalidOperationException("Error updating gift winner.");
            }

            return new ReadUserDto
            {
                Id = winner.Id,
                Email = winner.Email,
                Name = winner.Name,
                Phone = winner.Phone,
                Role = winner.Role.ToString()


            };
        }

        private async Task<int?> GetWinnerOfGift(string giftName)
        {
            var purchases = await _purchaseRepository.GetPurchasesByGiftAsync(giftName);
            if (purchases == null || !purchases.Any())
                return null;
            var randomIndex = new Random().Next(0, purchases.Count());
            var winnerId = purchases.ElementAt(randomIndex).CustomerId;
            return winnerId;

        }
    }
}