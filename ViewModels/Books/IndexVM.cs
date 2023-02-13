using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadABook.Entities;
using ReadABook.ViewModels.Shared;

namespace ReadABook.ViewModels.Books
{
    public class IndexVM
    {
        public List<Book> Books { get; set; }
        public List<Book> AllBooks { get; set; }
        public FilterVM Filter { get; set; }
        public PagerVM Pager { get; set; }
    }
}
