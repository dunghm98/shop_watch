using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOs
{
    public class Category
    {
        #region Fields
        int _id, _parentId, _order, _isVisible;
        string _name, _description, _createdAt, _updatedAt;

        public string UpdatedAt
        {
            get { return _updatedAt; }
            set { _updatedAt = value; }
        }

        public string CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region constructor
        public Category()
        {
            
        }
        public Category(int _id)
        {
            this.Id = _id;
        }
        public Category(int _id, string _name)
        {
            this.Id = _id;
            this.Name = _name;
        }
        public Category(int _id, int _parentId, int _order, int _isVisible, string _name, string _description)
        {
            this.Id = _id;
            this.ParentId = _parentId;
            this.Order = _order;
            this.IsVisible = _isVisible;
            this.Name = _name;
            this.Description = _description;
        }
        #endregion
    }
}
