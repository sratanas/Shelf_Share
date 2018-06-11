using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Shelf_Share.Models.MyShelfViewModels
{
    [Authorize]
    public class AuthorBookSearchResultsViewModel
    {
        public Author Author { get; set; }

        [FromQuery]
        public string SearchType { get; set; }

        [FromQuery]
    
        public string SearchInput { get; set; }

        public IEnumerable<Book> AuthorBookList { get; set; }

    }
}
