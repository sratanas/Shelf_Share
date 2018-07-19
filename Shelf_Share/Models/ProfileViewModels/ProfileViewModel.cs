using System.Collections.Generic;

namespace Shelf_Share.Models
{
    public class ProfileViewModel
    {
        public ApplicationUser AppUser { get; set; }
        public IEnumerable<Book> UserShelf { get; set; }
        public byte[] ProfilePicture { get; set; }
        
    }
}
