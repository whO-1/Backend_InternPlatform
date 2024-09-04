using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public interface IImageEntityService
    {
        Task<int> Update(ImageDTO img);
        Task<bool> UpdateImagesOrder(int[] ids, int eventId);
        Task<int> Add(string name);
        Task<ImageDTO> Get(Expression<Func<Image, bool>> filter, string includeProperties = null);
        List<ImageDTO> GetAll(Expression<Func<Image, bool>> filter, string includeProperties = null);
        Task<bool> Remove(int Id);
        Task<bool> RemoveRange(IEnumerable<int> IDs);
        Task<bool> Save();
        Task RemoveTempImages();

    }
}
