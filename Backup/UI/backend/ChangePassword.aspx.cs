using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using BUSs;

namespace UI.backend
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        Auth auth = new Auth();
        static BUS bus = new BUS();
        static string userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            auth.checkAuth();
            this.initialize();
            userID = RouteData.Values["user_id"].ToString();
        }

        protected void initialize()
        {
            btnCancel.Attributes["href"] = Request.UrlReferrer.ToString();
        }

        [WebMethod]
        public static Dictionary<string, object> DoChangePassword(string curPass, string newPass)
        {
            try
            {
                var dic = new Dictionary<string, object>();
                dic.Add("status_code", bus.UpdateUserPassword(curPass, newPass, userID));
                dic.Add("message", "Đổi mật khẩu thành công!");
                return dic;
            }
            catch (Exception e)
            {
                var dic = new Dictionary<string, object>();
                dic.Add("status_code", 0);
                dic.Add("message", e.Message);
                return dic;
            }
        }
    }
}