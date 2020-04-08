using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace S1E3.Pages
{
    public class IndexModel : PageModel
    {
        private IDistributedCache _distributedCache;
        public const string _cacheKey = "FirstAccessedTime";
        public IndexModel(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public void OnGet()
        {
            // Get the cache data.
            var firstAccessedTime = _distributedCache.GetString(_cacheKey);
            if (!string.IsNullOrEmpty(firstAccessedTime))
            {
                ViewData[_cacheKey] = firstAccessedTime;
            }
            else
            {
                firstAccessedTime = DateTime.UtcNow.ToLocalTime().ToString();
                _distributedCache.SetString(_cacheKey, firstAccessedTime);
                ViewData[_cacheKey] = firstAccessedTime;
            }
        }
    }
}