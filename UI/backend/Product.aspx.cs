using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTOs;
using BUSs;
using Newtonsoft.Json;

namespace UI.backend
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        Auth authUser = new Auth();
        BUS b = new BUS();
        static BUS sBus = new BUS();
        public string serverPath = HttpContext.Current.Request.Url.Segments[0] + "backend/";

        protected void Page_Load(object sender, EventArgs e)
        {
            authUser.checkAuth();
            if (!IsPostBack)
            {

            }
            btnNewProduct.ServerClick += new EventHandler(this.btnNewProduct_Click);
        }

        protected void fetchProductsList()
        {
            try
            {
            }
            catch (Exception e)
            {
                this.errorAlert(e.Message);
            }
        }

        protected void btnNewProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("/admin/manage/product/new");
        }
        #region Notifications
        public void errorAlert(String message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "errorAlert(`" + message + "`)", true);
        }
        public void successAlert(String message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "successAlert(`" + message + "`)", true);
        }
        public void bigTopConnerAlert(String message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "bigTopConnerAlert(`" + message + "`)", true);
        }
        #endregion
    }
}