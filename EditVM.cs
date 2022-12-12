using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReadABook.ViewModels.Books
{
    public class EditVM
    {
        public int Id { get; set; }
        public int ReaderId { get; set; }

        [DisplayName("Title: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Title { get; set; }

        [DisplayName("Author: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Author { get; set; }

        [DisplayName("Description: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Summary { get; set; }
    }
}
