using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibraryAPI.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string PlaceOfPublication { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public bool Borrowed { get; set; }
        public List<Author_Book> Author_Books { get; set; }
    }
}
