using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReadABook.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int ReaderId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Summary { get; set; }

        [ForeignKey("ReaderId")]
        public User Reader { get; set; }
    }
}
