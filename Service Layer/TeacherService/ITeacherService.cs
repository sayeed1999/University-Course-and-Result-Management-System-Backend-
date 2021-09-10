using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.TeacherService
{
    public interface ITeacherService
    {
        public Task<ServiceResponse<Teacher>> SaveTeacher(Teacher teacher); // Story 04
    }
}
