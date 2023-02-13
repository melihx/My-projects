using ReadABook.Entities;
using ReadABook.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadABook.ViewModels.Users
{
    public class IndexVM
    {
        public List<User> Items { get; set; }
        public PagerVM Pager { get; internal set; }
        public FilterVM Filter { get; internal set; }
    }
}
