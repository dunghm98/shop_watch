using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Newtonsoft.Json;
using DTOs;
using System.IO;

namespace UI.backend
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        #region DEFINE CONST
        const int ENABLE = 1;
        const int DISABLE = 0;
        #endregion
        BUSs.BUS bus = new BUSs.BUS();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        #region Brands
        [WebMethod()]
        public Json FetchBrands(int currentPage, int pageSize, string searchText)
        {
            Json json = new Json();
            try
            {
                json.statusCode = 200;
                json.data = bus.fetchBrands(currentPage, pageSize, searchText);
                return json;
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
                return json;
            }
        }

        [WebMethod()]
        public Json DeleteBrands(string[] brands)
        {
            Json json = new Json();
            try
            {
                if (bus.DeleteBrands(brands))
                {
                    json.statusCode = 200;
                    json.message = "Xoá thành công";
                }
                return json;
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
                return json;
            }
        }

        [WebMethod()]
        public Json DeleteABrand(string id)
        {
            Json json = new Json();
            try
            {
                if (bus.DeleteBrand(int.Parse(id)))
                {
                    json.statusCode = 200;
                    json.message = "Xoá thành công";
                }
                return json;
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
                return json;
            }
        }

        [WebMethod()]
        public Json DeleteBrandImage(string id)
        {
            Json json = new Json();
            try
            {
                if (bus.DeleteBrandLogo(id))
                {
                    json.statusCode = 200;
                    json.message = "Xoá thành công";
                }
                return json;
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
                return json;
            }
        }
        #endregion

        #region Products
        // ********************************************************************
        [WebMethod]
        public Json FetchProducts(int currentPage, int pageSize, string searchText)
        {
            Json json = new Json();
            try
            {
                json.statusCode = 200;
                json.data = bus.FetchProducts(currentPage, pageSize, searchText);
                return json;
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
                return json;
            }
        }

        [WebMethod()]
        public Json DeleteAProduct(string id)
        {
            Json json = new Json();
            try
            {
                if (bus.DeleteAProduct(id))
                {
                    json.statusCode = 200;
                    json.message = "Xoá thành công";
                }
                return json;
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
                return json;
            }
        }

        [WebMethod()]
        public Json DeleteProducts(string[] products )
        {
            Json json = new Json();
            try
            {
                if (bus.DeleteProducts(products))
                {
                    json.statusCode = 200;
                    json.message = "Xoá thành công";
                }
                return json;
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
                return json;
            }
        }

        [WebMethod()]
        public Json SetStatusProduct(string[] products, int actionType)
        {
            Json json = new Json();
            try
            {
                if (bus.SetStatusProduct(products, actionType))
                {
                    json.statusCode = 200;
                    json.message = "Đã cập nhật";
                }
                return json;
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
                return json;
            }
        }

        [WebMethod()]
        public Json DeleteProductImage(string id)
        {
            Json json = new Json();
            try
            {
                if (bus.DeleteProductImage(id))
                {
                    json.statusCode = 200;
                    json.message = "Xoá thành công";
                }
                return json;
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
                return json;
            }
        }
        // ./********************************************************************
        #endregion
    }
}
