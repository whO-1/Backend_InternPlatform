using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Application.Services.Mappings
{
    public class AgeGroupConvertor : IBaseConvertor<AgeGroup, AgeGroupDTO>
    {
        public AgeGroupConvertor() { }
        public AgeGroup DTOToEntity(AgeGroupDTO obj)
        {
            return new AgeGroup
            {
                AgeGroupId = obj.Id,
                Name = obj.Name,
                DisplayOrder = obj.DisplayOrder,
            };
        }
        public AgeGroupDTO EntityToDTO(AgeGroup entity)
        {
            return new AgeGroupDTO
            {
                Id = entity.AgeGroupId,
                Name = entity.Name,
                DisplayOrder = entity.DisplayOrder,
            };
        }
    }
}
