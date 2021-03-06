﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shelf_Share.Models.MyShelfViewModels
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }
        public Task<GoodreadsResponse> GoodreadsList { get; set; }
        public IEnumerable<Book> MyShelfBooks { get; set; }


    }
}
