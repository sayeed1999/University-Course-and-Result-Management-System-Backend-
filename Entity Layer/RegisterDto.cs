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

        // password, confirm password, username...
        public ICollection<string> Roles { get; set; } = new HashSet<string>();

    }
}
