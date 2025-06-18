using System;
using System.Collections.Generic;
using System.Text;

namespace MasterProjectCommonUtility.Paging
{
    public class PagingParameter : IPagingParameter
    {
        const int maxPageSize = 10000;
        //public int? PageNumber { get; set; } = 1;

        const int maxPageNumber = 10000;
        private int? _pageNumber = 1;
        public int? PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = (value == 0 ? maxPageNumber : value);
            }
        }


        private int? _pageSize = 10;
        public int? PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize || value == 0) ? maxPageSize : value;
            }
        }
    }
}
