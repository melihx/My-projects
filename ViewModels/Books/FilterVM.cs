using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ReadABook.ViewModels.Books
{
    public class FilterVM
    {
        [DisplayName("Title: ")]
        public string Title { get; set; }

        [DisplayName("Author: ")]
        public string Author { get; set; }
    }
}
