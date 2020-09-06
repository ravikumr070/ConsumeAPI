using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.ServiceInterfaces
{
    public interface IFileService
    {
        bool CheckIfImageFile(IFormFile file);

        bool CheckIfImageFileList(List<IFormFile> files);
        Task<string> SaveFileAsync(string path, IFormFile file);
        Task<List<string>> SaveFileAsync(string path, List<IFormFile> files);
        void DeleteFile(string path);
    }
}