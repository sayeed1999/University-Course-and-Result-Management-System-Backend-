using Entity_Layer;
using Repository_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.GradeService
{
    public interface IGradeService : IRepository<GradeLetter>
    {
    }
}
