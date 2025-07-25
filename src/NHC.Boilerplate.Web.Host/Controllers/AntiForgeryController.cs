using Abp.Web.Security.AntiForgery;
using NHC.Boilerplate.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace NHC.Boilerplate.Web.Host.Controllers
{
    public class AntiForgeryController : BoilerplateControllerBase
    {
        private readonly IAntiforgery _antiforgery;
        private readonly IAbpAntiForgeryManager _antiForgeryManager;

        public AntiForgeryController(IAntiforgery antiforgery, IAbpAntiForgeryManager antiForgeryManager)
        {
            _antiforgery = antiforgery;
            _antiForgeryManager = antiForgeryManager;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }

        public void SetCookie()
        {
            _antiForgeryManager.SetCookie(HttpContext);
        }
    }
}
