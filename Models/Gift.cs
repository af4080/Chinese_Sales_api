using System.ComponentModel.DataAnnotations.Schema;

namespace projectApiAngular.Models
{
    public class Gift
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int DonerId { get; set; }
        public Donner Doner { get; set; }
        public string  ImagePath { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }

        public int? WinnerId { get; set; }
        public Customer? Winner { get; set; }

        public List<Purchase> Purchases { get; set; }






    }
}
