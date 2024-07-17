using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Application.Services.Mappings
{
    public class AgeGroupConvertor : IBaseConvertor<AgeGroup, AgeGroupDTO>
    {
        public AgeGroupConvertor() { }
        public AgeGroup DTOToEntity(AgeGroupDTO obj)
        {
            throw new System.NotImplementedException();
        }

        public AgeGroupDTO EntityToDTO(AgeGroup entity)
        {
            return new AgeGroupDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                DisplayOrder = entity.DisplayOrder,
            };
        }
    }
}
