using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shelf_Share.Models;
using Shelf_Share.Models.MyShelfViewModels;
using Shelf_Share.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelf_Share.Controllers
{
    [Authorize]
    public class MyShelfController : Controller
    {

        private readonly IMyShelfDataService _myShelfDataService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IGoodreadsService _goodreadsService;
        

        public MyShelfController(
            IMyShelfDataService myShelfDataService, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IGoodreadsService goodreadsService)
        {
            _myShelfDataService = myShelfDataService;
            _userManager = userManager;
            _signInManager = signInManager;
            _goodreadsService = goodreadsService;
        }

        [HttpGet]
        public IActionResult Index(string userName)
        {

            var user = User.Identity.Name;
            
            var model = new IndexMyShelfViewModel();
            model.MyShelfBooks = _myShelfDataService.GetUserShelf(user);
            model.PeopleIFollow = _myShelfDataService.GetUsersIFollow(user);
            model.PendingFollowRequests = _myShelfDataService.GetPendingFollowers(user);

            return View(model);
        }


        public IActionResult SearchBooks()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AuthorBookSearchResults(string searchType, string searchInput)
        {
            
            if (searchType == "Author")
            {
                var model = new AuthorBookSearchResultsViewModel();
                model.AuthorBookList = _myShelfDataService.GetBooksByAuthor(searchInput);
                
                return View("AuthorBookSearchResults", model);
                 
            }
            else if (searchType == "Title")
            {
                var model = new TitleBookSearchResultsViewModel();
                var model2 = new TitleBookSearchResultsViewModel();
                model.BooksByTitle = _myShelfDataService.GetBooksByTitle(searchInput);
                model2.GoodreadsList = _goodreadsService.GetBookBasedOnTitleInput(searchInput);

                if (model.BooksByTitle.Count() != 0)
                {
                    
                    return View("TitleBookSearchResults", model);
                }
                else
                {
                    return View("TitleBookSearchNOResults", model2);
                }
            }
            else
            {
                return Content("No results found");
            }

        }


        [HttpGet]
        [Route("/myshelf/bookdetails/{id}")]
        public IActionResult BookDetails(int id)
        {

            var model = new BookDetailsViewModel();
            var user = User.Identity.Name;

            model.Book = _myShelfDataService.GetBookById(id);

            model.GoodreadsList = _goodreadsService.GetBookBasedOnTitleInput(model.Book.Title);

            model.MyShelfBooks = _myShelfDataService.GetUserShelf(user);
            var templist = new List<int>();
            foreach (var book in model.MyShelfBooks)
            {
                templist.Add(book.Id);
            };

            if (templist.Contains(id))
            {
                model.Book.IsOnUserShelf = true;
            }

            return View(model);
        }



        [HttpPost]
        public IActionResult AddBook(TitleBookSearchResultsViewModel model)
        {
            var newBook = new Book();
            var author = new Author();
            //author = newBook.Author;
            if (ModelState.IsValid)
            {
                newBook.Title = model.Book.Title;
                newBook.Author = model.Book.Author;
                newBook.ISBN = model.Book.ISBN;
            }

            _myShelfDataService.AddBookToShelfShare(newBook);
            //replace this view with confirmation screen
            return RedirectToAction("AuthorBookSearchResults", new { searchType="Title", searchInput = model.Book.Title });
        }

        [HttpGet]
        [Route("goodreads/{searchInput}")]
        public async Task<IActionResult> GetBookInfo(string searchInput)
        {
            var result = await _goodreadsService.GetBookBasedOnTitleInput(searchInput);

            return Ok(result);
        }

        //For adding to junction table
        [HttpPost]
        public IActionResult AddBookToUserShelf(BookDetailsViewModel model)
        {

            var userName = User.Identity.Name;
            var book = new Book();
            if (ModelState.IsValid)
            {
                    book = model.Book;
                    _myShelfDataService.AddBookToUserShelf(userName, book);           
            }

            

            return RedirectToAction("BookDetails", new { id = model.Book.Id});
        }

        public IActionResult RemoveBookFromUserShelf(BookDetailsViewModel model)
        {
            var userName = User.Identity.Name;
           
            if (ModelState.IsValid)
            {           
                _myShelfDataService.RemoveBookFromUserShelf(userName, model.Book);
            }

            return RedirectToAction("BookDetails", new { id = model.Book.Id });
        }


        public IActionResult SearchUsers()
        {
            return View();
        }


        [HttpGet]
        public IActionResult UserSearchResults(string email)
        {
            var user = new ApplicationUser();
            var model = new UserSearchResultsViewModel();
            
            model.AppUser = _myShelfDataService.GetUser(email);
            model.PeopleIFollow = _myShelfDataService.GetUsersIFollow(User.Identity.Name);
            model.PeoplePendingFollow = _myShelfDataService.ListFollowsRequestedByUser(User.Identity.Name);

            var tempList = new List<string>(); //list of Ids of people I follow

            foreach (var person in model.PeopleIFollow)
            {
                tempList.Add(person.Id);
            }

            var tempList2 = new List<string>(); //list of Ids of people I requested to follow

            foreach (var person in model.PeoplePendingFollow)
            {
                tempList2.Add(person.Id);
            }

            if (tempList.Contains(model.AppUser.Id))
            {
                model.IsFollowing = true;
            }
            else if (tempList2.Contains(model.AppUser.Id))
            {
                model.PendingFollowing = true;
            }
            return View("UserSearchResults", model);
        }


        //Need to create users and followers junction table, temp junction table for pending requests
        public IActionResult RequestToFollow(UserSearchResultsViewModel model)
        {
            var follower = User.Identity.Name;
            var followee = new ApplicationUser();

            if (ModelState.IsValid)
            {
                followee.UserName = model.AppUser.UserName;
                _myShelfDataService.CreatePendingFollowRequest(follower, followee.UserName);
                model.PendingFollowing = true;
            }

            //need to figure out how to redirect to 
            return RedirectToAction("UserSearchResults", new { email = model.AppUser.Email });
        }

        //create this action 
        public IActionResult ConfirmFollower(IndexMyShelfViewModel model)
        {
            if (ModelState.IsValid)
            {
                var followee = User.Identity.Name;

                _myShelfDataService.ConfirmFollower(model.Person.UserName, followee);
                
            }

            return RedirectToAction("Index");
        }
    }
}