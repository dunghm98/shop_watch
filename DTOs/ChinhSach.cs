using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOs
{
    public class ChinhSach
    {
        string _noiDung;
        int _id, _chinhSachApDung, _shopWatch_id;

        public int ShopWatch_id
        {
            get { return _shopWatch_id; }
            set { _shopWatch_id = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int ChinhSachApDung
        {
            get { return _chinhSachApDung; }
            set { _chinhSachApDung = value; }
        }

        public string NoiDung
        {
            get { return _noiDung; }
            set { _noiDung = value; }
        }

        public ChinhSach() { this.Id = -1; }

        public ChinhSach(int _id, string _noiDung,int _chinhSachApDung, int _shopWatch_id) {
            this.Id = _id;
            this.NoiDung = _noiDung;
            this.ChinhSachApDung = _chinhSachApDung;
            this.ShopWatch_id = _shopWatch_id;
        }

    }
}
