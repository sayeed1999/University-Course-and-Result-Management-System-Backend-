using Data_Access_Layer;
using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using Service_Layer.DepartmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.StudentService
{
    public class StudentService : Repository<Student>, IStudentService
    {
        private readonly IDepartmentService _departmentService;
        public StudentService(ApplicationDbContext dbContext, IDepartmentService departmentService) : base(dbContext)
        {
            _departmentService = departmentService;
        }

        public async Task<ServiceResponse<StudentCourse>> EnrollStudentInCourse(StudentCourse data)
        {
            var serviceResponse = new ServiceResponse<StudentCourse>();
            serviceResponse.Data = data;
            try
            {
                _dbContext.StudentsCourses.Add(data);
                await _dbContext.SaveChangesAsync();
                serviceResponse.Message = "Successfully enrolled.";
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Student>> RegisterStudent(StudentRegistration student)
        {
            var serviceResponse = new ServiceResponse<Student>();

            // Creating Registration number

            string reg = "";
            var response01 = await _departmentService.GetById(student.DepartmentId);
            if(!response01.Success)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = response01.Message;
                return serviceResponse;
            }
            reg += response01.Data.Code + '-';
            reg += student.Date.Year.ToString() + '-';
            int countOfStudents = await _dbContext.Students.CountAsync(x => x.DepartmentId == student.DepartmentId) + 1;
            if (countOfStudents / 10 == 0) reg += "00";
            else if (countOfStudents / 10 < 10) reg += "0";
            reg += countOfStudents.ToString();

            // Registration Number creating ends..

            var newStudent = new Student
            {
                Id = 0,
                Name = student.Name,
                Email = student.Email,
                Contact = student.Contact,
                Address = student.Address,
                Date = student.Date,
                DepartmentId = student.DepartmentId,
                RegistrationNumber = reg
            };
            return await this.Add(newStudent);
        }
    }
}
