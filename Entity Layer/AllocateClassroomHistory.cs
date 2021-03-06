using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class AllocateClassroomHistory
    {
        public long Id { get; set; }
        public Room Room { get; set; }
        public long RoomId { get; set; }
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public Course Course { get; set; }
        public long CourseId { get; set; }
        public Day Day { get; set; }
        public long DayId { get; set; }
        public float From { get; set; }
        public float To { get; set; }
        public long NthHistory { get; set; }
    }
}
