using Shelf_Share.Data;
using Shelf_Share.Models;
using System.Collections.Generic;

namespace Shelf_Share.Services
{
    public class MyShelfDataService : IMyShelfDataService
    {

        private readonly IMyShelfRepository _myShelfRepository;

        public MyShelfDataService(IMyShelfRepository myShelfRepository)
        {
            _myShelfRepository = myShelfRepository;
        }

        public IEnumerable<Book> GetUserShelf(string userId) => _myShelfRepository.GetUserShelf(userId);

        public IEnumerable<Book> GetBooksByAuthor(string authorName) => _myShelfRepository.GetBooksByAuthor(authorName);

        public IEnumerable<Book> GetBooksByTitle(string title) => _myShelfRepository.GetBooksByTitle(title);

        public Book GetBookById(int id) => _myShelfRepository.GetBookById(id);

        public void AddBookToShelfShare(Book book) => _myShelfRepository.AddBookToShelfShare(book);

        public void AddBookToUserShelf(string userName, Book book) => _myShelfRepository.AddBookToUserShelf(userName, book);

        public void RemoveBookFromUserShelf(string userName, Book book) => _myShelfRepository.RemoveBookFromUserShelf(userName, book);

        public ApplicationUser GetUser(string email) => _myShelfRepository.GetUser(email);

        public void CreatePendingFollowRequest(string followerUserName, string followeeUserName) => _myShelfRepository.CreatePendingFollowRequest(followerUserName, followeeUserName);

        public void ConfirmFollower(string followerUserName, string followeeUserName) => _myShelfRepository.ConfirmFollower(followerUserName, followeeUserName);

        public List<ApplicationUser> GetUsersIFollow(string userName) => _myShelfRepository.GetUsersIFollow(userName);

        public List<ApplicationUser> GetPendingFollowers(string userName) => _myShelfRepository.GetPendingFollowers(userName);

        public List<ApplicationUser> ListFollowsRequestedByUser(string userName) => _myShelfRepository.ListFollowsRequestedByUser(userName);

        public void UploadProfilePicture(byte[] picture, string userName) => _myShelfRepository.UploadProfilePicture(picture, userName);
    }
}
