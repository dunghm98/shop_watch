using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTOs;
using System.Web.Services;

namespace UI.backend
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        BUSs.BUS b = new BUSs.BUS();
        static BUSs.BUS staticBus = new BUSs.BUS();
        Auth auth = new Auth();
        protected void Page_Load(object sender, EventArgs e)
        {
            auth.checkAuth();
            if (!IsPostBack)
            {
                this.fetchCategoryToSelect();
                this.fetchDataToGv();
                category_order.Value = (int.Parse(b.FindParentOrder("-1")) + 1).ToString();
            }
            btnNewCategory.ServerClick += new EventHandler(this.btnNewCategory_Click);
        }

        protected void fetchCategoryToSelect()
        {
            List<Category> lstCat = new List<Category>();
            lstCat.Add(new Category(-1, 0, 0, 0, "Là danh mục con của?", ""));
            try
            {
                lstCat.AddRange(b.fetchLstCategory(-1, ""));
                parent_id.DataSource = lstCat;
                parent_id.DataValueField = "id";
                parent_id.DataTextField = "name";
                parent_id.DataBind();
            }
            catch (Exception e)
            {
                this.errorAlert(e.Message);
            }
        }


        void btnNewCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (category_name.Value == "" || category_order.Value == "")
                {
                    throw new Exception("Tên danh mục và thứ tự đừng để trống");
                }
                else
                {
                    Category cat = new Category(-1, int.Parse(parent_id.Value), int.Parse(category_order.Value), is_visible.Checked ? 1 : 0, category_name.Value, category_description.Value);
                    b.InsertNewCategory(cat);
                    this.fetchCategoryToSelect();
                    this.category_name.Value = "";
                    this.category_description.Value = "";
                    this.fetchDataToGv();
                }
            }
            catch (Exception ex)
            {
                // Hiển thị form nhập
                insertForm.Attributes["class"] = "card card-outline card-info";
                btnCollapse.Attributes["class"] = "fas fa-minus";
                // ./Hiển thị form nhập
                this.errorAlert(ex.Message);
            }
        }

        [WebMethod]
        public static Dictionary<string, object> FindParentOrder(string id)
        {
            try
            {
                var responseData = new Dictionary<string, object>();
                responseData.Add("status_code", 200);
                responseData.Add("message", "Thành công!");
                responseData.Add("order", staticBus.FindParentOrder(id));
                return responseData;
            }
            catch (Exception e)
            {
                var exception = new Dictionary<string, object>();
                exception.Add("status_code", 0);
                exception.Add("message", e.Message);
                return exception;
            }
        }

        [WebMethod]
        public static Dictionary<string, object> SetState(string id, bool state)
        {
            try
            {
                var responseData = new Dictionary<string, object>();
                responseData.Add("status_code", staticBus.SetState(id, state));
                responseData.Add("message", "Thành công!");
                return responseData;
            }
            catch (Exception e)
            {
                var exception = new Dictionary<string, object>();
                exception.Add("status_code", 0);
                exception.Add("message", e.Message);
                return exception;
            }
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

        protected void gv_vidList_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            this.fetchCategoryToSelect();
        }

        protected void gv_vidList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    HiddenField hf = e.Row.FindControl("hf_id") as HiddenField;
                    DropDownList dl = e.Row.FindControl("DropDownList1") as DropDownList;
                    List<Category> lstCat = new List<Category>();
                    lstCat.Add(new Category(-1, 0, 0, 0, "Là danh mục con của?", ""));
                    try
                    {
                        lstCat.AddRange(b.fetchLstCategory(-1, ""));
                    }
                    catch (Exception ex)
                    {
                        this.errorAlert(ex.Message);
                    }
                    dl.DataSource = lstCat;
                    dl.DataValueField = "id";
                    dl.DataTextField = "name";
                    dl.DataBind();
                    if (dl != null && hf != null)
                    {
                        dl.SelectedValue = hf.Value;
                    }
                }
            }
        }

        public void fetchDataToGv()
        {
            try
            {
                gv_vidList.DataSource = b.fetchCategoryData();
                gv_vidList.DataBind();
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

        protected void gv_vidList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gv_vidList.Rows[e.RowIndex];
            string id = gv_vidList.DataKeys[e.RowIndex].Value.ToString();
            TextBox des = row.FindControl("category_description") as TextBox;
            DropDownList dl = row.FindControl("DropDownList1") as DropDownList;
            TextBox order = row.FindControl("category_order") as TextBox;
            TextBox name = row.FindControl("category_name") as TextBox;
            Category cat = new Category(int.Parse(id), int.Parse(dl.SelectedValue), int.Parse(order.Text), 0, name.Text, des.Text);
            try
            {
                b.UpdateCategory(cat);
                gv_vidList.EditIndex = -1;
                this.fetchDataToGv();
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

        protected void gv_vidList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_vidList.PageIndex = e.NewPageIndex;
            this.fetchDataToGv();
        }

        protected void gv_vidList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_vidList.EditIndex = e.NewEditIndex;
            this.fetchDataToGv();
        }

        protected void gv_vidList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_vidList.EditIndex = -1;
            this.fetchDataToGv();
        }

        protected void gv_vidList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gv_vidList.DataKeys[e.RowIndex].Value.ToString();
            try
            {
                if (b.DeleteCategory(id))
                {
                    this.fetchDataToGv();
                    this.fetchCategoryToSelect();
                }
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

    }
}