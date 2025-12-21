using projectApiAngular.Models;

namespace projectApiAngular.DTO
{
    public class GiftDto
    {
        public class ReadGiftDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; } = 10;
            public string ImagePath { get; set; }
            public string CategoryName { get; set; }
             
            public string DonerName { get; set; }



        }
        public class CreateGiftDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public int DonerId { get; set; }
            public string ImagePath { get; set; }
           
            public int CategoryId { get; set; }
         
        }

        public class UpdateGiftDto
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public int? Price { get; set; }
            public int? DonerId { get; set; }
            public string? ImagePath { get; set; }
            public int? CategoryId { get; set; }

        }
    }
}
