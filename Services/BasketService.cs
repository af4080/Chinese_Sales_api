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
        public BasketService(IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor)
        {
            _basketRepository = basketRepository;
            _httpContextAccessor = httpContextAccessor;
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
                amount = b.amount,
                UserId = b.UserId,
                user = new ReadUserDto
                {
                    Id = b.User.Id,
                    Name = b.User.Name,
                    Email = b.User.Email,
                    Phone = b.User.Phone,
                    Role = Role.user.GetDisplayName()

                },
                GiftId = b.GiftId,
                gift = new ReadGiftDto
                {
                    Name = b.gift.Name,
                    Description = b.gift.Description,
                    Id = b.gift.Id,
                    Price = b.gift.Price,
                    ImagePath = b.gift.ImagePath,
                    CategoryName = b.gift.category.Name,
                    DonerName = b.gift.Doner.Name,
                    DonerId = b.gift.DonerId,
                    CategoryId = b.gift.CategoryId

                }
            };
        }



        //get my basket
        public async Task<IEnumerable<ReadBasketDto>> GetMyBasket()
        {
            int userId = GetCurrentUserId();

            var baskets = await _basketRepository.GetMyBasket(userId);
            return baskets.Select(Map);
        }


        //EnterToBasketAsync
        public async Task<ReadBasketDto> EnterToBasketAsync(CreateBasketDto basketDto)
        {
            int userId = GetCurrentUserId();

            var entity = new Basket
            {
                UserId = userId,
                GiftId = basketDto.GiftId,
                amount = basketDto.amount
            };

            var basket = await _basketRepository.EnterToBasketAsync(entity);
            return Map(basket);
        }

        //update amount
        public async Task<ReadBasketDto?> UpdateBasketAmountAsync(int id, int newAmount)
        {
            if (newAmount <= 0 || newAmount > 1000)
            {
                throw new ArgumentException("Amount must be greater than zero and cannot exceed 1000.");
            }
            var basket = await _basketRepository.UpdateBasketAmountAsync(id, newAmount);
            if (basket == null)
            {
                return null;
            }
            return Map(basket);
        }
        //delete basket
        public async Task<ReadBasketDto?> DeleteBasketAsync(int id)
        {
            var basket = await _basketRepository.DeleteBasketAsync(id);
            if (basket == null)
            {
                return null;
            }
            return Map(basket);
        }
    }
}
