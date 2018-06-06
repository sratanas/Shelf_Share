using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelf_Share.Models.MyShelfViewModels
{
    public class IndexMyShelfViewModel
    {
        public string UserName { get; set; }

        public IEnumerable<Book> MyShelfBooks { get; set; }

    }
}
