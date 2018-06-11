using System.Collections.Generic;
using System.Xml.Serialization;

namespace Shelf_Share.Models
{
    [XmlRoot("GoodreadsResponse")]
    public class GoodreadsResponse
    {
        //for xml deserialization
        [XmlElement("book")]
        public List<book> Books { get; set; }


    }

    public class book
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("image_url")]
        public string Image_url { get; set; }

        [XmlElement("num_pages")]
        public string Num_pages { get; set; }

        [XmlElement("average_rating")]
        public string Average_rating { get; set; }

        public Author Author { get; set; }

        [XmlElement("isbn13")]
        public string ISBN { get; set; }

        [XmlElement("authors")]
        public List<authors> Authors { get; set; }
    }

    public class authors
    {
        [XmlElement("author")]
        public List<author> Author { get; set; }
    }

    public class author
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }

}
