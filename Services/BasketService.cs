using Microsoft.OpenApi.Extensions;
using projectApiAngular.Models;
using projectApiAngular.Repositories;
using static projectApiAngular.DTO.BasketDto;
using static projectApiAngular.DTO.GiftDto;
using static projectApiAngular.DTO.UserDto;

namespace projectApiAngular.Services
{
    public class BasketService
    {
        private readonly IBasketRepository _basketRepository;
        public BasketService(IBasketRepository basketRepository)
        {
           _basketRepository = basketRepository;
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


        //get all baskets   
        public async Task<IEnumerable<ReadBasketDto>> GetAllBasketsAsync()
        {
            var basket = await _basketRepository.GetAllBasketsAsync();
            return basket.Select(Map);
        }
        //EntertoBasketAsync
        public async Task<ReadBasketDto> EntertoBasketAsync(CreateBasketDto basketDto)
        {
            try
            {
                var entity = new Basket
                {

                    UserId = basketDto.UserId,
                    GiftId = basketDto.GiftId,
                    amount = basketDto.amount,

                };

                 var basket=await _basketRepository.EntertoBasketAsync(entity);
                return Map(basket);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          

        }
    }
}
