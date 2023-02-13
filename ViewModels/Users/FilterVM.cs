using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ReadABook.ViewModels.Users
{
    public class FilterVM
    {
        [DisplayName("Username: ")]
        public string Username { get; set; }

        [DisplayName("First Name: ")]
        public string FirstName { get; set; }

        [DisplayName("Last Name: ")]
        public string LastName { get; set; }
    }
}
