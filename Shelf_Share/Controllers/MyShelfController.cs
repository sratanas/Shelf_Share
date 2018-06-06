using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shelf_Share.Models;
using Shelf_Share.Models.MyShelfViewModels;
using Shelf_Share.Services;

namespace Shelf_Share.Controllers
{
    [Authorize]
    public class MyShelfController : Controller
    {

        private readonly IMyShelfDataService _myShelfDataService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly ApplicationUser _appUser;

        public MyShelfController(IMyShelfDataService myShelfDataService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _myShelfDataService = myShelfDataService;
            _userManager = userManager;
            _signInManager = signInManager;
           // _appUser = appUser;
        }
        public IActionResult Index(string userName)
        {

            var user = User.Identity.Name;
            
            var model = new IndexMyShelfViewModel();
            model.MyShelfBooks = _myShelfDataService.GetUserShelf(user);
            

            return View(model);
        }
    }
}