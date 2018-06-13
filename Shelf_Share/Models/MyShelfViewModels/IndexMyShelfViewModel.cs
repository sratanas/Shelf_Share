using System.Collections.Generic;

namespace Shelf_Share.Models.MyShelfViewModels
{
    public class IndexMyShelfViewModel
    {
        public string UserName { get; set; }

        public IEnumerable<Book> MyShelfBooks { get; set; }

        public IEnumerable<ApplicationUser> PeopleIFollow { get; set; }

        public IEnumerable<ApplicationUser> PendingFollowRequests { get; set; }

        public ApplicationUser Person { get; set; }

    }
}
