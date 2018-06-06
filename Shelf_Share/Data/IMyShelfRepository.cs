using System.Collections.Generic;
using Shelf_Share.Models;

namespace Shelf_Share.Data
{
    public interface IMyShelfRepository
    {
        List<Book> GetUserShelf(string userId);
        List<Book> GetBooksByAuthor(string authorName);
    }
}