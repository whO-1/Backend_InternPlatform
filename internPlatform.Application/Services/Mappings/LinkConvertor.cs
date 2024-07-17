using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using System;

namespace internPlatform.Application.Services.Mappings
{
    public class LinkConvertor : IBaseConvertor<Link, LinkDTO>
    {
        public Link DTOToEntity(LinkDTO obj)
        {
            throw new NotImplementedException();
        }

        public LinkDTO EntityToDTO(Link entity)
        {
            throw new NotImplementedException();
        }
    }
}
