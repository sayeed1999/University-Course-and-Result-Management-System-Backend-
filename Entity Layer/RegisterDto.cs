using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class RegisterDto
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string? UserName { get; set; }

        // password, confirm password, username...
        public List<string> Roles { get; set; } = new List<string>();
    }
}
