using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using projectApiAngular.Services;
using static projectApiAngular.DTO.GiftDto;

namespace projectApiAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
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
            var gifts = await _giftService.GetAllGifts();
            return Ok(gifts);
        }
        //get by name
        [HttpGet("{name}")]
        public async Task<IActionResult> GetGiftByName(string name)
        {
            var gift = await _giftService.GetGiftByName(name);
            if (gift == null)
                return NotFound();
            return Ok(gift);
        }
        //get by doner
        [HttpGet("doner/{name}")]
        public async Task<IActionResult> GetGiftByDonnerName(string name)
        {
            var gifts = await _giftService.GetGiftByDonnerName(name);
            return Ok(gifts);
        }
        //get by num customer
        [HttpGet("numcustomer/{count}")]
        public async Task<IActionResult> GetbyNumCastomer(int count)
        {
            var gifts = await _giftService.GetbyNumCastomer(count);
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
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }


        }
        //delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGift(int id)
        {
            try
            {
                var deletedGift = await _giftService.DeleteGift(id);
                if (deletedGift == null)
                    return NotFound();
                return Ok(deletedGift);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        //update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGift(string name, [FromBody] UpdateGiftDto gift)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedGift = await _giftService.UpdateGift(name, gift);
                if (updatedGift == null)
                    return NotFound();
                return Ok(updatedGift);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }
    }
}
