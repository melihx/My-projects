using ReadABook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadABook.ViewModels.Books
{
    public class ShareVM
    {
        public int BookId { get; set; }
        public List<int> ReaderIds { get; set; }

        public Book Book { get; set; }
        public List<BookToRead> Shares { get; set; }
        public List<User> Readers { get; set; }
    }
}
