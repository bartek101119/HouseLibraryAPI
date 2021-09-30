using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibrary.Models
{
    public class UpdateBookDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string PlaceOfPublication { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public bool Borrowed { get; set; }
    }
}
