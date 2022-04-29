using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProperHouse.Core.Contracts;
using ProperHouse.Core.Models;
using ProperHouse.Models;
using System.Diagnostics;

using static ProperHouse.Core.Constants.WebConstants;

namespace ProperHouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IMemoryCache cache;

        public HomeController(IPropertyService _propertyService,
            IMemoryCache _cache)
        {
            propertyService = _propertyService;
            cache = _cache;
        }

        public IActionResult Index()
        {
            var latestProperties = cache.Get<List<PropertyListingViewModel>>(LatestPropertiesCacheKey);

            if(latestProperties == null)
            {
                latestProperties = propertyService.GetAllProperties()
                .OrderByDescending(p => p.Id)
                .Take(3)
                .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                cache.Set(LatestPropertiesCacheKey, latestProperties, cacheOptions);
            }            

            return View(latestProperties);
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}