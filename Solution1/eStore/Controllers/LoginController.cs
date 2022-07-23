using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BussinessObject.Models;
using System.Linq;

namespace eStore.Controllers
{
    public class LoginController : Controller
    {

        private readonly SalesManagementDBContext _context;

        public LoginController(SalesManagementDBContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult CheckLogin(string email, string password)
        {
            Member admin = JsonInteract.GetAdminAccount();
            if (admin.Email == email && admin.Password == password)
            {
                HttpContext.Session.SetString("role", "Admin");
                HttpContext.Session.SetString("email", "Admin");
                return RedirectToAction("Index", "Home");
            }
            Member loginUser = _context.Members.FirstOrDefault(m => m.Email == email && m.Password == password);
            if(loginUser.Email == email && loginUser.Password == password) 
            {
                HttpContext.Session.SetString("role", "User");
                HttpContext.Session.SetString("email", email);
                HttpContext.Session.SetInt32("id", loginUser.MemberId);
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            

        }
    }
}
