using System;
using System.Threading.Tasks;

namespace Application.ServiceInterfaces
{
    public interface IHttpClientService : IDisposable
    {
        Task<string> GetAsync(string path, bool secure);
        Task<string> GetAsync(string path, bool secure, int aid);
        Task<string> GetAsync(string path, bool secure, int aid, int bid);
        Task<string> GetAsync(string path, bool secure, int aid, int bid, int cid, int? did);
        Task<string> GetAsync(string path, bool secure, string str1);
        Task<string> GetAsync(string path, bool secure, string str1, int aid);
        Task<string> GetAsync(string path, bool secure, int aid, string str1);
        Task<string> GetAsync(string path, bool secure, int aid, string str1, string str2);
        Task<string> GetAsync(string path, bool secure, string str1, string str2);
        Task<string> GetAsync(string path, bool secure, string str1, string str2, string str3, int count);
        Task<string> PutAsync(string path, bool secure, int id, object model);
        Task<string> PutAsync(string path, bool secure, string id, object model);
        Task<string> PostAsync(string path, bool secure, object model);
        Task<string> DeleteAsync(string path, bool secure, int id);
        Task<string> DeleteAsync(string path, bool secure, int id, bool hard);
        Task<string> DeleteAsync(string path, bool secure, string id);

        Task<string> GetStringAsync(string path);
        
    }
}
