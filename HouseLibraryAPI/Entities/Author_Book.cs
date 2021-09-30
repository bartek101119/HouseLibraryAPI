using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibraryAPI.Entities
{
    public class Author_Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

    }
}
