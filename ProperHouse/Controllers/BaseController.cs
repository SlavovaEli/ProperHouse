using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProperHouse.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
