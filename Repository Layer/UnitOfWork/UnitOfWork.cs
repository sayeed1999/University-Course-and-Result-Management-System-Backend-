using Data_Access_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Child_Repositories;
using System;
using System.Threading.Tasks;

namespace Repository_Layer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IDepartmentRepository DepartmentRepository { get; private set; }
        public ICourseRepository CourseRepository {  get; private set; }
        public ICourseHistoryRepository CourseHistoryRepository { get; private set; }
        public ISemisterRepository SemisterRepository {  get; private set; }
        public ITeacherRepository TeacherRepository { get; private set; }
        public IDesignationRepository DesignationRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public IStudentCourseRepository StudentCourseRepository { get; private set; }
        public IStudentCourseHistoryRepository StudentCourseHistoryRepository { get; private set; }
        public IRoomRepository RoomRepository { get; private set; }
        public IAllocateClassroomRepository AllocateClassroomRepository { get; private set; }
        public IAllocateClassroomHistoryRepository AllocateClassroomHistoryRepository { get; private set; }
        public IDayRepository DayRepository { get; private set; }
        public IGradeRepository GradeRepository { get; private set; }
        public IMenuRepository MenuRepository {  get; private set; }
        public IMenuWiseRolePermissionRepository MenuWiseRolePermissionRepository { get; private set; }
        public UnitOfWork(
            ApplicationDbContext dbContext,
            IDepartmentRepository departmentRepository,
            ICourseRepository courseRepository,
            ICourseHistoryRepository courseHistoryRepository,
            ISemisterRepository semisterRepository,
            ITeacherRepository teacherRepository,
            IDesignationRepository designationRepository,
            IStudentRepository studentRepository,
            IStudentCourseRepository studentCourseRepository,
            IStudentCourseHistoryRepository studentCourseHistoryRepository,
            IRoomRepository roomRepository,
            IAllocateClassroomRepository allocateClassroomRepository,
            IAllocateClassroomHistoryRepository allocateClassroomHistoryRepository,
            IDayRepository dayRepository,
            IGradeRepository gradeRepository,
            IMenuRepository menuRepository,
            IMenuWiseRolePermissionRepository menuWiseRolePermissionRepository
        ) {
            _dbContext = dbContext;
            DepartmentRepository = departmentRepository;
            CourseRepository = courseRepository;
            CourseHistoryRepository = courseHistoryRepository;
            SemisterRepository = semisterRepository;
            TeacherRepository = teacherRepository;
            DesignationRepository = designationRepository;
            StudentRepository = studentRepository;
            StudentCourseRepository = studentCourseRepository;
            StudentCourseHistoryRepository = studentCourseHistoryRepository;
            RoomRepository = roomRepository;
            AllocateClassroomRepository = allocateClassroomRepository;
            AllocateClassroomHistoryRepository = allocateClassroomHistoryRepository;
            DayRepository = dayRepository;
            GradeRepository = gradeRepository;
            MenuRepository = menuRepository;
            MenuWiseRolePermissionRepository = menuWiseRolePermissionRepository;
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        //public void Dispose()
        //{
        //    _dbContext.Dispose();
        //}
    }
}
