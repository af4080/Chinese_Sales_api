using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using projectApiAngular.Services;
using static projectApiAngular.DTO.GiftDto;

namespace projectApiAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController: ControllerBase
    {
        private readonly IGiftService _giftService;

        public GiftController(IGiftService giftService)
        {
            _giftService = giftService;
        }
        //get
        [HttpGet]
        public async Task<IActionResult> GetAllGifts()
        {
            var gifts =await _giftService.GetAllGifts();
            return Ok(gifts);
        }
        //post
        [HttpPost]
        public async Task<IActionResult> Addgift([FromBody] CreateGiftDto gift)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var g = await _giftService.AddGift(gift);
                return Ok(g);


            }
            catch (Exception ex) {
                return BadRequest(new { error = ex.Message });
            }
         }   

    }
}
