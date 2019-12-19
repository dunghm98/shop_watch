using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOs
{
    public class ShopInfo
    {
        #region Field
        private int _id;
        private string _locate, _hotLine, _webSite, _email, _openTime, _openDates, _fb, _ytb, _maps, _reviewImg;

        public string ReviewImg
        {
            get { return _reviewImg; }
            set { _reviewImg = value; }
        }

        public string Maps
        {
            get { return _maps; }
            set { _maps = value; }
        }

        public string Ytb
        {
            get { return _ytb; }
            set { _ytb = value; }
        }

        public string Fb
        {
            get { return _fb; }
            set { _fb = value; }
        }

        public string OpenDates
        {
            get { return _openDates; }
            set { _openDates = value; }
        }

        public string OpenTime
        {
            get { return _openTime; }
            set { _openTime = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string WebSite
        {
            get { return _webSite; }
            set { _webSite = value; }
        }

        public string HotLine
        {
            get { return _hotLine; }
            set { _hotLine = value; }
        }

        public string Locate
        {
            get { return _locate; }
            set { _locate = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region Constructor
        public ShopInfo() {
            this.Id = -1;
        }

        public ShopInfo(int _id,string _locate,string _hotLine,string _webSite,string _email,string _openTime,string _openDates,string _fb,string _ytb,string _maps,string _reviewImg) {
            this.Id = _id;
            this.Locate = _locate;
            this.HotLine = _hotLine;
            this.WebSite = _webSite;
            this.Email = _email;
            this.OpenTime = _openTime;
            this.OpenDates = _openDates;
            this.Fb = _fb;
            this.Ytb = _ytb;
            this.Maps = _maps;
            this.ReviewImg = _reviewImg;
        }
        #endregion
    }
}
