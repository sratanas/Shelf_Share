using Microsoft.Extensions.Configuration;
using Shelf_Share.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Shelf_Share.Data
{
    public class GoodreadsRepository : IGoodreadsRepository
    {
        private readonly IConfiguration _config;

        public GoodreadsRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<GoodreadsResponse> ReturnBookBasedOnTitle(string searchInput)
        {
            string goodreadsKey = _config["GoodreadsApiKey"];
            //string goodreadsKey = Environment.ExpandEnvironmentVariables("GOODREADS_API");

            using (var client = new HttpClient())
            {
                var url = new Uri($"https://www.goodreads.com/book/title.xml?key={goodreadsKey}&title={searchInput}");

                var response = await client.GetAsync(url);

                string xml;

                using (var content = response.Content)
                {
                    xml = await content.ReadAsStringAsync();
                };



                GoodreadsResponse myObject;
                // Construct an instance of the XmlSerializer with the type  
                // of object that is being deserialized.  
                XmlSerializer mySerializer =
                new XmlSerializer(typeof(GoodreadsResponse));
                // To read the file, create a FileStream.  
                TextReader sr = new StringReader(xml);
                // FileStream myFileStream =new FileStream(xml, );
                // Call the Deserialize method and cast to the object type.  
                myObject = (GoodreadsResponse)mySerializer.Deserialize(sr);

                return myObject;
            };
        }
    }
}
