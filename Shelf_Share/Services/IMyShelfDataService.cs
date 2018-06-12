using Shelf_Share.Models;
using System.Collections.Generic;

namespace Shelf_Share.Services
{
    public interface IMyShelfDataService
    {
        IEnumerable<Book> GetUserShelf(string userId);
        IEnumerable<Book> GetBooksByAuthor(string authorName);
        IEnumerable<Book> GetBooksByTitle(string title);
        Book GetBookById(int id);
        void AddBookToShelfShare(Book book);
        void AddBookToUserShelf(string userName, Book book);
        void RemoveBookFromUserShelf(string userName, Book book);
    }
}