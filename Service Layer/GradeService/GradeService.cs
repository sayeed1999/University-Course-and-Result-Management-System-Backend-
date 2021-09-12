using Entity_Layer;
using Repository_Layer;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.GradeService
{
    public class GradeService : IGradeService
    {
        private readonly IUnitOfWork unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<Grade>>> GetGrades()
        {
            return await unitOfWork.Grades.GetAll();
        }
    }
}
