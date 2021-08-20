using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Room
    {
        public Room(string Id)
        {
            this.Id = Id;
        }
        public string Id { get; set; }
        public ICollection<AllocateClassroom> AllocateClassrooms { get; set; }
    }
}
