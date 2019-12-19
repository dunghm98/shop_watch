using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using DTOs;
using BUSs;
using System.Web.Script.Serialization;

namespace UI.backend
{
    /// <summary>
    /// Summary description for WebHandler
    /// </summary>
    public class WebHandler : IHttpHandler
    {
        BUS bus = new BUS();
        public void ProcessRequest(HttpContext context)
        {
            Json json = this.UpdateProductImage(context);
            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(json));
            //context.Response.ContentType = "text/plain";
            // context.Response.Write("Hello World");
        }

        /*
         * ProductUpdateImage
         */
        public Json UpdateProductImage(HttpContext context)
        {
            Json json = new Json();
            try
            {
                string imageName = "";
                string id = "";
                foreach (string formItem in context.Request.Form)
                {
                    id = context.Request.Form[formItem];
                }
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        imageName = System.IO.Path.GetFileNameWithoutExtension(fileName) + DateTime.Now.ToString("ddMMyyyyhhmmss") + fileExtension;
                        string imagePath = "/backend/assets/images/products/" + imageName;
                        string uloadImagePath = HttpContext.Current.Server.MapPath("~/backend/assets/images/products/") + imageName;
                        if (bus.UpdateProductImage(id, imagePath))
                        {
                            file.SaveAs(uloadImagePath);
                            json.statusCode = 200;
                            json.data = imagePath;
                            json.message = "Cập nhật ảnh thành công!";
                        }
                    }
                    else
                    {
                        throw new Exception("Không có file nào được chọn");
                    }
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}