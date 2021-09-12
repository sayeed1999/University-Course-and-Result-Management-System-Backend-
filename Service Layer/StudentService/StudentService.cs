using Entity_Layer;
using Microsoft.EntityFrameworkCore;
using Repository_Layer;
using Repository_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Layer.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<IEnumerable<Student>>> GetAll(string regNum = "")
        {
            return await _unitOfWork.Students.GetAll(regNum);
        }

        public async Task<ServiceResponse<Student>> GetStudentByRegNum(string regNum)
        {
            return await _unitOfWork.Students.GetStudentByRegNum(regNum);
        }

        public async Task<ServiceResponse<Student>> GetStudentByEmail(string email)
        {
            return await _unitOfWork.Students.GetStudentByEmail(email);
        }

        public async Task<ServiceResponse<Student>> RegisterStudent(StudentRegistration student)
        {
            var serviceResponse = new ServiceResponse<Student>();

            serviceResponse = await GetStudentByEmail(student.Email);
            if(serviceResponse.Data != null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Duplicate email cannot be processed";
                return serviceResponse;
            }

            // Creating Registration number

            string reg = "";
            
            var departmentResponse = await _unitOfWork.Departments.GetById(student.DepartmentId);
            if (!departmentResponse.Success)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Department not found.";
                return serviceResponse;
            }

            reg += departmentResponse.Data.Code + '-';
            reg += student.Date.Year.ToString() + '-';
            long countOfStudents = await _unitOfWork.Students.CountStudentsInDepartment(student.DepartmentId);
            long id = countOfStudents + 1;
            if (id / 10 == 0) reg += "00";
            else if (id / 10 < 10) reg += "0";
            reg += id.ToString();

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

            try
            {
                serviceResponse.Data = newStudent;
                await _unitOfWork.Students.Add(newStudent);
                await _unitOfWork.CompleteAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Message = "Student Registration failed";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<StudentCourse>> EnrollStudentInCourse(StudentCourse studentCourse)
        {
            var serviceResponse = new ServiceResponse<StudentCourse>();

            try
            {
                serviceResponse = await _unitOfWork.Students.EnrollStudentInCourse(studentCourse);
                await _unitOfWork.CompleteAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Message = "Enrolling student failed";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
