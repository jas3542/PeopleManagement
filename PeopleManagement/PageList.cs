using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PeopleManagement
{
    public class PageList<t> : List<t>
    {
        public PageParameter _PageParameter { get; }
        public PageResult _PageResult{ get; }

        public PageList(PageParameter param)
        {
            _PageParameter = param;
            _PageResult = new PageResult();
        }

        public async Task GetData(IQueryable<t> query)
        {
            // total items count: 
            _PageResult.TotalCount = await query.CountAsync();

            // total number of pages: 
            _PageResult.TotalPages = (int)Math.Ceiling(_PageResult.TotalCount / (double)_PageParameter.PageSize);

            // previous and next page number: 
            if (_PageParameter.CurrentPage - 1 > 0)
                _PageResult.PreviousPage = _PageParameter.CurrentPage - 1;
            if (_PageParameter.CurrentPage + 1 <= _PageResult.TotalPages)
                _PageResult.NextPage = _PageParameter.CurrentPage + 1;
            
            List<t> list = await query.Skip((_PageParameter.CurrentPage - 1) * _PageParameter.PageSize).Take(_PageParameter.PageSize).ToListAsync();
            
            AddRange(list);  
        }
    }
}
