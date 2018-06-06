using System.Collections.Generic;

namespace Shelf_Share.Models.MyShelfViewModels
{
    public class TitleBookSearchResultsViewModel
    {
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public IEnumerable<Book> BooksByTitle { get; set; }
    }
}
