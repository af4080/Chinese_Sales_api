using System.ComponentModel.DataAnnotations;

namespace projectApiAngular.DTO
{
    public class CategoryDto
    {
        public class CreateCategoryDto
        {
            [Required]
            public string Name { get; set; }
        }
        public class UpdateCategoryDto
        {
            public string? Name { get; set; }
        }
        public class ReadCategoryDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
