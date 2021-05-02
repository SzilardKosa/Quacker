using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.Dto
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Results { get; set; }

        public int AllResultsCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}