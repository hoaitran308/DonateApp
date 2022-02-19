using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace DonateApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IMemoryCache cache;
        public int moneyInSession, moneyInCache;

        public IndexModel(ILogger<IndexModel> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            cache = memoryCache;
        }

        public void OnGet()
        {
            GetMoney();
        }

        public void OnPost()
        {
            GetMoney();
            SetMoney();
            GetMoney();
        }

        public void SetMoney()
        {
            HttpContext.Session.SetInt32("Donation", ++moneyInSession);
            cache.Set("Donation", ++moneyInCache);
        }

        public void GetMoney()
        {
            int? totalDonationInSession = HttpContext.Session.GetInt32("Donation");
            moneyInSession = totalDonationInSession ?? 0;

            cache.TryGetValue("Donation", out moneyInCache);
        }
    }
}