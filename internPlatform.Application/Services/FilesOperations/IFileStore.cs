using System.Collections.Generic;

namespace internPlatform.Application.Services.FilesOperations
{
    public interface IFileStore
    {
        void Copy(List<string> fileNames);
        void Copy(string fileName);
        void DeleteFiles(string fileNames);
        void DeleteFile(string fileName);
    }
}
