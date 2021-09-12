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
        public ITeacherRepository Teachers { get; private set; }
        public IDesignationRepository Designations { get; private set; }
        public IStudentRepository Students { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public IDayRepository Days { get; private set; }
        public IGradeRepository Grades { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Departments = new DepartmentRepository(dbContext);
            Courses = new CourseRepository(dbContext);
            Semisters = new SemisterRepository(dbContext);
            Teachers = new TeacherRepository(dbContext);
            Designations = new DesignationRepository(dbContext);
            Students = new StudentRepository(dbContext);
            Rooms = new RoomRepository(dbContext);
            Days = new DayRepository(dbContext);
            Grades = new GradeRepository(dbContext);
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
