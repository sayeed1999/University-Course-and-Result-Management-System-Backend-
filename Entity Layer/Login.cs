using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Login
    {
        public string Email {  get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
