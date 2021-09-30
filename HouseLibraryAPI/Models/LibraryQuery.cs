using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibrary.Models
{
    public class LibraryQuery
    {
        public string SearchPhrase { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
