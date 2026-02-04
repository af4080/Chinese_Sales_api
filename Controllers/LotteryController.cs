using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectApiAngular.DTO;
using projectApiAngular.Services;

namespace projectApiAngular.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        private readonly ILotteryService _lotteryService;

        public LotteryController(ILotteryService lotteryService)
        {
            _lotteryService = lotteryService;
        }
        [HttpPost]
        public async Task<IActionResult> RunLottery()
        {
            try
            {
                var result = await _lotteryService.RunLottery();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllGiftWinners()
        {
            try
            {
                var result = await _lotteryService.GetAllGiftWinners();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("newSale")]
        public async Task<IActionResult> StartNewChineseSale()
        {
            try
            {
                await _lotteryService.StartNewChineseSale();
                return Ok("New sale started successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
