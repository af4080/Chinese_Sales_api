using System.ComponentModel.DataAnnotations;

namespace projectApiAngular.Models
{

   
    public class Customer
    {
        public int Id { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

       public List<Purchase> Purchases { get; set; }

        public List<Gift> WonGifts { get; set; }




    }
}
