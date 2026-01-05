using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using projectApiAngular.Models;
using projectApiAngular.Repositories;
using System.Security.Claims;
using static projectApiAngular.DTO.BasketDto;
using static projectApiAngular.DTO.GiftDto;
using static projectApiAngular.DTO.UserDto;

namespace projectApiAngular.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<BasketService> _logger;
        public BasketService(IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor, ILogger<BasketService> logger)
        {
            _basketRepository = basketRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        //GetCurrentUserId

        private int GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity!.IsAuthenticated)
                throw new UnauthorizedAccessException();

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new Exception("User Id claim missing");

            return int.Parse(userIdClaim.Value);
        }


        //map to dto

        private static ReadBasketDto Map(Basket b)
        {

            return new ReadBasketDto
            {
                Id = b.Id,
                Amount = b.Amount,
                UserId = b.UserId,
                User = new ReadUserDto
                {
                    Id = b.User.Id,
                    Name = b.User.Name,
                    Email = b.User.Email,
                    Phone = b.User.Phone,
                    Role = Role.user.GetDisplayName()

                },
                GiftId = b.GiftId,
                Gift = new ReadGiftDto
                {
                    Name = b.Gift.Name,
                    Description = b.Gift.Description,
                    Id = b.Gift.Id,
                    Price = b.Gift.Price,
                    ImagePath = b.Gift.ImagePath,
                    CategoryName = b.Gift.Category.Name,
                    DonerName = b.Gift.Doner.Name,
                    DonerId = b.Gift.DonerId,
                    CategoryId = b.Gift.CategoryId

                }
            };
        }



        //get my basket
        public async Task<IEnumerable<ReadBasketDto>> GetMyBasket()
        {
            int userId = GetCurrentUserId();
            _logger.LogInformation(
         "Fetching basket for user {UserId}",userId);

            var baskets = await _basketRepository.GetMyBasket(userId);
            _logger.LogInformation("User {UserId} has {Count} basket items", userId, baskets.Count());
            return baskets.Select(Map);
        }


        //EnterToBasketAsync
        public async Task<ReadBasketDto> EnterToBasketAsync(CreateBasketDto basketDto)
        {
            int userId = GetCurrentUserId();
            _logger.LogInformation(
            "User {UserId} adding gift {GiftId} amount {Amount} to basket",
            userId,
            basketDto.GiftId,
            basketDto.Amount
);

            var entity = new Basket
            {
                UserId = userId,
                GiftId = basketDto.GiftId,
                Amount = basketDto.Amount
            };

            var basket = await _basketRepository.EnterToBasketAsync(entity);
            _logger.LogInformation(
            "Basket item {BasketId} created for user {UserId}",
            basket.Id,
            userId
);

            return Map(basket);
        }

        //update amount
        public async Task<ReadBasketDto?> UpdateBasketAmountAsync(int id, int newAmount)
        {
            if (newAmount <= 0 || newAmount > 1000)
            {
                _logger.LogWarning(
                "Invalid basket amount {Amount} for basket {BasketId}",
                newAmount,
                id);
                throw new ArgumentException("Amount must be greater than zero and cannot exceed 1000.");
            }
            var basket = await _basketRepository.UpdateBasketAmountAsync(id, newAmount);
            if (basket == null)
            {
                        _logger.LogWarning(
             "Basket {BasketId} not found for update", id);
                return null;
            }
            _logger.LogInformation(
             "Basket {BasketId} updated to amount {Amount}",id,newAmount);
            return Map(basket);
        }
        //delete basket
        public async Task<ReadBasketDto?> DeleteBasketAsync(int id)
        {
            _logger.LogInformation(
           "Deleting basket {BasketId}",id);
            var basket = await _basketRepository.DeleteBasketAsync(id);
            if (basket == null)
            {
                _logger.LogWarning(
                "Basket {BasketId} not found for deletion", id );
                return null;
            }
            _logger.LogInformation(
            "Basket {BasketId} deleted successfully",id);
            return Map(basket);
        }
    }
}
