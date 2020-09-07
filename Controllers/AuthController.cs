using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreBlog.Models;
using NetCoreBlog.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreBlog.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<AppUser> signInManager;
        public AuthController(SignInManager<AppUser> SignInManager)
        {
            this.signInManager = SignInManager;
        }
        [HttpGet]
        public IActionResult Login() {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await this.signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
