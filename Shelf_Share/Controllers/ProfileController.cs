using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shelf_Share.Models;
using Shelf_Share.Services;

namespace Shelf_Share.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IMyShelfDataService _myShelfDataService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IGoodreadsService _goodreadsService;

        public ProfileController(IMyShelfDataService myShelfDataService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IGoodreadsService goodreadsService)
        {
            _myShelfDataService = myShelfDataService;
            _userManager = userManager;
            _signInManager = signInManager;
            _goodreadsService = goodreadsService;
        }


        [Route("/profile/{userName}")]
        public IActionResult Profile(string userName)
        {

            var model = new ProfileViewModel();
            model.AppUser = _myShelfDataService.GetUser(userName);
            model.UserShelf = _myShelfDataService.GetUserShelf(userName);

            return View(model);
        }

        public IActionResult UploadProfilePicture()
        {
            return RedirectToAction("");
        }

    }
}