using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBooks.Data.Entities
{
    public class Book_Author
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }


        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
