using System.ComponentModel.DataAnnotations;

namespace projectApiAngular.DTO
{
    public class DonnerDto
    {
        public class CreateDonnerDto
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Phone { get; set; } 
        }

        public class UpdateDonnerDto
        {
      
            public string? Name { get; set; }
      
            public string? Email { get; set; }
   
            public string? Phone { get; set; }
        }

        public class ReadDonnerDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
        }
    }
}
