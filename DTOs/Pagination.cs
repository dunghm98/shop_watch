using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOs
{
    public class Pagination
    {
        public int totalPage { get; set; }
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public Pagination()
        {
            this.currentPage = 1;
        }
        public Pagination(int _totalPage, int _currentPage, int _pageSize)
        {
            this.totalPage = _totalPage;
            this.currentPage = _currentPage;
            this.pageSize = _pageSize;
        }
    }
}
