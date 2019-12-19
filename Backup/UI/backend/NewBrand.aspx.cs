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
    public partial class WebForm10 : System.Web.UI.Page
    {
        public string serverPath = HttpContext.Current.Request.Url.Segments[0] + "backend/";
        BUS bus = new BUS();
        Auth authUser = new Auth();
        Helper helper = new Helper();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnBack.Attributes["href"] = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "/admin/manage/brand";
            btnSave.ServerClick += new EventHandler(btnSave_ServerClick);
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
                            -1
                            , _brandKol
                            , txtBrandName.Text
                            , imgPath
                            , txtBrandDescription.Value
                            , ""
                            , ""
                            );
                        if (bus.NewBrand(brand))
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