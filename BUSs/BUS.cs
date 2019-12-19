using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALs;
using DTOs;

namespace BUSs
{
    public class BUS
    {
        DAL dal = new DAL();

        public User getLogin(string _email, string _password)
        {
            try
            {
                User user = dal.checkLogin(_email, _password);
                return user;
            }
            catch (LoginException e)
            {
                throw new LoginException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int UpdateUserPassword(string _oldPass, string _newPass, string _userId)
        {
            try
            {
                return dal.UpdateUserPassword(_userId, _oldPass, _newPass);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #region ShopInfo
        public List<ShopInfo> GetShopInfo()
        {
            try
            {
                return dal.GetShopInfo();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool UpdateShopInfo(ShopInfo _shop)
        {
            try
            {
                return dal.UpdateShopInfo(_shop);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertShopInfo(ShopInfo _shop)
        {
            try
            {
                return dal.InsertShopInfo(_shop);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        // *****

        #region ShopWactch & CHinh Sach
        public List<ShopWatch> FetchShopWatch()
        {
            try
            {
                return this.dal.FetchShopWatch();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool UpdateShopWatch(ShopWatch _sw)
        {
            try
            {
                return dal.UpdateShopWatch(_sw);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool InsertShopWatch(ShopWatch _sw)
        {
            try
            {
                return dal.InsertShopWatch(_sw);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ChinhSach> fetchChinhSach(int _id)
        {
            try
            {
                return dal.fetchChinhSach(_id);
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public int InsertChinhSach(ChinhSach _cs)
        {
            try
            {
                return dal.InsertChinhSach(_cs);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool UpdateChinhSach(ChinhSach _cs)
        {
            try
            {
                return dal.UpdateChinhSach(_cs);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteChinhSach(int _id)
        {
            try
            {
                return dal.DeleteChinhSach(_id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ChinhSach> SearchChinhSach(int _id, string _searchText)
        {
            try
            {
                return dal.SearchChinhSach(_id, _searchText);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Open Box Videos
        public int InsertNewVideo(string txtContent)
        {
            try
            {
                return dal.InsertNewVideo(txtContent);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Category
        public List<Category> fetchCategory()
        {
            try
            {
                return dal.fetchCategory();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Category> fetchLstCategory(int _parentID, string _space)
        {
            try
            {
                 return dal.fetchLstCategory(_parentID, _space);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string FindParentOrder(string _id)
        {
            try
            {
                return dal.FindParentOrder(_id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool InsertNewCategory(Category _cat)
        {
            try
            {
                return dal.InsertNewCategory(_cat);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int SetState(string _id, bool _state)
        {
            try
            {
                return dal.SetState(_id, _state);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool UpdateCategory(Category _cat)
        {
            try
            {
                return dal.UpdateCategory(_cat);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Category> fetchCategoryData()
        {
            try
            {
                return dal.fetchCategoryData();
            }
            catch (Exception e){
                throw new Exception(e.Message);
            }
        }
        public bool DeleteCategory(string _id)
        {
            try
            {
                return dal.DeleteCategory(_id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Brands
        public List<Brand> fetchBrands()
        {
            try
            {
                return dal.fetchBrands();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public object fetchBrands(int currentPage, int pageSize, string searchText)
        {
            try
            {
                return dal.fetchBrands(currentPage, pageSize, searchText);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool NewBrand(Brand _brand)
        {
            try
            {
                return dal.NewBrand(_brand);
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
        public bool DeleteBrand(int _id)
        {
            try
            {
                return dal.DeleteBrand(_id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool DeleteBrands(string[] _ids)
        {
            try
            {
                foreach (string id in _ids)
                {
                    if (this.DeleteBrand(int.Parse(id))) { }
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Brand GetBrand(int id)
        {
            try
            {
                return dal.GetBrand(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool EditBrand(Brand _brand)
        {
            try
            {
                return dal.EditBrand(_brand);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool UpdateBrandLogo(string _id, string _imgPath)
        {
            try
            {
                return dal.UpdateBrandLogo(_id, _imgPath);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool DeleteBrandLogo(string _id)
        {
            try
            {
                return dal.DeleteBrandLogo(_id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region Products
        public int GetProductTotal()
        {
            try
            {
                return dal.GetProductTotal();
            }
            catch (Exception e)
            {
                throw new
                Exception(e.Message);
            }
            
        }
        public int GetBrandId(int id)
        {
            try
            {
                return dal.GetProductBrandId(id);
            }
            catch (Exception e)
            {
                throw new
                Exception(e.Message);
            }
        }
        public bool NewProduct(Product _product)
        {
            try
            {
                return dal.NewProduct(_product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public object FetchProducts(int currentPage, int pageSize, string searchText)
        {
            try
            {
                return dal.FetchProducts(currentPage, pageSize, searchText);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Product GetProduct(int _id)
        {
            try
            {
                return dal.GetProduct(_id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool UpdateProduct(Product _product)
        {
            try
            {
                return dal.UpdateProduct(_product);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool DeleteAProduct(string _id)
        {
            try
            {
                return dal.DeleteAProduct(_id);
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
        public bool DeleteProducts(string[] _ids)
        {
            try
            {
                foreach (string id in _ids)
                {
                    if (this.DeleteAProduct(id))
                    {
                        
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool SetStatusProduct(string[] _ids, int actionType)
        {
            try
            {
                foreach (string id in _ids)
                {
                    dal.SetStatusProduct(id, actionType);
                }
                return true;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
        public bool UpdateProductImage(string _id, string _imgPath)
        {
            try
            {
                return dal.UpdateProductImage(_id, _imgPath);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool DeleteProductImage(string _id)
        {
            try
            {
                return dal.DeleteProductImage(_id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
    }
}
