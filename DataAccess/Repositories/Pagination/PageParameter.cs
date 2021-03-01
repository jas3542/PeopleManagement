using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleManagement
{
    public class PageParameter
    {
        public int PageSize { get; set; } 
        public int CurrentPage { get; set; }
        public PageParameter()
        {
            PageSize = 10;
            CurrentPage = 1;
        }
    }


}
