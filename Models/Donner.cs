using System.ComponentModel.DataAnnotations;

namespace projectApiAngular.Models
{
    public class Donner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<Gift> Gifts { get; set; }

    }
}
