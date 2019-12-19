using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTOs;

namespace UI.backend.layout
{
    public partial class MainSidebar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.checkAuth();
            this.UIHandle();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                Session["user"] = null;
                Response.Redirect("/login");
            }
        }

        protected void checkAuth()
        {
            if (Session["user"] != null)
            {
                User authUser = (User)Session["user"];
                this.lb_userName.Controls.Add(new Literal() { Text = "Xin chào! " + authUser.Name + "<i style=\"top: 1rem;\" class=\"fas fa-angle-left right\"></i>" });
                this.btnChangePass.Attributes["href"] = "/admin/change-pass/" + authUser.Id;
            }
        }

        public Control BtnChangePass
        {
            get { return (Control)this.btnChangePass; }
        }

        /**
         * style side bar
         * */
        protected void UIHandle()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            if (path.Contains("/admin/dashboard"))
            {
                dashboardCss.Attributes.Add("class", dashboardCss.Attributes["class"] + " active");
            }
            if (path.Contains("admin/change-pass/"))
            {
                liChangePass.Attributes.Add("class", liChangePass.Attributes["class"] + " menu-open");
                btnChangePass.Attributes.Add("class", btnChangePass.Attributes["class"] + " active");
            }
            if (path.Contains("admin/manage/shop-info"))
            {
                shopInfo_htmlID.Attributes.Add("class", shopInfo_htmlID.Attributes["class"] + " active");
            }
            if (path.Contains("admin/manage/open-box"))
            {
                danhMucTinTuc_htmlID.Attributes.Add("class", danhMucTinTuc_htmlID.Attributes["class"] + " menu-open");
                openBox_htmlID.Attributes.Add("class", openBox_htmlID.Attributes["class"] + " active");
            }
            if (path.Contains("admin/manage/category"))
            {
                danhMucTinTuc_htmlID.Attributes.Add("class", danhMucTinTuc_htmlID.Attributes["class"] + " menu-open");
                category_htmlID.Attributes.Add("class", category_htmlID.Attributes["class"] + " active");
            }
            if (path.Contains("admin/manage/product"))
            {
                productBrand_htmlID.Attributes.Add("class", productBrand_htmlID.Attributes["class"] + " menu-open");
                products_htmlID.Attributes.Add("class", products_htmlID.Attributes["class"] + " active");
            }
            if (path.Contains("admin/manage/brand"))
            {
                productBrand_htmlID.Attributes.Add("class", productBrand_htmlID.Attributes["class"] + " menu-open");
                brands_htmlID.Attributes.Add("class", brands_htmlID.Attributes["class"] + " active");
            } 
            // href route
            dashboardCss.Attributes["href"] = Page.GetRouteUrl("Dashboard", null);
            shopInfo_htmlID.Attributes["href"] = Page.GetRouteUrl("ShopInfo", null);
            openBox_htmlID.Attributes["href"] = Page.GetRouteUrl("OpenBoxs", null);
            category_htmlID.Attributes["href"] = Page.GetRouteUrl("CategoryManagement", null);
            products_htmlID.Attributes["href"] = Page.GetRouteUrl("Product", null);
            brands_htmlID.Attributes["href"] = Page.GetRouteUrl("Brand", null);
        }
    }
}