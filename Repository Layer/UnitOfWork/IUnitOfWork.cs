using Data_Access_Layer;
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
        IDepartmentRepository DepartmentRepository { get; }
        ICourseRepository CourseRepository { get; }
        IClassScheduleRepository ClassScheduleRepository { get; }
        ICourseHistoryRepository CourseHistoryRepository { get; }
        ISemisterRepository SemisterRepository {  get; }
        ITeacherRepository TeacherRepository { get; }
        IDesignationRepository DesignationRepository { get; }
        IStudentRepository StudentRepository { get; }
        IStudentCourseRepository StudentCourseRepository { get; }
        IStudentCourseHistoryRepository StudentCourseHistoryRepository { get; }
        IRoomRepository RoomRepository { get; }
        IAllocateClassroomRepository AllocateClassroomRepository { get; }
        IAllocateClassroomHistoryRepository AllocateClassroomHistoryRepository { get; }
        IDayRepository DayRepository { get; }
        IGradeRepository GradeRepository { get; }
        IMenuRepository MenuRepository { get; }
        IMenuWiseRolePermissionRepository MenuWiseRolePermissionRepository { get; }
        Task CompleteAsync();


        // Another style:-
        // 
        // void CreateTransaction();
        // void Commit();
        // void Rollback();
        // void Save();
    }
}
