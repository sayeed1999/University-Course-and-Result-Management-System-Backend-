using Data_Access_Layer;
using Repository_Layer.Child_Repositories;
using System;
using System.Threading.Tasks;

namespace Repository_Layer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public IDepartmentRepository Departments { get; private set; }
        public ICourseRepository Courses {  get; private set; }
        public ISemisterRepository Semisters {  get; private set; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Departments = new DepartmentRepository(dbContext);
            Courses = new CourseRepository(dbContext);
            Semisters = new SemisterRepository(dbContext);
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
