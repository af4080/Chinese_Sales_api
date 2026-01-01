using projectApiAngular.DTO;

namespace projectApiAngular.Services
{
    public interface ILotteryService
    {
        Task<UserDto.ReadUserDto> RunLottery(string giftName);
    }
}