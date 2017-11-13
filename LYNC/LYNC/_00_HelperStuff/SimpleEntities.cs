using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LYNC
{
    // A few simple data entity types

    // Good OO would mean we should have references to objects rather than Ids for relationships,
    // but this sort of structure tends to be familiar.

    public class Employee
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return "EmployeeId:" + EmployeeId + ", DeptId:" + DepartmentId + ", Name:" + Name;
        }
    }

    public class Department
    {
        public int DepartmentId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            //return "DepartmentId:" + DepartmentId + ", Name:" + Name;
            return "DepartmentId:" + DepartmentId + ", ParentId:" + (ParentId.HasValue ? ParentId.Value.ToString() : "<null>") + ", Name:" + Name;
        }
    }
}