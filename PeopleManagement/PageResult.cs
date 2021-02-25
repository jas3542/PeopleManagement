using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleManagement
{
    public class PageResult
    {
        public int TotalCount { get; set; } = 0;
        public int TotalPages { get; set; } = 1;
        public int? PreviousPage { get; set; }
        public int? NextPage { get; set; }
    }
}
