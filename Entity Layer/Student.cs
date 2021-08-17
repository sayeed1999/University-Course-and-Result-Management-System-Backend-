﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Layer
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact {  get; set; }
        public DateTime Date {  get; set; }
        public string Address { get; set; }
        public Department? Department{ get; set; }
        public int DepartmentId { get; set; }
        public string RegistrationNumber { get; init; }
    }
}