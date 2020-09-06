using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionary _TempData;

        public HttpClientService(
            IConfiguration config,
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _config = config;
            httpClient.BaseAddress = new Uri(_config["BaseUrl"]);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _TempData = tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);
        }

        public async Task<string> GetAsync(string path, bool addAuthHeader)
        {
            if (addAuthHeader == false)
                return await GetResponse(path).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }
        public async Task<string> GetStringAsync(string path)
        {

            return await GetResponse(path).ConfigureAwait(false);

        }
        public async Task<string> GetAsync(string path, bool addAuthHeader, int aid)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, aid).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path, aid).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }
        private async Task<bool> AddAuthHeader()
        {
            return false;
        }
        public async Task<string> GetAsync(string path, bool addAuthHeader, int aid, int bid)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, aid, bid).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path, aid, bid).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> GetAsync(string path, bool addAuthHeader, int aid, int bid, int cid, int? did)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, aid, bid, cid, did).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path, aid, bid, cid, did).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> GetAsync(string path, bool addAuthHeader, string str1)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, str1).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path, str1).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> GetAsync(string path, bool addAuthHeader, string str1, int aid)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, str1, aid).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path, str1, aid).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> GetAsync(string path, bool addAuthHeader, int aid, string str1)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, aid, str1).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path, aid, str1).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> GetAsync(string path, bool addAuthHeader, int aid, string str1, string str2)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, aid, str1, str2).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path, aid, str1, str2).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> GetAsync(string path, bool addAuthHeader, string str1, string str2)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, str1, str2).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path, str1, str2).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> GetAsync(string path, bool addAuthHeader, string str1, string str2, string str3, int count)
        {
            if (addAuthHeader == false)
                return await GetResponse(path, str1, str2, str3, count).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await GetResponse(path, str1, str2, str3, count).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> PutAsync(string path, bool addAuthHeader, int id, object model)
        {
            var content = GenerateContent(model);
            if (addAuthHeader == false)
                return await PutResponse(path, id, content).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await PutResponse(path, id, content).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> PutAsync(string path, bool addAuthHeader, string id, object model)
        {
            var content = GenerateContent(model);
            if (addAuthHeader == false)
                return await PutResponse(path, id, content).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await PutResponse(path, id, content).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> PostAsync(string path, bool addAuthHeader, object model)
        {
            var content = GenerateContent(model);
            if (addAuthHeader == false)
                return await PostResponse(path, content).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await PostResponse(path, content).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> DeleteAsync(string path, bool addAuthHeader, int id)
        {
            if (addAuthHeader == false)
                return await DeleteResponse(path, id).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await DeleteResponse(path, id).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> DeleteAsync(string path, bool addAuthHeader, int id, bool hard)
        {
            if (addAuthHeader == false)
                return await DeleteResponse(path, id, hard).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await DeleteResponse(path, id, hard).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

        public async Task<string> DeleteAsync(string path, bool addAuthHeader, string id)
        {
            if (addAuthHeader == false)
                return await DeleteResponse(path, id).ConfigureAwait(false);

            var authHeaderAdded = await AddAuthHeader().ConfigureAwait(false);
            if (authHeaderAdded)
                return await DeleteResponse(path, id).ConfigureAwait(false);

            return HandleResult("warning", "Please login or register to access this resource.", "unauthorized");
        }

       
        
        private void ClearSessionCookies()
        {
            //clear the session data
            _httpContextAccessor.HttpContext.Session.Clear();

            //delete the session cookie from client  (browser)
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".SSS.Session");

            //remove the token cookie from response
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".SSS.AuthToken");

            //remove the refresh token cookie from response
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".SSS.RefreshToken");

            //delete the token cookie from client (browser)
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                Domain = _config["Domain"]
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(".AspNetCore.AuthToken", "token", options);
        }
        private async Task<string> ResponseHandler(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.InternalServerError)
                return HandleResult("danger", "An internal error occured. Please try again later.", null);

            var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
                return result;

            return HandleResult("info", result, null);
        }
        private StringContent GenerateContent(object model)
        {
            var jsonContent = JsonConvert.SerializeObject(model, Formatting.Indented);
            var content = new StringContent(jsonContent);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        private async Task<string> GetResponse(string path)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> GetResponse(string path, int aid)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}/{aid}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> GetResponse(string path, int aid, int bid)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}/{aid}/{bid}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> GetResponse(string path, int aid, int bid, int cid, int? did)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}/{aid}/{bid}/{cid}/{did}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> GetResponse(string path, string str1)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}/{str1}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> GetResponse(string path, string str1, int aid)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}/{str1}/{aid}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> GetResponse(string path, int aid, string str1)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}/{aid}/{str1}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> GetResponse(string path, int aid, string str1, string str2)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}/{aid}/{str1}/{str2}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> GetResponse(string path, string str1, string str2)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}/{str1}/{str2}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> GetResponse(string path, string str1, string str2, string str3, int count)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/{path}/{str1}/{str2}/{str3}/{count}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> PutResponse(string path, int id, StringContent content)
        {
            try
            {
                var response = await _httpClient.PutAsync($"/{path}/{id}", content).ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> PutResponse(string path, string id, StringContent content)
        {
            try
            {
                var response = await _httpClient.PutAsync($"/{path}/{id}", content).ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> PostResponse(string path, StringContent content)
        {
            try
            {
                var response = await _httpClient.PostAsync($"/{path}", content).ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> DeleteResponse(string path, int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/{path}/{id}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> DeleteResponse(string path, int id, bool hard)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/{path}/{id}/{hard}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private async Task<string> DeleteResponse(string path, string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/{path}/{id}").ConfigureAwait(false);
                return await ResponseHandler(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return HandleResult("danger", ex.Message, null);
            }
        }

        private string HandleResult(string alertclass, string message, string result)
        {
            SetTempMessage(alertclass, message);
            return result;
        }

        private void SetTempMessage(string alertclass, string message)
        {
            if (!string.IsNullOrEmpty(alertclass) && !string.IsNullOrEmpty(message))
                _TempData["Message"] = $"{alertclass}^{message}";
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
