using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Layer
{
    public class AppSettings
    {
        public string JWT_Secret_Key {  get; set; }
        public string Client_URL {  get; set; }
    }
}
