using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class MenuRole // the table for establishing many-to-many relationship between menu & identityrole
    {
        public long Id { get; set; }
        [ForeignKey("MenuId")]
        public Menu Menu {  get; set; }
        public long MenuId {  get; set; }
        [ForeignKey("RoleId")]
        public IdentityRole Role { get; set; }
        public string RoleId { get; set; }
    }
}
