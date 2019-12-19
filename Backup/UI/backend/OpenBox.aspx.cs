using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace UI.backend
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        static BUSs.BUS staticBus = new BUSs.BUS();
        Auth auth = new Auth();
        protected void Page_Load(object sender, EventArgs e)
        {
            auth.checkAuth();
            if (gv_vidList.DataSource != null)
            {
                gv_vidList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_vidList.UseAccessibleHeader = true;
            }
            
        }

        [WebMethod]
        public static Dictionary<string, object> InsertNewRecord(string txtContent)
        {
            try
            {
                var responseData = new Dictionary<string, object>();
                responseData.Add("status_code", staticBus.InsertNewVideo(txtContent));
                responseData.Add("message", "Thêm mới thành công");
                return responseData;
            }
            catch (Exception e)
            {
                var exception = new Dictionary<string, object>();
                exception.Add("status_code", 0);
                exception.Add("message", e);
                return exception;
            }
        }
    }
}