using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Application.Services.Mappings
{
    public class FaqConvertor : IBaseConvertor<Faq, FaqDTO>
    {
        public FaqConvertor() { }
        public Faq DTOToEntity(FaqDTO obj)
        {
            return new Faq
            {
                FaqId = obj.FaqId,
                Title = obj.Title,
                Description = obj.Description,
            };
        }
        public FaqDTO EntityToDTO(Faq entity)
        {
            return new FaqDTO
            {
                FaqId = entity.FaqId,
                Title = entity.Title,
                Description = entity.Description,
            };
        }
    }
}
