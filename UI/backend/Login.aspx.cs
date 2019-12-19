using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BUSs;
using DTOs;

namespace UI.backend
{
    public partial class Login : System.Web.UI.Page
    {
        BUS b = new BUS();
        User u = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            checkAuth();
        }

        protected void doLogin(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            try
            {
                if (email.Trim() == "" || password.Trim() == "")
                {
                    throw new LoginException("Email và mật khẩu không được để trống");
                }
                u = b.getLogin(email, password);
                Session["user"] = u;
                Response.Redirect("/admin/dashboard");
            }
            catch (LoginException le)
            {
                errorAlert(le.Message);
            }
            catch (Exception ex)
            {
                errorAlert(ex.Message);
            }
        }

        protected void checkAuth()
        {
            if (Session["user"] != null)
            {
                Response.Redirect("/admin/dashboard");
            }
        }

        public void errorAlert(String message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "errorAlert('" + message + "')", true);
        }
        public void successAlert(String message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "successAlert('" + message + "')", true);
        }
        public void bigTopConnerAlert(String message)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "bigTopConnerAlert('" + message + "')", true);
        }
    }
}