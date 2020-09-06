using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebApp.Pages.CV
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientService _httpClient;
        public IndexModel(
            IHttpClientService httpClient
        )
        {
            _httpClient = httpClient;
        }
        [FromRoute] public string id { get; set; }

        public string BankName { get; set; }
        public string BranchName { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _httpClient.GetAsync("api/listbanks", false).ConfigureAwait(false);

            var banklist = !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<BankList>(result) : null;
            if (banklist == null || banklist.Data.Length <= 0)
            {
                TempData["Message"] = "danger^Bank not found";
                return Page();
            }
            var bankdetails = new List<Bank>();
            foreach (var item in banklist.Data)
            {
                var bnk = new Bank();
                bnk.Id = item.Trim();
                bnk.Name = item.Trim();

                bankdetails.Add(bnk);
            }

            ViewData["Bank"] = new SelectList(bankdetails, "Id", "Name");
            return Page();
        }
       
        public async Task<IActionResult> OnGetBranchList(string bankName)
        {
            var result = await _httpClient.GetAsync("api/listbranches/", false, bankName).ConfigureAwait(false);

            var banklist = !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<BankList>(result) : null;
            if (banklist == null || banklist.Data.Length <= 0)
            {
                TempData["Message"] = "danger^Branch not found";
                return Page();
            }
            var bankdetails = new List<Bank>();
            foreach (var item in banklist.Data)
            {
                var bnk = new Bank();
                bnk.Id = item.Trim();
                bnk.Name = item.Trim();

                bankdetails.Add(bnk);
            }
            return new JsonResult(bankdetails);
        }
        public async Task<IActionResult> OnGetIFSC(string bankName,string branchName)
        {
            var postModel = new PostBankBranch();
            postModel.bankName = bankName;
            postModel.branchName = branchName;
            var result = await _httpClient.PostAsync("api/bank/search/likeBranchName", false, postModel).ConfigureAwait(false);

            var ifsclist = !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<IFSCList>(result) : null;
            if (ifsclist == null || ifsclist.Data.Length <= 0)
            {
                TempData["Message"] = "danger^IFSC not found";
                return Page();
            }

            return new JsonResult(ifsclist.Data);
        }
    }
}