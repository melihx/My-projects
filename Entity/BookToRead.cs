using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReadABook.Entities
{
    public class BookToRead
    {
        public int Id { get; set; }
        public int ReaderId { get; set; }
        public int BookId { get; set; }

        [ForeignKey("ReaderId")]
        public virtual User Reader { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}
