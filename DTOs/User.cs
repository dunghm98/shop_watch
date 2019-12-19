using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCrypt.Net;

namespace DTOs
{
    public class User
    {
        private int _id, _role;
        private string _userName, _email, _phone, _name, _password;

        public string Password
        {
            get { return _password; }
            set { _password = BCrypt.Net.BCrypt.HashPassword(value); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public int Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public User()
        {
            this.Id = 0;
            this.UserName = null;
            this.Email = null;
            this.Phone = null;
            this.Name = null;
            this.Role = 0;
        }

        public User(int _id, string _userName, string _email, string _phone, string _name, string _password, int _role)
        {
            this.Id = _id;
            this.UserName = _userName;
            this.Email = _email;
            this.Phone = _phone;
            this.Name = _name;
            this.Password = _password;
            this.Role = _role;
        }
    }
}
