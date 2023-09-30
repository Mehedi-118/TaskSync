using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.Ovidhan.Common.Implementation
{
    public class PagedData<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public decimal TotalPage { get; set; }
        public decimal TotalRecord { get; set; }
        public IList<T> Data { get; set; } = new List<T>();
    }
}
