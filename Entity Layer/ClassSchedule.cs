using Microsoft.EntityFrameworkCore;

namespace Entity_Layer
{
    [Keyless]
    public class ClassSchedule
    {
        public string Code {  get; set; }
        public string Name { get; set; }
        public string ScheduleInfo { get; set; }
    }
}
