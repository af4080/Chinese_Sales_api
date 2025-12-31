using projectApiAngular.DTO;

namespace projectApiAngular.Services
{
    public interface IBasketService
    {
        Task<BasketDto.ReadBasketDto?> DeleteBasketAsync(int id);
        Task<BasketDto.ReadBasketDto> EnterToBasketAsync(BasketDto.CreateBasketDto basketDto);
        Task<IEnumerable<BasketDto.ReadBasketDto>> GetMyBasket();
        Task<BasketDto.ReadBasketDto?> UpdateBasketAmountAsync(int id, int newAmount);
    }
}