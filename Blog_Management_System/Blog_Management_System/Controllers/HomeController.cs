using Blog_Management_System.Areas.Identity.Data;
using Blog_Management_System.Models;
using BlogManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace Blog_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Application_user> _userManager;
        IUserRepository _userRepository;
        IBlogRepository _blogRepository;

        public HomeController(ILogger<HomeController> logger,UserManager<Application_user> userManager,IUserRepository userRepository,IBlogRepository blogRepository)
        {
            _logger = logger;
            this._userManager = userManager;
            this._userRepository = userRepository;
            this._blogRepository = blogRepository;
        }

        public IActionResult Index1()
        {
          
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            ViewData["UserID"]=_userManager.GetUserId(this.User);
            var user = _userManager.GetUserAsync(User).Result;
            if(user == null)
            {

                //return RedirectToAction("IdentityAccountRegister");
                return Redirect("https://localhost:7173/Identity/Account/Login");
            }
            Customer c1 = _userRepository.GetByEmail(ViewData["UserId"].ToString());
            int id = 1;
            if (c1 == null)
            {
                Customer c = new Customer();
                c.FirstName = user.FName;
                c.LastName = user.LastName;
                
                c.Phone = user.Phone;
                c.Password = " ";
                c.Address = " ";
                c.Email = _userManager.GetUserId(this.User);
                _userRepository.Create(c);
                Customer temp = _userRepository.GetByEmail(ViewData["UserId"].ToString());
                id = temp.Id;
            }
            else
            {
                id = c1.Id;
            }
            HttpContext.Session.SetInt32("UserId", id);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}