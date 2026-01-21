using projectApiAngular.Models;
using projectApiAngular.Repositories;
using static projectApiAngular.DTO.UserDto;

namespace projectApiAngular.Services
{
    public class LotteryService : ILotteryService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IGiftRepository _giftRepository;
        private readonly ILogger<LotteryService> _logger;
        public LotteryService(IPurchaseRepository purchaseRepository, IGiftRepository giftRepository, ILogger<LotteryService> logger)
        {
            _purchaseRepository = purchaseRepository;
            _giftRepository = giftRepository;
            _logger = logger;
        }
        public async Task<ReadUserDto> RunLottery(string giftName)
        {
            _logger.LogInformation("Starting lottery for gift: {GiftName}", giftName);
            var gift = await _giftRepository.GetGiftByName(giftName);
            if (gift == null)
            {
                _logger.LogWarning("Gift with name {GiftName} not found.", giftName);
                throw new ArgumentException($"Gift with name {giftName} does not exist.");
            }
            if (gift.WinnerId != null)
            {
                _logger.LogWarning("Lottery already executed for gift: {GiftName}", giftName);
                throw new InvalidOperationException("Lottery already executed for this gift.");
            }
                


            var winnerId = await GetWinnerOfGift(gift.Name);
            var winner = await _giftRepository.UpdateGiftWinner(gift.Name, winnerId.Value);
            if (winner == null)
            {
                _logger.LogWarning("Failed to update winner for gift: {GiftName}", giftName);
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
            _logger.LogInformation("Selecting winner for gift: {GiftName}", giftName);
            var purchases = await _purchaseRepository.GetPurchasesByGiftAsync(giftName);
            if (purchases == null || !purchases.Any())
                return null;
            var randomIndex = new Random().Next(0, purchases.Count());
            var winnerId = purchases.ElementAt(randomIndex).CustomerId;
            _logger.LogInformation("Winner selected: CustomerId {WinnerId} for gift: {GiftName}", winnerId, giftName);
            return winnerId;

        }
    }
}