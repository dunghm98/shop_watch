using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOs
{
    public class ShopWatch
    {
        #region Field
        string _baoHiem, _thamDinh, _giaoHang, _thoiGianBaoHanh;

        int _id, _baoHanh;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        public int BaoHanh
        {
            get { return _baoHanh; }
            set { _baoHanh = value; }
        }

        public string ThoiGianBaoHanh
        {
            get { return _thoiGianBaoHanh; }
            set { _thoiGianBaoHanh = value; }
        }

        public string GiaoHang
        {
            get { return _giaoHang; }
            set { _giaoHang = value; }
        }

        public string ThamDinh
        {
            get { return _thamDinh; }
            set { _thamDinh = value; }
        }

        public string BaoHiem
        {
            get { return _baoHiem; }
            set { _baoHiem = value; }
        }
        #endregion
        #region Constructor
        public ShopWatch() { this.Id = -1; }
        public ShopWatch(int _id, string _baoHiem, int _baoHanh, string _thamDinh, string _giaoHang, string _thoiGianBaoHanh)
        {
            this.Id = _id;
            this.BaoHiem = _baoHiem;
            this.BaoHanh = _baoHanh;
            this.ThamDinh = _thamDinh;
            this.GiaoHang = _giaoHang;
            this.ThoiGianBaoHanh = _thoiGianBaoHanh;
        }
        #endregion
    }
}
