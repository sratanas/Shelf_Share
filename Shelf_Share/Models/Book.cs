namespace Shelf_Share.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public string ISBN { get; set; }
        public ApplicationUser User { get; set; }
        public bool IsOnUserShelf { get; set; }

    }
}
