using internPlatform.Application.Services.Mappings;
using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public class ImageEntityService : IImageEntityService
    {
        private readonly IRepository<Image> _repository;
        private readonly IBaseConvertor<Image, ImageDTO> _mapper;
        public ImageEntityService(IRepository<Image> repository, IBaseConvertor<Image, ImageDTO> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Add(string name)
        {
            var image = await _repository.Get(img => img.Name == name);
            if (image == null)
            {
                _repository.Add(
                    new Image
                    {
                        Name = name,
                        IsTemp = true,
                        TimeStamp = new TimeStamp(),
                        DisplayOrder = 0,
                    }
                );
                await Save();
            }
            return 0;
        }


        public async Task<ImageDTO> Get(Expression<Func<Image, bool>> filter, string includeProperties = null)
        {
            Image entity = await _repository.Get(filter, includeProperties);
            return _mapper.EntityToDTO(entity);
        }
        public List<ImageDTO> GetAll(Expression<Func<Image, bool>> filter, string includeProperties = null)
        {
            var listDTO = new List<ImageDTO>();
            IEnumerable<Image> entities = _repository.GetAll(filter, includeProperties);
            foreach (var item in entities)
            {
                listDTO.Add(_mapper.EntityToDTO(item));
            }
            return listDTO;
        }




        public async Task<bool> Remove(int Id)
        {
            Image entity = await _repository.GetById(Id);
            if (entity != null)
            {
                _repository.Remove(entity);
                return await Save();
            }
            return false;
        }

        public async Task<bool> RemoveRange(IEnumerable<int> IDs)
        {
            List<Image> images = new List<Image>();
            var results = _repository.GetAll(i => IDs.Contains(i.ImageId));
            if (results != null)
            {
                _repository.RemoveRange(results);
                return await Save();
            }
            return false;
        }

        public async Task<bool> Save()
        {
            int result = await _repository.Save();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> Update(ImageDTO img)
        {
            var image = await _repository.Get(f => img.Name == f.Name);
            if (image != null)
            {
                image.Name = img.Name;
                image.DisplayOrder = img.DisplayOrder;
                image.IsTemp = img.IsTemp;

                _repository.Update(image);
                await Save();
            }
            return 0;
        }

        public async Task<bool> UpdateImagesOrder(int[] ids, int eventId)
        {
            var images = _repository.GetAll(img => img.Event.EventId == eventId).ToList();
            foreach (var image in images)
            {
                int index = Array.IndexOf(ids, image.ImageId);
                if (index >= 0)
                {
                    image.DisplayOrder = index;
                    _repository.Update(image);
                    await _repository.Save();
                    return true;
                }

            }
            return false;
        }

        public async Task RemoveTempImages()
        {
            DateTime today = DateTime.Now;
            DateTime yesterday = today.AddDays(-1);

            var oldImgs = _repository.GetAll(img => img.IsTemp == true && img.TimeStamp.CreatedDate < yesterday);
            _repository.RemoveRange(oldImgs);
            await _repository.Save();
        }
    }
}
