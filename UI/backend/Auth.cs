using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTOs;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.backend
{
    public class Auth : System.Web.UI.Page
    {
        public void checkAuth()
        {
            if (Session["user"] == null)
            {
                HttpContext.Current.Response.Redirect("/login");
            }
            
        }

        public User authUser()
        {
            return (User) Session["user"];
        }
    }
}