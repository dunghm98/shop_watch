using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BUSs;
using DTOs;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace UI.backend
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        Auth auth = new Auth();
        BUS b = new BUS();
        static BUS staticBus = new BUS();
        List<ChinhSach> lstChinhSach = new List<ChinhSach>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Protect Route
            auth.checkAuth();
            if (!Page.IsPostBack)
            {
                this.fetchDataToShopInfoFormView();
                this.fetchDataToShopWatchFormView();
                this.fetchDataToChinhSachGridView();
            }
        }

        protected void fetchDataToShopInfoFormView()
        {
           
            try
            {
                List<ShopInfo> lstShop = b.GetShopInfo();
                FV_shopInfo.DataSource = lstShop;
                FV_shopInfo.DataBind();

            }
            catch (Exception e)
            {
                errorAlert(e.Message);
            }
        }

        protected void fetchDataToShopWatchFormView()
        {
            try
            {
                List<ShopWatch> lstShopWatch = b.FetchShopWatch();
                FV_ShopWatch.DataSource = lstShopWatch;
                FV_ShopWatch.DataBind();
            }
            catch (Exception e)
            {
                this.errorAlert(e.Message);
            }
        }

        protected void fetchDataToChinhSachGridView()
        {
            try
            {
                DataKey id = FV_ShopWatch.DataKey;
                string searchText = tb_timKiemChinhSach.Text.Trim();
                if (searchText != "")
                {
                    lstChinhSach = b.SearchChinhSach(int.Parse(id.Value.ToString()), searchText);
                }
                else
                {
                    lstChinhSach = b.fetchChinhSach(int.Parse(id.Value.ToString()));
                }
                gv_ChinhSach.DataSource = lstChinhSach;
                gv_ChinhSach.DataBind();
                gv_ChinhSach.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_ChinhSach.UseAccessibleHeader = true;

            }
            catch (Exception e)
            {
                this.errorAlert(e.Message);
            }
        }

        // Các chế độ xem thay đổi ví dụ: thêm sửa 
        protected void FV_shopInfo_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            FV_shopInfo.ChangeMode(e.NewMode);
            this.fetchDataToShopInfoFormView();
        }

        protected void FV_shopInfo_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            // Get tất cả các field
            DataKey id = FV_shopInfo.DataKey;
            TextBox wf_locate = (TextBox)FV_shopInfo.FindControl("wf_locate");
            TextBox wf_hotLine = (TextBox)FV_shopInfo.FindControl("wf_hotLine");
            TextBox wf_website = (TextBox)FV_shopInfo.FindControl("wf_website");
            TextBox wf_email = (TextBox)FV_shopInfo.FindControl("wf_email");
            TextBox wf_openTime = (TextBox)FV_shopInfo.FindControl("wf_openTime");
            TextBox wf_openDates = (TextBox)FV_shopInfo.FindControl("wf_openDates");
            TextBox wf_fb = (TextBox)FV_shopInfo.FindControl("wf_fb");
            TextBox wf_ytb = (TextBox)FV_shopInfo.FindControl("wf_ytb");
            TextBox wf_maps = (TextBox)FV_shopInfo.FindControl("wf_maps");
            FileUpload wf_images = (FileUpload)FV_shopInfo.FindControl("wf_images");

            string imgPath = wf_images.FileName != "" ? "/backend/assets/images/" + System.IO.Path.GetFileNameWithoutExtension(wf_images.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(wf_images.FileName) : "";


            try
            {
                ShopInfo s = new ShopInfo(
                    int.Parse(id.Value.ToString())
                    , wf_locate.Text.Trim()
                    , wf_hotLine.Text.Trim()
                    , wf_website.Text.Trim()
                    , wf_email.Text.Trim()
                    , wf_openTime.Text.Trim()
                    , wf_openDates.Text.Trim()
                    , wf_fb.Text.Trim()
                    , wf_ytb.Text.Trim()
                    , wf_maps.Text.Trim()
                    , imgPath
                    );
                // Nếu cập nahajt thành công
                if (b.UpdateShopInfo(s))
                {
                    // Nếu người dùng up ảnh thì up lên server
                    if (imgPath != "")
                    {
                        imgPath = AppDomain.CurrentDomain.BaseDirectory + imgPath;
                        wf_images.PostedFile.SaveAs(imgPath);
                    }
                    bigTopConnerAlert("Đã cập nhật thành công");
                    // back to default view
                    FV_shopInfo.ChangeMode(FormViewMode.ReadOnly);
                    this.fetchDataToShopInfoFormView();
                }
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

        protected void FV_shopInfo_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            TextBox wf_locate = (TextBox)FV_shopInfo.FindControl("wf_locate");
            TextBox wf_hotLine = (TextBox)FV_shopInfo.FindControl("wf_hotLine");
            TextBox wf_website = (TextBox)FV_shopInfo.FindControl("wf_website");
            TextBox wf_email = (TextBox)FV_shopInfo.FindControl("wf_email");
            TextBox wf_openTime = (TextBox)FV_shopInfo.FindControl("wf_openTime");
            TextBox wf_openDates = (TextBox)FV_shopInfo.FindControl("wf_openDates");
            TextBox wf_fb = (TextBox)FV_shopInfo.FindControl("wf_fb");
            TextBox wf_ytb = (TextBox)FV_shopInfo.FindControl("wf_ytb");
            TextBox wf_maps = (TextBox)FV_shopInfo.FindControl("wf_maps");
            FileUpload wf_images = (FileUpload)FV_shopInfo.FindControl("wf_images");

            string imgPath = wf_images.FileName != "" ? "/backend/assets/images/" + System.IO.Path.GetFileNameWithoutExtension(wf_images.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(wf_images.FileName) : "";

            try
            {
                ShopInfo s = new ShopInfo(
                    -1
                    , wf_locate.Text.Trim()
                    , wf_hotLine.Text.Trim()
                    , wf_website.Text.Trim()
                    , wf_email.Text.Trim()
                    , wf_openTime.Text.Trim()
                    , wf_openDates.Text.Trim()
                    , wf_fb.Text.Trim()
                    , wf_ytb.Text.Trim()
                    , wf_maps.Text.Trim()
                    , imgPath
                    );
                // Nếu cập nahajt thành công
                if (b.InsertShopInfo(s))
                {
                    // Nếu người dùng up ảnh thì up lên server
                    if (imgPath != "")
                    {
                        imgPath = AppDomain.CurrentDomain.BaseDirectory + imgPath;
                        wf_images.PostedFile.SaveAs(imgPath);
                    }
                    // back to default view
                    FV_shopInfo.ChangeMode(FormViewMode.ReadOnly);
                    this.fetchDataToShopInfoFormView();
                }
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
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

        protected void FV_ShopWatch_ItemUpdating(object sender, FormViewUpdateEventArgs e)
        {
            DataKey id = FV_ShopWatch.DataKey;
            TextBox wf_baoHiem = (TextBox)FV_ShopWatch.FindControl("wf_baoHiem");
            TextBox wf_baoHanh = (TextBox)FV_ShopWatch.FindControl("wf_baoHanh");
            TextBox wf_thamDinh = (TextBox)FV_ShopWatch.FindControl("wf_thamDinh");
            TextBox wf_giaoHang = (TextBox)FV_ShopWatch.FindControl("wf_giaoHang");
            TextBox wf_thoiGianBaoHanh = (TextBox)FV_ShopWatch.FindControl("wf_thoiGianBaoHanh");

            try
            {
                ShopWatch sw = new ShopWatch(
                    int.Parse(id.Value.ToString())
                    , wf_baoHiem.Text.Trim()
                    , int.Parse(wf_baoHanh.Text.Trim())
                    , wf_thamDinh.Text.Trim()
                    , wf_giaoHang.Text.Trim()
                    , wf_thoiGianBaoHanh.Text.Trim()
                    );
                if (b.UpdateShopWatch(sw))
                {
                    bigTopConnerAlert("Đã cập nhật thành công");
                    FV_ShopWatch.ChangeMode(FormViewMode.ReadOnly);
                    this.fetchDataToShopWatchFormView();
                }
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

        protected void FV_ShopWatch_ModeChanging(object sender, FormViewModeEventArgs e)
        {
            FV_ShopWatch.ChangeMode(e.NewMode);
            this.fetchDataToShopWatchFormView();
        }

        protected void wf_baoHanh_validation(object sender, ServerValidateEventArgs e)
        {
            if (Regex.IsMatch(e.Value, @"\d"))
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        /*
        protected void gv_ChinhSach_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Control control = null;
            if (gv_ChinhSach.FooterRow != null)
            {
                control = gv_ChinhSach.FooterRow;
            }
            else
            {
                control = gv_ChinhSach.Controls[0].Controls[1];
            }
            string tb_noiDungText = (control.FindControl("tb_noiDung") as TextBox).Text;
            switch (e.CommandName)
            {
                case "InsertRow":
                    // string tb_noiDungText = (row.FindControl("tb_noiDung") as TextBox).Text;
                    break;
                case "EditRow":
                    gv_ChinhSach.EditIndex = GridViewEditEventArgs.Empty.ew
                    break;
            }
        }*/

        /*
        protected void lnkNew_Click(object sender, EventArgs e)
        {
            Control control = gv_ChinhSach.Controls[0].Controls[1];
            RadioButton r = control.FindControl("opt1") as RadioButton;
            ChinhSach cs = new ChinhSach();
            cs.NoiDung = (control.FindControl("tb_noiDung") as TextBox).Text;
            cs.ChinhSachApDung = (control.FindControl("opt1") as RadioButton).Checked == true ? 1 : 0;
            cs.ShopWatch_id = int.Parse(FV_ShopWatch.DataKey.Value.ToString());
            try
            {
                if (b.InsertChinhSach(cs))
                {
                    this.bigTopConnerAlert("Thêm thành công");
                    this.fetchDataToChinhSachGridView();
                }
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

        protected void lnkNew_Command(object sender, CommandEventArgs e)
        {
            Control control = gv_ChinhSach.Controls[0].Controls[1];
            RadioButton r = control.FindControl("opt1") as RadioButton;
            ChinhSach cs = new ChinhSach();
            cs.NoiDung = (control.FindControl("tb_noiDung") as TextBox).Text;
            cs.ChinhSachApDung = (control.FindControl("opt1") as RadioButton).Checked == true ? 1 : 0;
            cs.ShopWatch_id = int.Parse(FV_ShopWatch.DataKey.Value.ToString());
            try
            {
                if (b.InsertChinhSach(cs))
                {
                    this.bigTopConnerAlert("Thêm thành công");
                    this.fetchDataToChinhSachGridView();
                }
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }*/

        [WebMethod]
        public static Dictionary<string, object> InsertNewRecord(string noi_dung, string chinh_sach, string shopWatchID)
        {
            try
            {
                var responseData = new Dictionary<string, object>();
                ChinhSach cs = new ChinhSach();
                cs.NoiDung = noi_dung;
                cs.ChinhSachApDung = int.Parse(chinh_sach);
                cs.ShopWatch_id = int.Parse(shopWatchID);
                responseData.Add("status_code", staticBus.InsertChinhSach(cs));
                responseData.Add("message", "Thêm mới thành công");
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

        protected void gv_ChinhSach_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_ChinhSach.ShowFooter = false;
            gv_ChinhSach.EditIndex = e.NewEditIndex;
            this.fetchDataToChinhSachGridView();
        }

        protected void gv_ChinhSach_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_ChinhSach.ShowFooter = true;
            gv_ChinhSach.EditIndex = -1;
            this.fetchDataToChinhSachGridView();
        }

        protected void gv_ChinhSach_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gv_ChinhSach.Rows[e.RowIndex];
            string chinhSachID = gv_ChinhSach.DataKeys[e.RowIndex].Value.ToString();
            TextBox edit_tb_noiDung = row.FindControl("edit_tb_noiDung") as TextBox;
            RadioButton edit_opt1 = row.FindControl("edit_opt1") as RadioButton;

            ChinhSach cs = new ChinhSach();
            cs.Id = int.Parse(chinhSachID);
            cs.NoiDung = edit_tb_noiDung.Text;
            cs.ChinhSachApDung = edit_opt1.Checked ? 1 : 0;
            cs.ShopWatch_id = int.Parse(FV_ShopWatch.DataKey.Value.ToString());

            try
            {
                if (b.UpdateChinhSach(cs))
                {
                    gv_ChinhSach.ShowFooter = true;
                    gv_ChinhSach.EditIndex = -1;
                    this.fetchDataToChinhSachGridView();
                }
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

        protected void gv_ChinhSach_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = int.Parse(gv_ChinhSach.DataKeys[e.RowIndex].Value.ToString());
            try
            {
                if (b.DeleteChinhSach(id))
                {
                    this.bigTopConnerAlert("Xoá thành công!!");
                    this.fetchDataToChinhSachGridView();
                }
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

        protected void FV_ShopWatch_ItemInserting(object sender, FormViewInsertEventArgs e)
        {
            TextBox wf_baoHiem = (TextBox)FV_ShopWatch.FindControl("wf_baoHiem");
            TextBox wf_baoHanh = (TextBox)FV_ShopWatch.FindControl("wf_baoHanh");
            TextBox wf_thamDinh = (TextBox)FV_ShopWatch.FindControl("wf_thamDinh");
            TextBox wf_giaoHang = (TextBox)FV_ShopWatch.FindControl("wf_giaoHang");
            TextBox wf_thoiGianBaoHanh = (TextBox)FV_ShopWatch.FindControl("wf_thoiGianBaoHanh");

            try
            {
                ShopWatch sw = new ShopWatch(
                    -1
                    , wf_baoHiem.Text.Trim()
                    , int.Parse(wf_baoHanh.Text.Trim())
                    , wf_thamDinh.Text.Trim()
                    , wf_giaoHang.Text.Trim()
                    , wf_thoiGianBaoHanh.Text.Trim()
                    );
                if (b.InsertShopWatch(sw))
                {
                    bigTopConnerAlert("Đã cập nhật thành công");
                    FV_ShopWatch.ChangeMode(FormViewMode.ReadOnly);
                    this.fetchDataToShopWatchFormView();
                }
            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

        // Tìm kiếm chính sách
        protected void tb_timKiemChinhSach_TextChanged(object sender, EventArgs e)
        {
            string searchText = tb_timKiemChinhSach.Text.Trim();
            try
            {
                DataKey id = FV_ShopWatch.DataKey;
                lstChinhSach = b.SearchChinhSach(int.Parse(id.Value.ToString()), searchText);
                this.fetchDataToChinhSachGridView();

            }
            catch (Exception ex)
            {
                this.errorAlert(ex.Message);
            }
        }

    }
}