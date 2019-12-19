using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DTOs;
using BCrypt.Net;
using System.IO;
using System.Web;
using System.Globalization;

namespace DALs
{
    public class DAL
    {
        #region Constants Define
        public const int ENABLE = 1;
        public const int DISABLE = 0;
        #endregion

        private SqlConnection conn = null;
        private SqlDataReader reader = null;
        private SqlCommand cmd = null;

        protected void getConnect()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnect"].ConnectionString);
            conn.Open();
        }

        protected void getClose()
        {
            conn.Dispose();
            conn.Close();
        }
        #region User
        public User checkLogin(string _email, string _password)
        {
            string passTest = BCrypt.Net.BCrypt.HashPassword("1");
            try
            {
                getConnect();
                User user = new User();
                string query = "SELECT * FROM users WHERE email = @email";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", _email);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (BCrypt.Net.BCrypt.Verify(_password, reader["password"].ToString().Trim()))
                    {
                        user.Id = int.Parse(reader["id"].ToString());
                        user.UserName = reader["username"].ToString();
                        user.Email = reader["email"].ToString();
                        user.Phone = reader["phone"].ToString();
                        user.Name = reader["name"].ToString();
                        user.Role = int.Parse(reader["role"].ToString());
                        user.Password = "secret";
                        return user;
                    }
                    else
                    {
                        throw new LoginException("Sai địa chỉ email hoặc mật khẩu");
                    }
                }
                throw new LoginException("Sai địa chỉ email hoặc mật khẩu");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                getClose();
            }
        }

        public int UpdateUserPassword(string _userID, string _oldPass, string _newPass)
        {
            try
            {
                getConnect();
                string query = "SELECT * FROM users WHERE ID = @ID";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", _userID);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                // int isFound = (int)cmd.ExecuteScalar();
                while (reader.Read())
                {
                    if (BCrypt.Net.BCrypt.Verify(_oldPass, reader["password"].ToString().Trim()))
                    {
                        reader.Dispose();
                        reader.Close();
                        query = "UPDATE users SET [password]=@password, [updated_at]=GETDATE() WHERE id=@ID";
                        SqlCommand cmd2 = new SqlCommand(query, conn);
                        cmd2.Parameters.AddWithValue("@ID", _userID);
                        cmd2.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(_newPass));
                        cmd2.ExecuteNonQuery();
                        return 1;
                    }
                    else
                    {
                        throw new LoginException("Lỗi: Mật khẩu cũ không chính xác!");
                    }
                }
                throw new LoginException("Lỗi: Không tìm thấy tài khoản của bạn!");
            }
            catch (SqlException)
            {
                throw new LoginException("Lỗi kết nối với cơ sở dữ liệu");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                getClose();
            }
        }
        #endregion

        // *****************************************************************************************

        #region ShopInfo
        public List<ShopInfo> GetShopInfo()
        {
            try
            {
                getConnect();
                string query = "SELECT * FROM shop_info";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                List<ShopInfo> lstShop = new List<ShopInfo>();
                while (reader.Read())
                {
                    ShopInfo shop = new ShopInfo();
                    shop.Id = int.Parse(reader["id"].ToString());
                    shop.Locate = reader["locate"].ToString();
                    shop.HotLine = reader["hotline"].ToString();
                    shop.WebSite = reader["website"].ToString();
                    shop.Email = reader["email"].ToString();
                    shop.OpenTime = reader["open_time"].ToString();
                    shop.OpenDates = reader["open_dates"].ToString();
                    shop.Fb = reader["facebook"].ToString();
                    shop.Ytb = reader["youtube"].ToString();
                    shop.Maps = reader["maps"].ToString();
                    shop.ReviewImg = reader["review_img"].ToString();
                    lstShop.Add(shop);
                }
                return lstShop;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool UpdateShopInfo(ShopInfo _shop)
        {
            try
            {
                getConnect();
                string query = "SELECT * FROM shop_info WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _shop.Id);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imgPath = reader["review_img"].ToString().Trim();
                    query = "UPDATE [dbo].[shop_info] SET [locate] = @locate ,[hotline] = @hotline ,[website] = @website ,[email] = @email ,[open_time] = @open_time ,[open_dates] = @open_dates ,[facebook] = @facebook ,[youtube] = @youtube ,[maps] = @maps, [review_img] = @review_img ,[updated_at] = GETDATE() WHERE [id]=@id";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", _shop.Id);
                    cmd.Parameters.AddWithValue("@locate", _shop.Locate);
                    cmd.Parameters.AddWithValue("@hotline", _shop.HotLine);
                    cmd.Parameters.AddWithValue("@website", _shop.WebSite);
                    cmd.Parameters.AddWithValue("@email", _shop.Email);
                    cmd.Parameters.AddWithValue("@open_time", _shop.OpenTime);
                    cmd.Parameters.AddWithValue("@open_dates", _shop.OpenDates);
                    cmd.Parameters.AddWithValue("@facebook", _shop.Fb);
                    cmd.Parameters.AddWithValue("@youtube", _shop.Ytb);
                    cmd.Parameters.AddWithValue("@maps", _shop.Maps);
                    cmd.Parameters.AddWithValue("@review_img", _shop.ReviewImg == "" ? imgPath : _shop.ReviewImg);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        // Nếu đã có ảnh trên hệ thống và người dùng phải tải ảnh lên
                        if (_shop.ReviewImg != "" && imgPath != "")
                        {
                            this.DeleteFile(imgPath);
                        }
                        return true;
                    }
                    throw new MyCatch("Có lỗi phát sinh khu tiến hành cập nhận dữ liệu!");
                }
                throw new MyCatch("Không tìm thấy dữ liệu của bạn !");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool InsertShopInfo(ShopInfo _shop)
        {
            try
            {
                getConnect();
                string query = @"INSERT INTO [shop_watch].[dbo].[shop_info]
                               ([locate]
                               ,[hotline]
                               ,[website]
                               ,[email]
                               ,[open_time]
                               ,[open_dates]
                               ,[facebook]
                               ,[youtube]
                               ,[maps]
                               ,[review_img]
                               ,[created_at]
                               ,[updated_at])
                         VALUES
                               (@locate
                               ,@hotline
                               ,@website
                               ,@email
                               ,@open_time
                               ,@open_dates
                               ,@facebook
                               ,@youtube
                               ,@maps
                               ,@review_img
                               ,GETDATE()
                               ,GETDATE())
                    ";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@locate", _shop.Locate);
                cmd.Parameters.AddWithValue("@hotline", _shop.HotLine);
                cmd.Parameters.AddWithValue("@website", _shop.WebSite);
                cmd.Parameters.AddWithValue("@email", _shop.Email);
                cmd.Parameters.AddWithValue("@open_time", _shop.OpenTime);
                cmd.Parameters.AddWithValue("@open_dates", _shop.OpenDates);
                cmd.Parameters.AddWithValue("@facebook", _shop.Fb);
                cmd.Parameters.AddWithValue("@youtube", _shop.Ytb);
                cmd.Parameters.AddWithValue("@maps", _shop.Maps);
                cmd.Parameters.AddWithValue("@review_img", _shop.ReviewImg);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                throw new MyCatch("Có lỗi phát sinh khi thêm dữ liệu vào cơ sở dữ liệu, vui lòng thử lại !");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        #endregion

        // *****************************************************************************************

        #region ShopWatch & Chinh sách
        public List<ShopWatch> FetchShopWatch()
        {
            try
            {
                getConnect();
                string query = "SELECT * FROM [dbo].[shop_watch]";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                List<ShopWatch> lstSw = new List<ShopWatch>();
                while (reader.Read())
                {
                    ShopWatch sw = new ShopWatch(
                         int.Parse(reader["id"].ToString())
                        , reader["bao_hiem_shopwatch"].ToString()
                        , int.Parse(reader["bao_hanh_shopwatch"].ToString())
                        , reader["tham_dinh"].ToString()
                        , reader["giao_hang"].ToString()
                        , reader["thoi_gian_bao_hanh"].ToString()
                        );
                    lstSw.Add(sw);
                }
                return lstSw;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
            finally
            {
                this.getClose();
            }
        }
        public bool UpdateShopWatch(ShopWatch sw)
        {
            try
            {
                getConnect();
                string query = "UPDATE [dbo].[shop_watch] SET [bao_hiem_shopwatch] = @baoHiem ,[bao_hanh_shopwatch] = @baoHanh ,[tham_dinh] = @thamDinh ,[giao_hang] = @giaoHang ,[thoi_gian_bao_hanh] = @thoiGian ,[updated_at] = GETDATE() WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@baoHiem", sw.BaoHiem);
                cmd.Parameters.AddWithValue("@baoHanh", sw.BaoHanh);
                cmd.Parameters.AddWithValue("@thamDinh", sw.ThamDinh);
                cmd.Parameters.AddWithValue("@giaoHang", sw.GiaoHang);
                cmd.Parameters.AddWithValue("@thoiGian", sw.ThoiGianBaoHanh);
                cmd.Parameters.AddWithValue("@id", sw.Id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                throw new MyCatch("Có lỗi trong quá trình cập nhật dữ liệu vào cơ sở dữ liệu!");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool InsertShopWatch(ShopWatch _Sw)
        {
            try
            {
                getConnect();
                string query = @"INSERT INTO [dbo].[shop_watch]
                               ([bao_hiem_shopwatch]
                               ,[bao_hanh_shopwatch]
                               ,[tham_dinh]
                               ,[giao_hang]
                               ,[thoi_gian_bao_hanh]
                               ,[created_at]
                               ,[updated_at])
                         VALUES
                               (@bao_hiem_shopwatch
                               ,@bao_hanh_shopwatch
                               ,@tham_dinh
                               ,@giao_hang
                               ,@thoi_gian_bao_hanh
                               ,GETDATE()
                               ,GETDATE())
                    ";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bao_hiem_shopwatch", _Sw.BaoHiem);
                cmd.Parameters.AddWithValue("@bao_hanh_shopwatch", _Sw.BaoHanh);
                cmd.Parameters.AddWithValue("@tham_dinh", _Sw.ThamDinh);
                cmd.Parameters.AddWithValue("@giao_hang", _Sw.GiaoHang);
                cmd.Parameters.AddWithValue("@thoi_gian_bao_hanh", _Sw.ThoiGianBaoHanh);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                throw new MyCatch("Có lỗi phát sinh khi thêm dữ liệu vào cơ sở dữ liệu, vui lòng thử lại !");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public List<ChinhSach> fetchChinhSach(int _id)
        {
            try
            {
                getConnect();
                string query = "SELECT * FROM [dbo].[chinh_sach_bao_hanh] WHERE [shop_watch_id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _id);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                List<ChinhSach> lstChinhSach = new List<ChinhSach>();

                while (reader.Read())
                {
                    object obj = reader["chinh_sach"].ToString();
                    ChinhSach cs = new ChinhSach(
                        int.Parse(reader["id"].ToString())
                        , reader["noi_dung"].ToString()
                        , reader["chinh_sach"].ToString().Contains("True") ? 1 : 0
                        , int.Parse(reader["shop_watch_id"].ToString())
                        );
                    lstChinhSach.Add(cs);
                }
                return lstChinhSach;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public int InsertChinhSach(ChinhSach _chinhSach)
        {
            try
            {
                getConnect();
                string query = "INSERT INTO [dbo].[chinh_sach_bao_hanh] ([noi_dung] ,[chinh_sach] ,[shop_watch_id] ,[created_at] ,[updated_at]) VALUES (@noi_dung ,@chinh_sach ,@shop_watch_id ,GETDATE() ,GETDATE())";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@noi_dung", _chinhSach.NoiDung);
                cmd.Parameters.AddWithValue("@chinh_sach", _chinhSach.ChinhSachApDung == 1 ? true : false);
                cmd.Parameters.AddWithValue("@shop_watch_id", _chinhSach.ShopWatch_id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return 1;
                }
                throw new MyCatch("Có lỗi trong quá trình thêm mới!!!");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool UpdateChinhSach(ChinhSach _cs)
        {
            try
            {
                getConnect();
                string query = @"UPDATE [dbo].[chinh_sach_bao_hanh] SET [noi_dung] = @noi_dung
                                                                      ,[chinh_sach] = @chinh_sach
                                                                      ,[shop_watch_id] = @shop_watch_id
                                                                      ,[updated_at] = GETDATE()
                                                                 WHERE [id]=@id
                                                                ";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@noi_dung", _cs.NoiDung);
                cmd.Parameters.AddWithValue("@chinh_sach", _cs.ChinhSachApDung);
                cmd.Parameters.AddWithValue("@shop_watch_id", _cs.ShopWatch_id);
                cmd.Parameters.AddWithValue("@id", _cs.Id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    throw new MyCatch("Lỗi xảy ra khi cập nhật dữ liệu, vui lòng thử lại !!!");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool DeleteChinhSach(int _id)
        {
            try
            {
                getConnect();
                string query = @"DELETE FROM [dbo].[chinh_sach_bao_hanh]
                                  WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                throw new MyCatch("Có lỗi xảy ra khi xoá, vui lòng thử lại");
            }
            catch (Exception e)
            {
                throw new
                    Exception("Lỗi: " + e.Message);
            }
            finally { this.getClose(); }
        }
        public List<ChinhSach> SearchChinhSach(int _id, string _searchText)
        {
            try
            {
                getConnect();
                string query = @"SELECT * FROM [dbo].[chinh_sach_bao_hanh] WHERE [noi_dung] like N'%'+ @searchText +'%' AND [shop_watch_id] = @shop_watch_id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@searchText", _searchText);
                cmd.Parameters.AddWithValue("@shop_watch_id", _id);
                cmd.ExecuteNonQuery();
                List<ChinhSach> lstChinhSach = new List<ChinhSach>();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ChinhSach cs = new ChinhSach(
                        int.Parse(reader["id"].ToString())
                        , reader["noi_dung"].ToString()
                        , reader["chinh_sach"].ToString().Contains("True") ? 1 : 0
                        , int.Parse(reader["shop_watch_id"].ToString())
                        );
                    lstChinhSach.Add(cs);
                }
                return lstChinhSach;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        #endregion

        #region Open Box Video
        public int InsertNewVideo(string content)
        {
            try
            {
                getConnect();
                string query = @"INSERT INTO [shop_watch].[dbo].[open_box_videos]
                                           ([content]
                                           ,[created_at]
                                           ,[updated_at])
                                     VALUES
                                           (@content
                                           ,GETDATE()
                                           ,GETDATE())";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@content", content);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return 1;
                }
                throw new MyCatch("Có lỗi trong quá trình thêm mới, vui lòng thử lại");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        #endregion

        #region Category
        public List<Category> fetchCategory()
        {
            try
            {
                List<Category> lstCat = new List<Category>();
                getConnect();
                string query = @"SELECT * FROM [dbo].[categories]";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category cat = new Category(
                         int.Parse(reader["id"].ToString())
                        , int.Parse(reader["parentID"].ToString())
                        , int.Parse(reader["order"].ToString())
                        , reader["is_visible"].ToString().Contains("True") ? 1 : 0
                        , reader["name"].ToString()
                        , reader["description"].ToString()
                        );

                    lstCat.Add(cat);
                }
                return lstCat;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public List<Category> fetchLstCategory(int _parentID, string _space)
        {
            List<Category> lstCat = new List<Category>();

            try
            {
                SqlConnection tempConn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnect"].ConnectionString);
                tempConn.Open();
                string query = @"SELECT * FROM [dbo].[categories] WHERE [parentID] = @parentID ORDER BY [parentID],[order]";
                SqlCommand cmd2 = new SqlCommand(query, tempConn);
                cmd2.Parameters.AddWithValue("@parentID", _parentID);
                cmd2.ExecuteNonQuery();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                if (_parentID == -1)
                {
                    _space = "";
                }
                else
                {
                    _space += "|--- ";
                }
                if (!reader2.HasRows)
                {
                    return lstCat;
                }
                else
                {
                    while (reader2.Read())
                    {
                        Category cat = new Category(
                             int.Parse(reader2["id"].ToString())
                            , int.Parse(reader2["parentID"].ToString())
                            , int.Parse(reader2["order"].ToString())
                            , reader2["is_visible"].ToString().Contains("True") ? 1 : 0
                            , _space + reader2["name"].ToString()
                            , reader2["description"].ToString()
                            );
                        lstCat.Add(cat);
                        lstCat.AddRange(this.fetchLstCategory(int.Parse(reader2["id"].ToString()), _space));
                    }
                    tempConn.Dispose();
                    tempConn.Close();
                }
                return lstCat;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
        }
        public string FindParentOrder(string _id)
        {
            try
            {
                getConnect();
                if (_id != "-1")
                {
                    string query = @"SELECT [order] FROM [dbo].[categories] WHERE [ID] = @id";
                    string query2 = @"SELECT count(*) FROM [dbo].[categories] WHERE [parentID] = @id";
                    cmd = new SqlCommand(query2, conn);
                    cmd.Parameters.AddWithValue("@id", _id);
                    Int32 count = (Int32)cmd.ExecuteScalar();
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", _id);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return (int.Parse(reader["order"].ToString()) + count).ToString();
                    }
                    throw new MyCatch("Chú ý: Không còn danh mục này trong CSDL nữa!!!");
                }
                else
                {
                    string query = @"SELECT TOP 1 [order] FROM [dbo].[categories] WHERE [parentID] = @id ORDER BY [order] DESC";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", _id);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return reader["order"].ToString();
                    }
                    return "0";
                }
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool InsertNewCategory(Category _cat)
        {
            try
            {
                getConnect();
                string query = @"INSERT INTO [dbo].[categories]
                           ([name]
                           ,[description]
                           ,[parentID]
                           ,[order]
                           ,[is_visible]
                           ,[created_at]
                           ,[updated_at])
                     VALUES
                           (@name
                           ,@description
                           ,@parentID
                           ,@order
                           ,@is_visible
                           ,GETDATE()
                           ,GETDATE())";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", _cat.Name);
                cmd.Parameters.AddWithValue("@description", _cat.Description);
                cmd.Parameters.AddWithValue("@parentID", _cat.ParentId);
                cmd.Parameters.AddWithValue("@order", _cat.Order);
                cmd.Parameters.AddWithValue("@is_visible", _cat.IsVisible == 1 ? true : false);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                throw new MyCatch("Có lỗi phát sinh khi thêm danh mục, vui lòng thử lại!");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public int SetState(string _id, bool _state)
        {
            try
            {
                getConnect();
                string query = @"UPDATE [dbo].[categories]
   SET [is_visible] = @state
      ,[updated_at] = GETDATE()
 WHERE [id] = @id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@state", _state);
                cmd.Parameters.AddWithValue("@id", _id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return 200;
                }
                throw new MyCatch("Thất bại");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi :" + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool UpdateCategory(Category _cat)
        {
            try
            {
                getConnect();
                string query = @"UPDATE [dbo].[categories]
   SET [name] = @name
,[description] = @description
      ,[parentID] = @parentID
      ,[order] = @order
      ,[updated_at] = GETDATE()
 WHERE [id] =@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@description", _cat.Description);
                cmd.Parameters.AddWithValue("@parentID", _cat.ParentId);
                cmd.Parameters.AddWithValue("@order", _cat.Order);
                cmd.Parameters.AddWithValue("@id", _cat.Id);
                cmd.Parameters.AddWithValue("@name", _cat.Name);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public List<Category> fetchCategoryData()
        {
            try
            {
                getConnect();
                string query = @"SELECT * FROM [dbo].[categories]";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                List<Category> lstCat = new List<Category>();
                while (reader.Read())
                {
                    Category cat = new Category(
                         int.Parse(reader["id"].ToString())
                        , int.Parse(reader["parentID"].ToString())
                        , int.Parse(reader["order"].ToString())
                        , reader["is_visible"].ToString().Contains("True") ? 1 : 0
                        , reader["name"].ToString()
                        , reader["description"].ToString()
                        );
                    cat.CreatedAt = reader["created_at"].ToString();
                    cat.UpdatedAt = reader["updated_at"].ToString();
                    lstCat.Add(cat);
                }
                return lstCat;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            finally { getClose(); }
        }
        public bool DeleteCategory(string _id)
        {
            try
            {
                getConnect();
                string query = @"DELETE FROM [dbo].[categories] WHERE [id] = @id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _id);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                throw new MyCatch("Xoá thất bại");
            }
            catch (Exception e)
            {

                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public List<Category> GetListCategoryByProductId(int _id)
        {
            try
            {
                string query = @"SELECT [c].[ID], [c].[name] FROM product_category pc INNER JOIN categories c ON c.ID=pc.category_id WHERE pc.product_id=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _id);
                List<Category> lstCat = new List<Category>();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstCat.Add(new Category(int.Parse(reader["id"].ToString()), reader["name"].ToString().Trim()));
                }
                return lstCat;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
        }
        #endregion

        #region Brand
        public List<Brand> fetchBrands()
        {
            try
            {
                getConnect();
                string query = @"SELECT [ID]
      ,[name]
      ,[logo]
      ,[description]
      ,[KOL]
      ,[created_at]
      ,[updated_at]
  FROM [dbo].[brands]";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                List<Brand> lstBrand = new List<Brand>();
                while (reader.Read())
                {
                    Brand b = new Brand(
                            int.Parse(reader["id"].ToString())
                            , int.Parse(reader["kol"].ToString())
                            , reader["name"].ToString()
                            , reader["logo"].ToString()
                            , reader["description"].ToString()
                            , reader["created_at"].ToString()
                            , reader["updated_at"].ToString()
                        );
                    lstBrand.Add(b);
                }
                return lstBrand;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public object fetchBrands(int currentPage, int pageSize, string searchText)
        {
            try
            {
                getConnect();
                string query;
                if (searchText == "")
                {
                    query = @"SELECT count(*) FROM [dbo].[brands]";
                    cmd = new SqlCommand(query, conn);
                }
                else
                {
                    query = @"SELECT count(*) FROM [dbo].[brands] WHERE [name] LIKE @searchName";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@searchName", "%" + searchText + "%");
                }


                int totalRows = (int)cmd.ExecuteScalar();
                double totalPage = (double)totalRows / pageSize;
                totalPage = Math.Ceiling(totalPage);
                if (currentPage > totalPage)
                {
                    currentPage = (int)totalPage;
                }
                else if (currentPage < 1)
                {
                    currentPage = 1;
                }
                if (searchText == "")
                {
                    query = @"SELECT * FROM (SELECT *,ROW_NUMBER() OVER (ORDER BY [created_at] DESC) AS RowNum FROM [dbo].[brands] ) AS SOD WHERE SOD.RowNum BETWEEN ((@currentPage-1)*@pageSize)+1 AND @pageSize*(@currentPage)";
                    cmd = new SqlCommand(query, conn);
                }
                else
                {
                    query = @"SELECT * FROM (SELECT *,ROW_NUMBER() OVER (ORDER BY [created_at] DESC) AS RowNum FROM [dbo].[brands] WHERE [name] LIKE @searchName) AS SOD WHERE SOD.RowNum BETWEEN ((@currentPage-1)*@pageSize)+1 AND @pageSize*(@currentPage)";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@searchName", "%" + searchText + "%");
                }
                cmd.Parameters.AddWithValue("@currentPage", currentPage);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.ExecuteNonQuery();
                List<Brand> lstBrands = new List<Brand>();
                List<object> lstResult = new List<object>();
                SqlDataReader r2 = cmd.ExecuteReader();
                while (r2.Read())
                {
                    lstBrands.Add(this.ReaderBrand(r2));
                }
                Pagination page = new Pagination(
                    (int)totalPage
                    , currentPage
                    , pageSize
                    );
                object obj = new
                {
                    lstBrands = lstBrands,
                    pages = page
                };
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool NewBrand(Brand _brand)
        {
            try
            {
                getConnect();
                string query = @"INSERT INTO [dbo].[brands]
                               ([name]
                               ,[logo]
                               ,[description]
                               ,[KOL]
                               ,[created_at]
                               ,[updated_at])
                         VALUES
                               (@name
                               ,@logo
                               ,@description
                               ,@KOL
                               ,GETDATE()
                               ,GETDATE())";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", _brand.Name);
                cmd.Parameters.AddWithValue("@logo", _brand.Logo);
                cmd.Parameters.AddWithValue("@description", _brand.Description);
                cmd.Parameters.AddWithValue("@KOL", _brand.Kol);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                throw new MyCatch("Thêm mới thất bại!");
            }
            catch (Exception e)
            {

                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool DeleteBrand(int _id)
        {
            try
            {
                getConnect();
                string query = @"SELECT [logo] FROM [dbo].[brands] WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _id);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imgPath = reader["logo"].ToString().Trim();
                    query = @"DELETE FROM [brands] WHERE [id]=@id";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", _id);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        // Nếu đã có ảnh trên hệ thống và người dùng phải tải ảnh lên
                        if (imgPath != "")
                        {
                            // Kiểm tra xem ảnh có trên đĩa hay ko nếu có thì xoá đi
                            string localPath = AppDomain.CurrentDomain.BaseDirectory + imgPath;
                            if (File.Exists(localPath))
                            {
                                File.Delete(localPath);
                            }
                        }
                        return true;
                    }
                    throw new MyCatch("Xoá thất bại!!");
                }
                throw new MyCatch("Không tìm thấy dữ liệu");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool EditBrand(Brand _brand)
        {
            try
            {
                getConnect();
                string query = @"SELECT [logo] FROM [dbo].[brands] WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _brand.Id);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imgPath = reader["logo"].ToString().Trim();
                    query = @"UPDATE [dbo].[brands]
                           SET [name] = @name
                              ,[logo] = @logo
                              ,[description] = @description
                              ,[KOL] = @KOL
                              ,[updated_at] = GETDATE()
                         WHERE [id]=@id";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", _brand.Id);
                    cmd.Parameters.AddWithValue("@name", _brand.Name);
                    cmd.Parameters.AddWithValue("@logo", _brand.Logo == "" ? imgPath : _brand.Logo);
                    cmd.Parameters.AddWithValue("@description", _brand.Description);
                    cmd.Parameters.AddWithValue("@KOL", _brand.Kol);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        // Nếu đã có ảnh trên hệ thống và người dùng phải tải ảnh lên
                        if (_brand.Logo != "" && imgPath != "")
                        {
                            this.DeleteFile(imgPath);
                        }
                        return true;
                    }
                    throw new MyCatch("Sửa thất bại!");
                }
                throw new MyCatch("Không tìm thấy dữ liệu");

            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public Brand GetBrand(int _id)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnect"].ConnectionString);
            try
            {
                con.Open();
                string query = @"SELECT * FROM [dbo].[brands] WHERE [ID]=@id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", _id);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return ReaderBrand(reader);
                    }
                }
                return new Brand(-1);
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
        }
        private Brand ReaderBrand(SqlDataReader reader)
        {
            return new Brand(
                            int.Parse(reader["id"].ToString())
                            , int.Parse(reader["kol"].ToString())
                            , reader["name"].ToString()
                            , reader["logo"].ToString()
                            , reader["description"].ToString()
                            , reader["created_at"].ToString()
                            , reader["updated_at"].ToString()
                        );
        }
        public bool UpdateBrandLogo(string _id, string _imgPath)
        {
            try
            {
                getConnect();
                string query = @"SELECT [logo] FROM [dbo].[brands] WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", int.Parse(_id));
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imgPath = reader["logo"].ToString();
                    query = @"UPDATE [dbo].[brands] SET [logo] = @logo, [updated_at] = GETDATE() WHERE [ID] = @ID";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@logo", _imgPath);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        if (imgPath != "")
                        {
                            this.DeleteFile(imgPath);
                        }
                        return true;
                    }
                    throw new MyCatch("Lỗi khi cập nhật");
                }
                throw new MyCatch("Không tìm thấy sản phẩm này");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool DeleteBrandLogo(string _id)
        {
            try
            {
                getConnect();
                string query = @"SELECT [logo] FROM [dbo].[brands] WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", int.Parse(_id));
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imgPath = reader["logo"].ToString();
                    query = @"UPDATE [dbo].[brands] SET [logo] = null, [updated_at] = GETDATE() WHERE [ID] = @ID";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", _id);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        if (imgPath != "")
                        {
                            this.DeleteFile(imgPath);
                        }
                        return true;
                    }
                    throw new MyCatch("Xoá thất bại ");
                }
                throw new MyCatch("Không tìm thấy sản phẩm này");
            }
            finally
            {
                getClose();
            }
        }
        #endregion

        #region Products
        public int GetProductTotal()
        {
            try
            {
                getConnect();
                string query = @"SELECT COUNT(*) FROM [dbo].[products]";
                cmd = new SqlCommand(query, conn);
                int totalProduct = 0;
                totalProduct = (int)cmd.ExecuteScalar();
                return totalProduct;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public int GetProductBrandId(int id)
        {
            try
            {
                getConnect();
                int brandId = -1;
                string query = @"Select brand_id from products where ID = @id ";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                brandId = (int)cmd.ExecuteScalar();
                return brandId;
            }
            catch (Exception e)
            {
                throw new Exception("loi" + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool NewProduct(Product _product)
        {
            try
            {
                getConnect();
                string query = @"INSERT INTO [dbo].[products]
                                   ([is_enable]
                                   ,[name]
                                   ,[cost]
                                   ,[image]
                                   ,[origin]
                                   ,[machine_type]
                                   ,[for_gender]
                                   ,[size]
                                   ,[height]
                                   ,[shell_material]
                                   ,[chain_material]
                                   ,[glasses_material]
                                   ,[functions]
                                   ,[water_resistance_lv]
                                   ,[description]
                                   ,[short_description]
                                   ,[is_out_of_stock]
                                   ,[sale]
                                   ,[brand_id]
                                   ,[created_at]
                                   ,[updated_at])
                             VALUES
                                   (@is_enable
                                   ,@name
                                   ,@cost
                                   ,@image
                                   ,@origin
                                   ,@machine_type
                                   ,@for_gender
                                   ,@size
                                   ,@height
                                   ,@shell_material
                                   ,@chain_material
                                   ,@glasses_material
                                   ,@functions
                                   ,@water_resistance_lv
                                   ,@description
                                   ,@short_description
                                   ,@is_out_of_stock
                                   ,@sale
                                   ,@brand_id
                                   ,GETDATE()
                                   ,GETDATE()); SELECT SCOPE_IDENTITY();";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@is_enable", _product.IsEnable);
                cmd.Parameters.AddWithValue("@name", _product.Name);
                cmd.Parameters.AddWithValue("@cost", _product.Price);
                cmd.Parameters.AddWithValue("@image", _product.Image);
                cmd.Parameters.AddWithValue("@origin", _product.Origin);
                cmd.Parameters.AddWithValue("@machine_type", _product.MachineType);
                cmd.Parameters.AddWithValue("@for_gender", _product.ForGender);
                cmd.Parameters.AddWithValue("@size", _product.Size);
                cmd.Parameters.AddWithValue("@height", _product.Height);
                cmd.Parameters.AddWithValue("@shell_material", _product.ShellMaterial);
                cmd.Parameters.AddWithValue("@chain_material", _product.ChainMaterial);
                cmd.Parameters.AddWithValue("@glasses_material", _product.GlassesMaterial);
                cmd.Parameters.AddWithValue("@functions", _product.Functions);
                cmd.Parameters.AddWithValue("@water_resistance_lv", _product.WaterResistLv);
                cmd.Parameters.AddWithValue("@description", _product.Description);
                cmd.Parameters.AddWithValue("@short_description", _product.ShortDescription);
                cmd.Parameters.AddWithValue("@is_out_of_stock", _product.IsOutOfStock == 0 ? false : true);
                cmd.Parameters.AddWithValue("@sale", _product.Sale);
                cmd.Parameters.AddWithValue("@brand_id", _product.Brand.Id);
                object createdProductObj = cmd.ExecuteScalar();
                if (createdProductObj != null)
                {
                    if (_product.Categories.Count > 0)
                    {
                        int productId = int.Parse(createdProductObj.ToString());
                        query = @"INSERT INTO [dbo].[product_category]
                               ([product_id]
                               ,[category_id]
                               ,[created_at]
                               ,[updated_at])
                         VALUES
                               (@product_id
                               ,@category_id
                               ,GETDATE()
                               ,GETDATE())";
                        foreach (Category cat in _product.Categories)
                        {
                            cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@product_id", productId);
                            cmd.Parameters.AddWithValue("@category_id", cat.Id);
                            if (cmd.ExecuteNonQuery() == 0)
                                throw new MyCatch("Thêm vào danh mục lỗi, id danh mục:" + cat.Id);
                        }
                    }
                    return true;
                }
                throw new MyCatch("Thêm thất bại");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public List<Product> GetProductByBrandId(string id)
        {
            try
            {
                getConnect();
                string query = @"Select * from products where brand_id = @brand_id ";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@brand_id", id);
                cmd.ExecuteNonQuery();
                SqlDataReader r3 = cmd.ExecuteReader();
                List<Product> lstProducts = new List<Product>();
                while (r3.Read())
                {
                    lstProducts.Add(this.ReaderProduct(r3));
                }
                return lstProducts;
            }catch(Exception e)
            {
                throw new Exception("loi" + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public List<Product> FetchProductsNonPaginate()
        {
            try
            {
                getConnect();
                string query = @"SELECT * FROM [dbo].[products]";
                cmd = new SqlCommand(query, conn);
                SqlDataReader r2 = cmd.ExecuteReader();
                List<Product> lstProducts = new List<Product>();
                while (r2.Read())
                {
                    lstProducts.Add(this.ReaderProduct(r2));
                }
                return lstProducts;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public object FetchProducts(int currentPage, int pageSize, string searchText)
        {
            try
            {
                getConnect();
                string query;
                if (searchText == "")
                {
                    query = @"SELECT count(*) FROM [dbo].[products]";
                    cmd = new SqlCommand(query, conn);
                }
                else
                {
                    query = @"SELECT count(*) FROM [dbo].[products] WHERE [name] LIKE @searchName";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@searchName","%"+searchText+"%");
                }
                
                
                int totalRows = (int)cmd.ExecuteScalar();
                double totalPage =(double) totalRows / pageSize;
                totalPage = Math.Ceiling(totalPage);
                if (currentPage > totalPage)
                {
                    currentPage = (int)totalPage;
                }
                else if (currentPage < 1)
                {
                    currentPage = 1;
                }
                if (searchText == "")
                {
                    query = @"SELECT * FROM (SELECT *,ROW_NUMBER() OVER (ORDER BY [created_at] DESC) AS RowNum FROM products ) AS SOD WHERE SOD.RowNum BETWEEN ((@currentPage-1)*@pageSize)+1 AND @pageSize*(@currentPage)";
                    cmd = new SqlCommand(query, conn);
                }
                else
                {
                    query = @"SELECT * FROM (SELECT *,ROW_NUMBER() OVER (ORDER BY [created_at] DESC) AS RowNum FROM products WHERE [name] LIKE @searchName) AS SOD WHERE SOD.RowNum BETWEEN ((@currentPage-1)*@pageSize)+1 AND @pageSize*(@currentPage)";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@searchName", "%" + searchText + "%");
                }
                cmd.Parameters.AddWithValue("@currentPage", currentPage);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.ExecuteNonQuery();
                List<Product> lstProducts = new List<Product>();
                List<object> lstResult = new List<object>();
                SqlDataReader r2 = cmd.ExecuteReader();
                while (r2.Read())
                {
                    lstProducts.Add(this.ReaderProduct(r2));
                }
                Pagination page = new Pagination(
                    (int)totalPage
                    ,currentPage
                    ,pageSize
                    );
                object obj = new
                {
                    lstProducts = lstProducts,
                    pages = page
                };
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public Product GetProduct(int _id)
        {
            try
            {
                getConnect();
                string query = @"SELECT * FROM [dbo].[products] WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _id);
                cmd.ExecuteNonQuery();
                SqlDataReader _reader = cmd.ExecuteReader();
                Product product = new Product();
                while (_reader.Read())
                {
                    product = this.ReaderProduct(_reader);
                }
                return product;
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool UpdateProduct(Product _product)
        {
            
            try
            {
                getConnect();
                string query = @"SELECT [image] FROM [products] WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _product.Id);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    string imgPath = reader["image"].ToString();
                    query = @"UPDATE [products] SET [is_enable] = @is_enable, [name] = @name, [cost] = @cost, [image] = @image, [origin] = @origin, [machine_type] = @machine_type, [for_gender] = @for_gender, [size] = @size, [height] = @height, [shell_material] = @shell_material, [chain_material] = @chain_material, [glasses_material] = @glasses_material, [functions] = @functions, [water_resistance_lv] = @water_resistance_lv, [description] = @description, [short_description] = @short_description, [is_out_of_stock] = @is_out_of_stock, [sale] = @sale, [brand_id] = @brand_id, [updated_at] = GETDATE() WHERE [ID] = @ID";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@is_enable", _product.IsEnable);
                    cmd.Parameters.AddWithValue("@name", _product.Name);
                    cmd.Parameters.AddWithValue("@cost", _product.Price);
                    cmd.Parameters.AddWithValue("@image", _product.Image == "" ? imgPath : _product.Image);
                    cmd.Parameters.AddWithValue("@origin", _product.Origin);
                    cmd.Parameters.AddWithValue("@machine_type", _product.MachineType);
                    cmd.Parameters.AddWithValue("@for_gender", _product.ForGender);
                    cmd.Parameters.AddWithValue("@size", _product.Size);
                    cmd.Parameters.AddWithValue("@height", _product.Height);
                    cmd.Parameters.AddWithValue("@shell_material", _product.ShellMaterial);
                    cmd.Parameters.AddWithValue("@chain_material", _product.ChainMaterial);
                    cmd.Parameters.AddWithValue("@glasses_material", _product.GlassesMaterial);
                    cmd.Parameters.AddWithValue("@functions", _product.Functions);
                    cmd.Parameters.AddWithValue("@water_resistance_lv", _product.WaterResistLv);
                    cmd.Parameters.AddWithValue("@description", _product.Description);
                    cmd.Parameters.AddWithValue("@short_description", _product.ShortDescription);
                    cmd.Parameters.AddWithValue("@is_out_of_stock", _product.IsOutOfStock == 0 ? false : true);
                    cmd.Parameters.AddWithValue("@sale", _product.Sale);
                    cmd.Parameters.AddWithValue("@brand_id", _product.Brand.Id);
                    cmd.Parameters.AddWithValue("@ID", _product.Id);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        query = @"DELETE FROM [product_category] WHERE [product_id] = @ID";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", _product.Id);
                        cmd.ExecuteNonQuery();
                        if (_product.Categories.Count > 0)
                        {
                            query = @"INSERT INTO [dbo].[product_category]
                               ([product_id]
                               ,[category_id]
                               ,[created_at]
                               ,[updated_at])
                         VALUES
                               (@product_id
                               ,@category_id
                               ,GETDATE()
                               ,GETDATE())";
                            foreach (Category cat in _product.Categories)
                            {
                                cmd = new SqlCommand(query, conn);
                                cmd.Parameters.AddWithValue("@product_id", _product.Id);
                                cmd.Parameters.AddWithValue("@category_id", cat.Id);
                                if (cmd.ExecuteNonQuery() == 0)
                                    throw new MyCatch("Thêm vào danh mục lỗi, id danh mục:" + cat.Id);
                            }
                            // Nếu đã có ảnh trên hệ thống và người dùng phải tải ảnh lên
                            if (_product.Image != "" && imgPath != "")
                            {
                                // Kiểm tra xem ảnh có trên đĩa hay ko nếu có thì xoá đi
                                string localPath = AppDomain.CurrentDomain.BaseDirectory + imgPath;
                                if (File.Exists(localPath))
                                {
                                    File.Delete(localPath);
                                }
                            }
                        }
                        return true;
                    }
                    throw new MyCatch("Cập nhật thất bại");
                }
                throw new MyCatch("Cập nhật thất bại");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool DeleteAProduct(string _id)
        {
            try
            {
                getConnect();
                string query = @"SELECT [image] FROM [dbo].[products] WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", _id);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imgPath = reader["image"].ToString();
                    query = @"DELETE FROM [dbo].[products] WHERE [id]=@id";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", _id);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        query = @"DELETE FROM [product_category] WHERE [product_id] = @ID";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", _id);
                        cmd.ExecuteNonQuery();
                        // Nếu đã có ảnh trên hệ thống và người dùng phải tải ảnh lên
                        if (imgPath != "")
                        {
                            // Kiểm tra xem ảnh có trên đĩa hay ko nếu có thì xoá đi
                            string localPath = AppDomain.CurrentDomain.BaseDirectory + imgPath;
                            if (File.Exists(localPath))
                            {
                                File.Delete(localPath);
                            }
                        }
                        return true;
                    }
                    throw new Exception("Xoá thất bại");
                }
                throw new Exception("Không tìm thấy sản phẩm này");
            }
            catch (Exception e)
            {

                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public void SetStatusProduct(string _id, int actionType)
        {
            try
            {
                getConnect();
                string query = @"UPDATE [dbo].[products] SET [is_enable] = @is_enable ,[updated_at] = GETDATE() WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                if (actionType == ENABLE)
                {
                    cmd.Parameters.AddWithValue("@is_enable", true);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@is_enable", false);
                }
                cmd.Parameters.AddWithValue("@id", _id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool UpdateProductImage(string _id, string _imgPath)
        {
            try
            {
                getConnect();
                string query = @"SELECT [image] FROM [products] WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", int.Parse(_id));
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imgPath = reader["image"].ToString();
                    query = @"UPDATE [products] SET [image] = @image, [updated_at] = GETDATE() WHERE [ID] = @ID";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@image", _imgPath);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        if (imgPath != "")
                        {
                            this.DeleteFile(imgPath);
                        }
                        return true;
                    }
                    throw new MyCatch("Lỗi khi cập nhật");
                }
                throw new MyCatch("Không tìm thấy sản phẩm này");
            }
            catch (Exception e)
            {
                throw new Exception("Lỗi: " + e.Message);
            }
            finally
            {
                getClose();
            }
        }
        public bool DeleteProductImage(string _id)
        {
            try
            {
                getConnect();
                string query = @"SELECT [image] FROM [products] WHERE [id]=@id";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", int.Parse(_id));
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string imgPath = reader["image"].ToString();
                    query = @"UPDATE [products] SET [image] = null, [updated_at] = GETDATE() WHERE [ID] = @ID";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", _id);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        if (imgPath != "")
                        {
                            this.DeleteFile(imgPath);
                        }
                        return true;
                    }
                    throw new MyCatch("Xoá thất bại ");
                }
                throw new MyCatch("Không tìm thấy sản phẩm này");
            }
            finally
            {
                getClose();
            }
        }
        public Product ReaderProduct(SqlDataReader r)
        {
            Product product = new Product();
            product.Id = int.Parse(r["id"].ToString().Trim());
            product.IsEnable = bool.Parse(r["is_enable"].ToString());
            product.Name = r["name"].ToString().Trim();
            /*
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                Value.toString(nfi);
            */
            product.Price = double.Parse(r["cost"].ToString());
            product.Image = r["image"].ToString();
            product.Origin = r["origin"].ToString();
            product.MachineType = r["machine_type"].ToString();
            product.ForGender = int.Parse(r["for_gender"].ToString().Trim());
            product.Size = r["size"].ToString().Trim();
            product.Height = r["height"].ToString().Trim();
            product.ShellMaterial = r["shell_material"].ToString().Trim();
            product.ChainMaterial = r["chain_material"].ToString().Trim();
            product.GlassesMaterial = r["glasses_material"].ToString().Trim();
            product.Functions = r["functions"].ToString().Trim();
            product.WaterResistLv = r["water_resistance_lv"].ToString().Trim();
            product.Description = r["description"].ToString().Trim();
            product.ShortDescription = r["short_description"].ToString().Trim();
            product.IsOutOfStock = r["is_out_of_stock"].ToString().Trim().Contains("True") ? 1 : 0;
            product.Sale = float.Parse(r["sale"].ToString().Trim());
            product.UpdatedAt = r["updated_at"].ToString();
            product.CreatedAt = r["created_at"].ToString();
            product.Brand = this.GetBrand(int.Parse(r["brand_id"].ToString().Trim()));
            product.Categories = this.GetListCategoryByProductId(product.Id);
            return product;
        }
        #endregion

        private void DeleteFile(string filePath)
        {
            // Kiểm tra xem file trên đĩa hay ko nếu có thì xoá đi
            string localPath = AppDomain.CurrentDomain.BaseDirectory + filePath;
            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }
        }
    }
}
