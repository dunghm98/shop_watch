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
    public partial class WebForm2 : System.Web.UI.Page
    {
        public List<Product> lstRelateProducts = new List<Product>();
        public Product product = new Product();
        Helper helper = new Helper();
        BUS bus = new BUS();
        Madu_BUS mBus = new Madu_BUS();
        private int productId = -1;
        public string serverPath = HttpContext.Current.Request.Url.Segments[0] + "frontend/";
        protected void Page_Load(object sender, EventArgs args)
        {
            
            try
            {
                
                bool isProductId = int.TryParse(RouteData.Values["Product"].ToString(), out productId);
                if (isProductId)
                {
                    product = bus.GetProduct(productId);
                    int brandId = this.GetRelateProduct(productId);
                    this.lstRelateProducts = mBus.GetProductByBrandId(brandId.ToString());
                }
            
            }
            catch(Exception e)
            {
                this.helper.errorAlert(this, e.Message);
            }
        }
        public int GetRelateProduct(int id)
        {
            return bus.GetBrandId(id);
           
        }
    }
}