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
    public partial class WebForm7 : System.Web.UI.Page
    {
        BUS bus = new BUS();
        Auth authUser = new Auth();
        public string serverPath = HttpContext.Current.Request.Url.Segments[0] + "backend/";
        Helper help = new Helper();

        protected void Page_Load(object sender, EventArgs e)
        {
            authUser.checkAuth();
            btnNewBrand.ServerClick += new EventHandler(this.NewBrand);
        }

        protected void NewBrand(object sender, EventArgs args)
        {
            Response.Redirect("/admin/manage/brand/new");
        }

        public void gvListBrands_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            /*
            GridViewRow row = gvListBrands.Rows[e.RowIndex];
            string id = gvListBrands.DataKeys[e.RowIndex].Value.ToString();
            TextBox brandName = row.FindControl("txtBrandName") as TextBox;
            FileUpload brandLogo = row.FindControl("wf_image") as FileUpload;
            TextBox brandDescription = row.FindControl("brandDescription") as TextBox;
            TextBox txtBrandKol = row.FindControl("txtBrandKol") as TextBox;

            try
            {
                if (brandName.Text != "" || txtBrandKol.Text != "")
                {
                    int _brandKol;
                    bool isNumber = int.TryParse(txtBrandKol.Text, out _brandKol);
                    if (!isNumber)
                    {
                        throw new Exception("Hãy nhập số cho mục mức độ nổi tiếng.");
                    }
                    else
                    {
                        string imgPath = brandLogo.FileName != "" ? "assets/images/" + System.IO.Path.GetFileNameWithoutExtension(brandLogo.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(brandLogo.FileName) : "";
                        Brand brand = new Brand(
                            int.Parse(id)
                            , _brandKol
                            , brandName.Text
                            , imgPath
                            , brandDescription.Text
                            , ""
                            , ""
                            );
                        if (bus.EditBrand(brand))
                        {
                            // Nếu người dùng up ảnh thì up lên server
                            if (imgPath != "")
                            {
                                imgPath = AppDomain.CurrentDomain.BaseDirectory + "/backend/" + imgPath;
                                brandLogo.PostedFile.SaveAs(imgPath);
                            }
                            gvListBrands.EditIndex = -1;
                            this.fetchDataToGV();
                        }
                    }

                }
                else
                {
                    throw new Exception("Hãy nhập đủ các trường");
                }
            }
            catch (Exception ex)
            {
                this.help.errorAlert(this, ex.Message);
            }
             * */
        }
    }
}