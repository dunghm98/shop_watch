using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTOs;
using BUSs;
using System.IO;
using System.Web.Script.Serialization;

namespace UI.backend
{
    /// <summary>
    /// Summary description for UpdateBrandLogo
    /// </summary>
    public class UpdateBrandLogo : IHttpHandler
    {
        BUS bus = new BUS();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
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
                        if (bus.UpdateBrandLogo(id, imagePath))
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
            }
            catch (Exception e)
            {
                json.statusCode = 404;
                json.message = e.Message;
            }
            finally
            {
                context.Response.Write(new JavaScriptSerializer().Serialize(json));
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