﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class StudentCourseHistory
    {
        public long Id { get; set; }
        public int DepartmentId { get; set; }
        public string CourseCode { get; set; }
        public long StudentId { get; set; }
        public DateTime Date { get; set; }
        public string? Grade { get; set; }
        public GradeLetter? GradeLetter { get; set; }
        public Student? Student { get; set; }
        public Course? Course { get; set; }
        public int NthHistory {  get; set; }
    }
}
