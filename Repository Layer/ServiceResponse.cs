using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer
{
    public class ServiceResponse<T> where T : class
    {
        public T Data { get; set; }
        public string Message { get; set; } = "Operation done successfully";
        public bool Success { get; set; } = true;
    }
}
