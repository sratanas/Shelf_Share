using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shelf_Share.Models.MyShelfViewModels
{
    public class TitleBookSearchResultsViewModel
    {
        public Book Book { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public IEnumerable<Book> BooksByTitle { get; set; }
        public Task<GoodreadsResponse> GoodreadsList { get; set; }

    }
}
