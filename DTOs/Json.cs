using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTOs
{
    public class Json
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public Json()
        {
        }
    }
}
