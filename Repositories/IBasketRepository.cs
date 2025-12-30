using projectApiAngular.Models;

namespace projectApiAngular.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> EnterToBasketAsync(Basket basket);
        Task<Basket?> DeleteBasketAsync(int id);
        Task<IEnumerable<Basket>> GetAllBasketsAsync();
        Task<Basket?> UpdateBasketAmountAsync(int id, int newAmount);
    }
}