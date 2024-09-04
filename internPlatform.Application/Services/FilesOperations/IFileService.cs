using System.Collections.Generic;

namespace internPlatform.Application.Services.FilesOperations
{
    public interface IFileService : IFileValidate, IFileBuffer
    {
        string GetFilePath(string fileName);
        string GetFileExt(string filePath);
        void DeleteFile(string filePath);
        void DeleteFiles(List<string> fileNames);
        byte[] GetFileAsBytes(string filePath);
        string ConvertBytesToBase64(byte[] fileBytes);
    }
}
