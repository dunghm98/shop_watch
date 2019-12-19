using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALs;
using DTOs;

namespace BUSs
{
    public class Madu_BUS
    {
        DAL dal = new DAL();
        #region Products
        public List<Product> FetchProductsNonPaginate()
        {
            try
            {
                return dal.FetchProductsNonPaginate();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Product> GetProductByBrandId(string id)
        {
            try
            {
                return dal.GetProductByBrandId(id);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
    }

    
}
