using internPlatform.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace internPlatform.Application.Services.FilesOperations
{
    public class FileService : IFileService
    {
        private readonly string _rootPath;
        private readonly string _tempFolder;
        private readonly string _repositoryFolder;
        private readonly string _thumbnailsFolder;
        private readonly int _maxFileSize;
        private readonly string[] _validImageTypes;

        public FileService(string tempFolder, string fullsizeFolder, string thumbnailsFolder, int maxFileSize, string[] validFileTypes, string rootPath)
        {
            _maxFileSize = maxFileSize;
            _tempFolder = tempFolder;
            _repositoryFolder = fullsizeFolder;
            _thumbnailsFolder = thumbnailsFolder;
            _validImageTypes = validFileTypes;
            _rootPath = rootPath;

        }
        public FileService()
        {
            _maxFileSize = Constants.MaxImageSize;
            _tempFolder = Constants.TempFolder;
            _repositoryFolder = Constants.FullSizeFolder;
            _thumbnailsFolder = Constants.ThumbnailsFolder;
            _validImageTypes = Constants.ValidImageTypes;
            _rootPath = Constants.RootPath;
        }


        public List<string> Validate(IEnumerable<HttpPostedFileBase> files)
        {
            var errors = new List<string>();
            if (files != null)
            {
                foreach (var file in files)
                {
                    errors.AddRange(Validate(file));
                }
            }
            return errors;
        }
        public List<string> Validate(HttpPostedFileBase file)
        {
            var errors = new List<string>();
            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentLength / 1000 >= _maxFileSize)
                {
                    errors.Add($"File {file.FileName} is too big.");
                }
                if (!_validImageTypes.Contains(file.ContentType))
                {
                    errors.Add($"{file.ContentType} is not valid type");
                }
            }
            return errors;
        }




        public List<string> Buffer(IEnumerable<HttpPostedFileBase> files)
        {
            var savedFiles = new List<string>();
            if (files != null)
            {
                var errors = Validate(files);
                if (errors.Count() > 0)
                {
                    throw new Exception("Validation failed: " + string.Join(", ", errors));
                }
                else
                {
                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(_rootPath, _tempFolder, fileName);

                            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                            file.SaveAs(filePath);
                            savedFiles.Add(fileName);

                            //var thumbnailPath = Path.Combine(_rootPath, _thumbnailsFolder);
                            //var thumbnailFilePath = Path.Combine(thumbnailPath, fileName);
                            //var result = Directory.CreateDirectory(thumbnailPath);
                            //ImageHelper.CreateThumbnail(filePath, thumbnailFilePath, 50, 50);
                        }
                    }
                }
            }
            return savedFiles;
        }

        public string Buffer(HttpPostedFileBase file)
        {
            string savedFile = String.Empty;
            if (file != null)
            {
                var errors = Validate(file);
                if (errors.Count() > 0)
                {
                    throw new Exception("Validation failed: " + string.Join(", ", errors));
                }
                else
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(_rootPath, _tempFolder, fileName);
                    var fileExt = GetFileExt(filePath);
                    fileName = (Guid.NewGuid().ToString() + $".{fileExt}");
                    filePath = Path.Combine(_rootPath, _tempFolder, fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    file.SaveAs(filePath);
                    savedFile = fileName;

                }
            }
            return savedFile;
        }


        public void SaveFromBuffer(string fileName)
        {
            var tempFilePath = Path.Combine(_rootPath, _tempFolder, fileName);
            var destinationFilePath = Path.Combine(_rootPath, _repositoryFolder, fileName);
            if (File.Exists(tempFilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destinationFilePath));
                File.Move(tempFilePath, destinationFilePath);
            }
        }
        public void SaveFromBuffer(List<string> fileNames)
        {
            if (fileNames != null && fileNames.Count > 0)
            {
                fileNames.ForEach(fileName => SaveFromBuffer(fileName));
            }
        }

        public string GetFilePath(string fileName)
        {
            var filePath = Path.Combine(_rootPath, _repositoryFolder, fileName);
            if (File.Exists(filePath))
            {
                return filePath;
            }
            return String.Empty;
        }

        public string GetFileExt(string filePath)
        {
            return Path.GetExtension(filePath).TrimStart('.');
        }


        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public void DeleteFiles(List<string> fileNames)
        {
            fileNames.ForEach(f => DeleteFile(GetFilePath(f)));
        }

        public void EmptyBuffer()
        {
            var pathBuffer = Path.Combine(_rootPath, _tempFolder);

            if (Directory.Exists(pathBuffer))
            {
                var files = Directory.GetFiles(pathBuffer);

                var now = DateTime.Now;
                var cutoffTime = now.AddMinutes(-10); // 10 minutes ago

                foreach (var file in files)
                {
                    try
                    {
                        var fileCreationTime = File.GetCreationTime(file);

                        if (fileCreationTime < cutoffTime)
                        {
                            File.Delete(file);
                            Debug.WriteLine($"Deleted file: {file}");
                        }
                        else
                        {
                            Debug.WriteLine($"File {file} is either locked or not old enough for deletion.");
                        }
                    }
                    catch (IOException ex)
                    {
                        Debug.WriteLine($"Error deleting file {file}: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Unexpected error deleting file {file}: {ex.Message}");
                    }
                }

                Debug.WriteLine("Buffer emptied.");
            }

        }

        public byte[] GetFileAsBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
        public string ConvertBytesToBase64(byte[] fileBytes)
        {
            return Convert.ToBase64String(fileBytes);
        }
    }
}
