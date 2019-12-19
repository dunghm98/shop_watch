using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOs
{
    public class Brand
    {
        #region Fields
        int _id, _kol;
        string _name, _logo, _description, _created_at, _updated_at;

        public string Updated_at
        {
            get { return _updated_at; }
            set { _updated_at = value; }
        }

        public string Created_at
        {
            get { return _created_at; }
            set { _created_at = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Kol
        {
            get { return _kol; }
            set { _kol = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion
        #region Constructor
        public Brand()
        {
        }
        public Brand(int _id)
        {
            this.Id = _id;
        }
        public Brand(int _id, int _kol, string _name, string _logo, string _description, string _created_at, string _updated_at)
        {
            this.Id = _id;
            this.Kol = _kol;
            this.Name = _name;
            this.Logo = _logo;
            this.Description = _description;
            this.Created_at = _created_at;
            this.Updated_at = _updated_at;
        }
        #endregion
    }
}
