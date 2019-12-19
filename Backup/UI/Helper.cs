using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI
{
    public class Helper
    {
        #region Notifications
        public void errorAlert(System.Web.UI.Page page, String message)
        {
            page.ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "errorAlert(`" + message + "`)", true);
        }
        public void successAlert(System.Web.UI.Page page, String message)
        {
            page.ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "successAlert(`" + message + "`)", true);
        }
        public void bigTopConnerAlert(System.Web.UI.Page page, String message)
        {
            page.ClientScript.RegisterStartupScript(this.GetType(), "randomtext", "bigTopConnerAlert(`" + message + "`)", true);
        }
        #endregion
    }
}