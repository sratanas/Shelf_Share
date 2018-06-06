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
       

    }
}
