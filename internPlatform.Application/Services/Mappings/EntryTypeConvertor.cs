using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Application.Services.Mappings
{
    public class EntryTypeConvertor : IBaseConvertor<EntryType, EntryTypeDTO>
    {
        public EntryTypeConvertor() { }
        public EntryType DTOToEntity(EntryTypeDTO obj)
        {
            return new EntryType
            {
                EntryTypeId = obj.Id,
                Name = obj.Name,
                DisplayOrder = obj.DisplayOrder,
            };
        }

        public EntryTypeDTO EntityToDTO(EntryType entity)
        {
            return new EntryTypeDTO
            {
                Id = entity.EntryTypeId,
                Name = entity.Name,
                DisplayOrder = entity.DisplayOrder,
            };
        }
    }
}
