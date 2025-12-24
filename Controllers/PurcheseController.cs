namespace projectApiAngular.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using projectApiAngular.DTO;
    using projectApiAngular.Services;
    using static projectApiAngular.DTO.PurcheseDto;

    [ApiController]
    [Route("api/[controller]")]
    public class PurcheseController : ControllerBase
    {
        private readonly IPurchaseService _service;

        public PurcheseController(IPurchaseService service)
        {
            _service = service;
        }

        // POST: api/Purchese
        [HttpPost]
        public async Task<ActionResult<ReadPurcheseDto>> AddPurchase([FromBody] CreatePurcheseDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var created = await _service.AddPurchaseAsync(dto);
                // There is no GetById endpoint in the service, so return Created with the created resource.
                return Created(string.Empty, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // GET: api/Purchese/buyers
        [HttpGet("buyers")]
        public async Task<ActionResult<IEnumerable<ReadPurcheseDto>>> GetBuyersDetails()
        {
            var result = await _service.GetBuyersDetails();
            return Ok(result);
        }

        // GET: api/Purchese/gifts/sorted
        [HttpGet("gifts/sorted")]
        public async Task<ActionResult<IEnumerable<ReadPurcheseDto>>> GetGiftsSortedBySales()
        {
            var result = await _service.GetGiftsSortedBySalesAsync();
            return Ok(result);
        }

        // GET: api/Purchese/gift/{name}
        [HttpGet("gift/{name}")]
        public async Task<ActionResult<IEnumerable<PurcheseDto.ReadPurcheseDto>>> GetPurchasesByGift(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("Gift name is required.");

            var result = await _service.GetPurchasesByGiftAsync(name);
            return Ok(result);
        }

        // GET: api/Purchese/ordered-by-price
        [HttpGet("ordered-by-price")]
        public async Task<ActionResult<IEnumerable<PurcheseDto.ReadPurcheseDto>>> GetPurchasesOrderedByPrice()
        {
            var result = await _service.GetPurchasesOrderedByPriceAsync();
            return Ok(result);
        }
    }
}
