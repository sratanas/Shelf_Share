using Shelf_Share.Models;
using System.Collections.Generic;

namespace Shelf_Share.Services
{
    public interface IMyShelfDataService
    {
        IEnumerable<Book> GetUserShelf(string userId);

        ApplicationUser CurrentUser { get; set; }

    }
}