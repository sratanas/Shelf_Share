using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Shelf_Share.Models.MyShelfViewModels
{
    public class UserSearchResultsViewModel
    {

        [FromQuery]
        public string email { get; set; }

        public ApplicationUser AppUser { get; set; }
        public bool IsFollowing { get; set; }
        public bool PendingFollowing { get; set; }
        public IEnumerable<ApplicationUser> PeopleIFollow { get; set; }
        public IEnumerable<ApplicationUser> PeoplePendingFollow { get; set; }

    }
}
