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
        public Room? Room { get; set; }
        public string RoomId { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public Course? Course { get; set; }
        public string CourseCode { get; set; }
        public Day? Day { get; set; }
        public string DayId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public UnallocatingRoomsCount UnallocatingRoomsCount { get; set; }
        public int UnallocatingRoomsCountId { get; set; }

    }
}
