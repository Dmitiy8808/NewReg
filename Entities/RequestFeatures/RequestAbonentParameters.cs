using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class RequestAbonentParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 15;
        public int PageSize
        {
            get
            {
                return _pageSize;       
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string? SearchTerm { get; set; } 
        public string OrderBy { get; set; } = "name";
    }
}