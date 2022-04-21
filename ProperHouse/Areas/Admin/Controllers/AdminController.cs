using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProperHouse.Core.Contracts;

namespace ProperHouse.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        
    }
}
