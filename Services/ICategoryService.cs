using projectApiAngular.DTO;

namespace projectApiAngular.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto.ReadCategoryDto> AddCategory(CategoryDto.CreateCategoryDto category);
        Task<CategoryDto.ReadCategoryDto?> DeleteCategory(int id);
        Task<IEnumerable<CategoryDto.ReadCategoryDto>> GetAllCategories();
        Task<CategoryDto.ReadCategoryDto?> UpdateCategory(int id, CategoryDto.UpdateCategoryDto category);
    }
}