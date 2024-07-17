using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Application.Services.Mappings
{
    public class CategoryConvertor : IBaseConvertor<Category, CategoryDTO>
    {
        public CategoryConvertor() { }
        public CategoryDTO EntityToDTO(Category entity)
        {
            return new CategoryDTO
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                DisplayOrder = entity.DisplayOrder
            };
        }

        public Category DTOToEntity(CategoryDTO obj)
        {
            return new Category
            {
                CategoryId = obj.CategoryId,
                Name = obj.Name,
                DisplayOrder = obj.DisplayOrder
            };
        }
    }
}
