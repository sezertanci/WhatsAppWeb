using Common.Models.RequestModels.UserRequestModels;
using Microsoft.AspNetCore.Mvc;
using WebAppMvc.Infrastructure.Services.Interfaces;
using WebAppMvc.Models;

namespace WebAppMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserCommand loginUserCommand)
        {
            userService.Login(loginUserCommand);

            return RedirectToAction("", "");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserCommand createUserCommand)
        {
            var response = await userService.Register(createUserCommand);

            return Json(response);
        }
    }
}
