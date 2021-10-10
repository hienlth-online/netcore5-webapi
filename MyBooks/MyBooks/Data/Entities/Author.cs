﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBooks.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        //Navigations Properties
        public List<Book_Author> Book_Authors { get; set; }
    }
}
