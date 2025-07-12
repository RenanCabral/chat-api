using ChatApp.Application.Services;
using ChatApp.DataContracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChatApp.Web.Controllers
{
    public class LoginController : Controller
    {
        private ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<HttpResponseMessage> LogInAsync([FromBody] User user)
        {
            try
            {
                await loginService.LogInAsync(user);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}