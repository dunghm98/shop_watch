using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.frontend
{
    public partial class App : System.Web.UI.MasterPage
    {
        public string serverPath = HttpContext.Current.Request.Url.Segments[0] + "frontend/";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}