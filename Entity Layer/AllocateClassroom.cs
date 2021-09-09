using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class AllocateClassroom
    {
        public long Id {  get; set; }
        public Room Room { get; set; }
        public long RoomId { get; set; }
        public Department Department { get; set; }
        public long DepartmentId { get; set; }
        public Course Course { get; set; }
        public long CourseId {  get; set; }
        public Day Day {  get; set; }
        public long DayId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
