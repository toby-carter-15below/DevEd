using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqFromScratch
{
    public static class SampleData
    {
        public static readonly Employee[] EmployeeList = new [] {
                new Employee(){EmployeeId = 1, DepartmentId = 1, Name = "Eddie Van Halen"},
                new Employee(){EmployeeId = 2, DepartmentId = 2, Name = "Alex Van Halen"},
                new Employee(){EmployeeId = 3, DepartmentId = 3, Name = "David Lee Roth"},
                new Employee(){EmployeeId = 4, DepartmentId = 3, Name = "Sammy Hagar"}};

        public static readonly Department[] DepartmentList = new [] {
                new Department(){DepartmentId = 1, ParentId = null, Name = "Dept. of redefining the very meaning of rock guitar"},
                new Department(){DepartmentId = 2, ParentId = null, Name = "Dept. of mind blowing percussion"},
                new Department(){DepartmentId = 3, ParentId = 1, Name = "Dept. of frontmanology"}};

        public static readonly IEnumerable<int> IntList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    }
}
