using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleManagement
{
    public class PageParameter
    {
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public PageParameter()
        {
        }
    }


}
