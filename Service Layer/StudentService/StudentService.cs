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

        public async Task<ServiceResponse<IEnumerable<Student>>> GetAllIncludingAll(string regNum = "")
        {
            return await _unitOfWork.StudentRepository.GetAllIncludingAll(regNum);
        }

        public async Task<ServiceResponse<Student>> GetStudentByRegNum(string regNum)
        {
            var response = new ServiceResponse<Student>();
            try
            {
                response.Data = await _unitOfWork.StudentRepository.SingleOrDefaultAsync(x => x.RegistrationNumber == regNum);
                if (response.Data == null) throw new Exception("No student found with the registration number");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<Student>> GetStudentByEmail(string email)
        {
            var response = new ServiceResponse<Student>();
            try
            {
                response.Data = await _unitOfWork.StudentRepository.SingleOrDefaultAsync(x => x.Email == email);
                if (response.Data == null) throw new Exception("No student found with the email");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<Student>> RegisterStudent(StudentRegistration student)
        {
            var serviceResponse = new ServiceResponse<Student>();

            var temp = await GetStudentByEmail(student.Email);
            if(temp.Data != null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Duplicate email cannot be processed";
                return serviceResponse;
            }

            // Creating Registration number

            string reg = "";
            
            Department dept = await _unitOfWork.DepartmentRepository.FindAsync(student.DepartmentId);
            if (dept == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Department not found.";
                return serviceResponse;
            }

            reg += dept.Code + '-';
            reg += student.Date.Year.ToString() + '-';
            long countOfStudents = await _unitOfWork.StudentRepository.CountAsync(x => x.DepartmentId == student.DepartmentId);
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
                await _unitOfWork.StudentRepository.AddAsync(newStudent);
                await _unitOfWork.CompleteAsync();
                serviceResponse.Message = "Student registration successful.";
            }
            catch(Exception ex)
            {
                serviceResponse.Message = "Student Registration failed.";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<StudentCourse>> EnrollStudentInCourse(StudentCourse studentCourse)
        {
            var serviceResponse = new ServiceResponse<StudentCourse>();

            bool isCourseInDepartment = await _unitOfWork.CourseRepository.ContainsAsync(x => x.Id == studentCourse.CourseId && x.DepartmentId == studentCourse.DepartmentId);
            if (!isCourseInDepartment)
            {
                serviceResponse.Message = "Course is not in the department";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            bool isStudentEnrolledInCourse = await _unitOfWork.StudentCourseRepository.ContainsAsync(x => x.StudentId == studentCourse.StudentId && x.CourseId == studentCourse.CourseId);
            if (isStudentEnrolledInCourse)
            {
                serviceResponse.Message = "Student is already enrolled in the course";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            try
            {
                await _unitOfWork.StudentCourseRepository.AddAsync(studentCourse);
                await _unitOfWork.CompleteAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Message = "Enrolling student failed";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<StudentCourse>> SaveResult(StudentCourse data)
        {
            var serviceResponse = new ServiceResponse<StudentCourse>();
            try
            {
                StudentCourse? studentCourse = await _unitOfWork.StudentCourseRepository.FirstOrDefaultAsync(x => x.StudentId == data.StudentId && x.CourseId == data.CourseId);
                if (studentCourse == null) throw new Exception("Student is not enrolled in the course. Try enrolling first.");

                studentCourse.GradeId = data.GradeId;
                _unitOfWork.StudentCourseRepository.Update(studentCourse);
                serviceResponse.Data = studentCourse;

                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Message = "Student result saving failed";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Student>> GetStudentResultById(long id)
        {
            return await _unitOfWork.StudentRepository.GetStudentResultById(id);
        }
    }
}
