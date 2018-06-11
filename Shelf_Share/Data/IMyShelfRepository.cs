using Shelf_Share.Models;
using System.Collections.Generic;

namespace Shelf_Share.Data
{
    public interface IMyShelfRepository
    {
        List<Book> GetUserShelf(string userId);
        List<Book> GetBooksByAuthor(string authorName);
        List<Book> GetBooksByTitle(string title);
        Book GetBookById(int id);
        void AddBookToShelfShare(Book book);
        void AddBookToUserShelf(string userName, Book book);
    }
}