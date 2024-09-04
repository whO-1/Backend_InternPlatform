using System.Collections.Generic;
using System.Web;

namespace internPlatform.Application.Services.FilesOperations
{
    public interface IFileBuffer
    {
        List<string> Buffer(IEnumerable<HttpPostedFileBase> files);
        string Buffer(HttpPostedFileBase file);
        void EmptyBuffer();
        void SaveFromBuffer(string fileName);
        void SaveFromBuffer(List<string> fileNames);
    }
}
