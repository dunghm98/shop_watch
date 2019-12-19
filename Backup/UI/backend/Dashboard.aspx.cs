using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTOs;
using System.Web.UI.HtmlControls;

namespace UI.backend
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        BUSs.BUS bus = new BUSs.BUS();
        public static User authUser;
        Auth auth = new Auth();
        public string serverPath = HttpContext.Current.Request.Url.Segments[0] + "backend/";
        public int productTotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            auth.checkAuth();
            productTotal = bus.GetProductTotal();
        }

    }
}