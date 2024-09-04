using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Application.Services.Mappings
{
    public class LinkConvertor : IBaseConvertor<Link, LinkDTO>
    {
        public LinkConvertor() { }
        public Link DTOToEntity(LinkDTO obj)
        {
            if (obj != null)
            {
                return new Link
                {
                    Id = obj.Id,
                    LinkTitle = obj.LinkTitle,
                    LinkUrl = obj.LinkUrl,
                    DisplayOrder = obj.DisplayOrder,
                    HeadId = obj.HeadId,
                };
            }
            return null;
        }

        public LinkDTO EntityToDTO(Link entity)
        {
            if (entity != null)
            {
                return new LinkDTO
                {
                    Id = entity.Id,
                    LinkTitle = entity.LinkTitle,
                    LinkUrl = entity.LinkUrl,
                    DisplayOrder = entity.DisplayOrder,
                    HeadId = entity.HeadId,
                };
            }
            return null;
        }
    }
}
