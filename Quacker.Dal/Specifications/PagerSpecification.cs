using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.Specifications
{
    public class PagerSpecification
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}