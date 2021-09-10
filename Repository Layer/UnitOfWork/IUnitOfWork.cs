using Microsoft.EntityFrameworkCore;
using Repository_Layer.Child_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDepartmentRepository Departments { get; }
        ICourseRepository Courses { get; }
        ISemisterRepository Semisters {  get; }
        ITeacherRepository Teachers { get; }
        IDesignationRepository Designations { get; }
        Task CompleteAsync();


        // Another style:-
        // 
        // void CreateTransaction();
        // void Commit();
        // void Rollback();
        // void Save();
    }
}
