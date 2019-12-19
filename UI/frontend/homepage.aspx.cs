using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTOs;
using BUSs;

namespace UI.frontend
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public List<Product> lstCasioProducts = new List<Product>();
        public List<Product> lstSeikoProducts = new List<Product>();
        public List<Product> lstOrientProducts = new List<Product>();
        public List<Product> lstOpProducts = new List<Product>();
        public List<Product> lstCitizenProducts = new List<Product>();
        BUS bus = new BUS();
        Madu_BUS mBus = new Madu_BUS();
        Helper helper = new Helper();
        private const string CASIO = "9";
        private const string SEIKO= "10";
        private const string ORIENT = "9";
        private const string OP = "10";
        private const string CITIZEN = "9";
        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                this.lstCasioProducts = mBus.GetProductByBrandId(CASIO);
                this.lstSeikoProducts = mBus.GetProductByBrandId(SEIKO);
                this.lstOrientProducts = mBus.GetProductByBrandId(ORIENT);
                this.lstOpProducts = mBus.GetProductByBrandId(OP);
                this.lstCitizenProducts = mBus.GetProductByBrandId(CITIZEN);
            }
            catch (Exception e)
            {
                this.helper.errorAlert(this, e.Message);
            }
        }

    }
}