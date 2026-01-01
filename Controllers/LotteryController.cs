using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectApiAngular.Services;

namespace projectApiAngular.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController: ControllerBase
    {
        private readonly LotteryService _service;

        public LotteryController(LotteryService service)
        {
            _service = service;
        }
        [HttpPost]
        public async  Task<IActionResult> RunLottery(string giftName)
        {
            try
            {
                var result = await _service.RunLottery(giftName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }
    }
}
