using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using System;

namespace internPlatform.Application.Services.Mappings
{
    public class EntryTypeConvertor : IBaseConvertor<EntryType, EntryTypeDTO>
    {
        public EntryType DTOToEntity(EntryTypeDTO obj)
        {
            throw new NotImplementedException();
        }

        public EntryTypeDTO EntityToDTO(EntryType entity)
        {
            return new EntryTypeDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                DisplayOrder = entity.DisplayOrder,
            };
        }
    }
}
