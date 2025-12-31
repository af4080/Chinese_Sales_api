using System.ComponentModel.DataAnnotations;
using static projectApiAngular.DTO.GiftDto;
using static projectApiAngular.DTO.UserDto;

namespace projectApiAngular.DTO
{
    public class BasketDto
    {
        public class CreateBasketDto
        {

            public int amount { get; set; } = 1;

            [Required]
            public int GiftId { get; set; }

        }
        public class ReadBasketDto
        {
            public int Id { get; set; }
            public int amount { get; set; } = 1;
            public int UserId { get; set; }

            public required ReadUserDto user { get; set; }

            public int GiftId { get; set; }

            public required ReadGiftDto gift { get; set; }
        }
    }
}


