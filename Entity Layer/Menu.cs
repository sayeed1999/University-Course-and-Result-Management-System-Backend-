using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Menu
    {
        public int Id {  get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url {  get; set; }
        [Required]
        public Status StatusId { get; set; } = Status.Inactive;
        public Menu? Parent {  get; set; }
        public int? ParentId { get; set; } = null;
    }

    public enum Status
    {
        Active, // 0
        Inactive, // 1
    }
}
