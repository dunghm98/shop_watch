using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace UI
{
    public class Global : System.Web.HttpApplication
    {
        
        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Admin", "admin", "~/backend/Login.aspx");
            routes.MapPageRoute("Login", "login", "~/backend/Login.aspx");
            routes.MapPageRoute("Dashboard", "admin/dashboard", "~/backend/Dashboard.aspx");
            routes.MapPageRoute("ChangePass", "admin/change-pass/{user_id}", "~/backend/ChangePassword.aspx");
            routes.MapPageRoute("ShopInfo", "admin/manage/shop-info", "~/backend/ShopInfo.aspx");
            routes.MapPageRoute("OpenBoxs", "admin/manage/open-box", "~/backend/OpenBox.aspx");
            routes.MapPageRoute("CategoryManagement", "admin/manage/category", "~/backend/CategoryManagement.aspx");
            routes.MapPageRoute("Brand", "admin/manage/brand", "~/backend/Brand.aspx");
            routes.MapPageRoute("NewBrand", "admin/manage/brand/new", "~/backend/NewBrand.aspx");
            routes.MapPageRoute("BrandDetail", "admin/manage/brand/{Brand}", "~/backend/BrandDetail.aspx");
            routes.MapPageRoute("Product", "admin/manage/product", "~/backend/Product.aspx");
            routes.MapPageRoute("NewProduct", "admin/manage/product/new", "~/backend/NewProduct.aspx");
            routes.MapPageRoute("ProductDetail", "admin/manage/product/{Product}", "~/backend/ProductDetail.aspx");
            #region FE
            routes.MapPageRoute("FEProductDetail", "product/{Product}", "~/frontend/ProductDetail.aspx");
            routes.MapPageRoute("Home", "", "~/frontend/homepage.aspx");
            #endregion
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}