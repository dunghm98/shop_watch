using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DALs
{
    public class LoginException : ApplicationException
    {
        public LoginException(String message)
            : base(message)
        {
            
        }
    }
}
