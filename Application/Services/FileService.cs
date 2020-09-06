using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class FileService : IFileService
    {
        //private enum ImageFormat { jpeg, jpeg2, png, bmp, gif, tiff, tiff2, pdf, unknown }

        private static int AllowedFileSize;
        private static List<string> AllowedContentTypes;
        private static List<string> AllowedFileExtensions;

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;
        public FileService(IHostingEnvironment hostingEnvironment,
            IConfiguration config)
        {
            _hostingEnvironment = hostingEnvironment;
            _config = config;
            AllowedContentTypes = _config.GetSection("AllowedContentTypes").GetChildren().Select(x => x.Value).ToList();
            AllowedFileExtensions = _config.GetSection("AllowedFileExtensions").GetChildren().Select(x => x.Value).ToList();
            AllowedFileSize = Convert.ToInt32(_config["AllowedFileSize"]);
        }

      
        public bool CheckIfImageFile(IFormFile file)
        {
            //Check the image mime types
            if (!AllowedContentTypes.Any(x => x.Equals(file.ContentType, StringComparison.InvariantCultureIgnoreCase)))
                return false;

            //  Check the image extension
            var fileExtension = Path.GetExtension(file.FileName);
            if (!AllowedFileExtensions.Any(x => x.Equals(fileExtension, StringComparison.InvariantCultureIgnoreCase)))
                return false;

            try
            {
                //Attempt to read the file and check the first bytes
                if (!file.OpenReadStream().CanRead) return false;

                //Check whether the image size exceeding the limit or not
                if (file.Length > AllowedFileSize) return false;

                var buffer = new byte[AllowedFileSize];
                file.OpenReadStream().Read(buffer, 0, AllowedFileSize);
                var content = Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool CheckIfImageFileList(List<IFormFile> files)
        {

            foreach (var file in files)
            {


                //Check the image mime types
                if (!AllowedContentTypes.Any(x => x.Equals(file.ContentType, StringComparison.InvariantCultureIgnoreCase)))
                    return false;

                //  Check the image extension
                var fileExtension = Path.GetExtension(file.FileName);
                if (!AllowedFileExtensions.Any(x => x.Equals(fileExtension, StringComparison.InvariantCultureIgnoreCase)))
                    return false;

                try
                {
                    //Attempt to read the file and check the first bytes
                    if (!file.OpenReadStream().CanRead) return false;

                    //Check whether the image size exceeding the limit or not
                    if (file.Length > AllowedFileSize) return false;

                    var buffer = new byte[AllowedFileSize];
                    file.OpenReadStream().Read(buffer, 0, AllowedFileSize);
                    var content = Encoding.UTF8.GetString(buffer);
                    if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<string> SaveFileAsync(string path, IFormFile file)
        {
            //check to see if folder exists if not create it
            FolderCreator(path);
            var filename = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
            path = $"{_hostingEnvironment.WebRootPath}{path}\\{filename}";
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream).ConfigureAwait(false);
            }
            return filename;
        }

        public async Task<List<string>> SaveFileAsync(string path, List<IFormFile> files)
        {
            var filenames = new List<string>(files.Count);
            foreach (var file in files)
                filenames.Add(await SaveFileAsync(path, file).ConfigureAwait(false));
            return filenames;
        }

        private void FolderCreator(string path)
        {
            var path_to_check = _hostingEnvironment.WebRootPath;
            var folders = path.Split(@"\");
            for (var i = 0; i < folders.Length; i++)
            {
                path_to_check += folders[i];
                if (!Directory.Exists(path_to_check))
                    Directory.CreateDirectory(path_to_check);
                path_to_check += @"\";
            }
        }

        public void DeleteFile(string path)
        {
            var folderOK = true;
            //check to see if folder exists
            var path_to_check = _hostingEnvironment.WebRootPath;
            var folders = path.Split(@"\");
            for (var i = 0; i < folders.Length - 1; i++)
            {
                path_to_check += folders[i];
                if (!Directory.Exists(path_to_check))
                    folderOK = false;
                path_to_check += @"\";
            }
            if (folderOK)
                File.Delete($"{_hostingEnvironment.WebRootPath}{path}");
        }

        public byte[] Download(string path, string filename)
        {
            var filepath = $"{_hostingEnvironment.WebRootPath}{path}{filename}";
            var fileBytes = File.ReadAllBytes(filepath);
            return fileBytes;
        }
    }
}
