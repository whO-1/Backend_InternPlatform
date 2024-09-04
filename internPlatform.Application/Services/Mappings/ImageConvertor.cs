using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;

namespace internPlatform.Application.Services.Mappings
{
    public class ImageConvertor : IBaseConvertor<Image, ImageDTO>
    {
        public ImageConvertor() { }
        public Image DTOToEntity(ImageDTO obj)
        {
            return new Image
            {
                ImageId = obj.Id,
                Name = obj.Name,
                IsTemp = obj.IsTemp,
                IsMain = obj.IsMain,
                DisplayOrder = obj.DisplayOrder,
                TimeStamp = obj.TimeStamp,
            };
        }

        public ImageDTO EntityToDTO(Image entity)
        {
            return new ImageDTO
            {
                Id = entity.ImageId,
                Name = entity.Name,
                IsTemp = entity.IsTemp,
                IsMain = entity.IsMain,
                DisplayOrder = entity.DisplayOrder,
                TimeStamp = entity.TimeStamp,
            };
        }
    }
}
