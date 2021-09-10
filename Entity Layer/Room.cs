using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Room
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<AllocateClassroom> AllocateClassrooms { get; set; }
        public ICollection<AllocateClassroomHistory> AllocateClassroomHistories { get; set; }
    }
}
