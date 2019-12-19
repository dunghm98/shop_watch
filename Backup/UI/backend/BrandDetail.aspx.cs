using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTOs;
using BUSs;

namespace UI.backend
{
    public partial class WebForm11 : System.Web.UI.Page
    {
        Auth authUser = new Auth();
        BUS bus = new BUS();
        public Brand brand = new Brand();
        Helper helper = new Helper();
        int brandId = -1;
        public string serverPath = HttpContext.Current.Request.Url.Segments[0] + "backend/";

        protected void Page_Load(object sender, EventArgs e)
        {
            authUser.checkAuth();
            btnBack.Attributes["href"] = "/admin/manage/brand";
            btnSave.ServerClick += new EventHandler(this.btnSave_ServerClick);
            #region Get Brand
            bool isBrandId = int.TryParse(RouteData.Values["Brand"].ToString(), out brandId);
            if (isBrandId)
            {
                brand = bus.GetBrand(brandId);
            }
            else
            {
                this.helper.errorAlert(this, "404");
            }
            if (!IsPostBack)
            {
                txtBrandName.Text = brand.Name;
                txtBrandKol.Text = brand.Kol.ToString().Trim();
                txtBrandDescription.Value = brand.Description;
            }
            #endregion
        }

        protected void btnSave_ServerClick(object sender, EventArgs args)
        {
            try
            {
                if (txtBrandName.Text != "" || txtBrandKol.Text != "")
                {
                    int _brandKol;
                    bool isNumber = int.TryParse(txtBrandKol.Text, out _brandKol);
                    if (!isNumber)
                    {
                        throw new Exception("Hãy nhập số cho mục mức độ nổi tiếng.");
                    }
                    else
                    {
                        string imgPath = brandLogo.FileName != "" ? "/backend/assets/images/" + System.IO.Path.GetFileNameWithoutExtension(brandLogo.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(brandLogo.FileName) : "";
                        Brand brand = new Brand(
                            brandId
                            , _brandKol
                            , txtBrandName.Text
                            , imgPath
                            , txtBrandDescription.Value
                            , ""
                            , ""
                            );
                        if (bus.EditBrand(brand))
                        {
                            // Nếu người dùng up ảnh thì up lên server
                            if (imgPath != "")
                            {
                                imgPath = AppDomain.CurrentDomain.BaseDirectory + imgPath;
                                brandLogo.PostedFile.SaveAs(imgPath);
                            }
                            Response.Redirect("/admin/manage/brand");
                        }
                    }
                }
                else
                {
                    throw new Exception("Hãy nhập đủ các trường");
                }
            }
            catch (Exception e)
            {
                this.helper.errorAlert(this, e.Message);
            }
        }
    }
}