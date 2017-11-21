using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqFromScratch
{
    [TestClass]
    public class _02_SearchWithCommonCode
    {
        // This is an implementation of the Strategy design pattern...

        [TestMethod]
        public void SearchForEmployeeUsingCommonCodeAndStrategyPatternToCompare()
        {
            // We create an object instance that knows how to look for a condition that fits a simple interface definition
            IEmployeeConditionChecker departmentIdEmployeeChecker = new DepartmentIdEmployeeChecker(3);
            // And use that in a call to a more generic (not in a generics sense) method
            var filteredList = FilterEmployees(SampleData.EmployeeList, departmentIdEmployeeChecker);

            Display.List(filteredList, "DepartmentId==3");

            // Then use a different checker for a different search
            IEmployeeConditionChecker nameEmployeeChecker = new NameEmployeeChecker("Ed");
            filteredList = FilterEmployees(SampleData.EmployeeList, nameEmployeeChecker);

            Display.List(filteredList, "Name contains Ed");
        }

        public Employee[] FilterEmployees(IEnumerable<Employee> originalList, IEmployeeConditionChecker check)
        {
            var filteredList = new ArrayList();
            foreach (var employee in originalList)
            {
                if (check.IsMatch(employee))
                {
                    filteredList.Add(employee);
                }
            }
            return (Employee[]) filteredList.ToArray(typeof(Employee));
        }


        public interface IEmployeeConditionChecker
        {
            bool IsMatch(Employee employee);
        }

        public class DepartmentIdEmployeeChecker : IEmployeeConditionChecker
        {
            private readonly int departmentId;
            public DepartmentIdEmployeeChecker(int departmentId)
            {
                this.departmentId = departmentId;
            }
            public bool IsMatch(Employee employee)
            {
                return employee.DepartmentId == departmentId;
            }
        }

        public class NameEmployeeChecker : IEmployeeConditionChecker
        {
            private readonly string nameSubSection;
            public NameEmployeeChecker(string nameSubSection)
            {
                this.nameSubSection = nameSubSection;
            }
            public bool IsMatch(Employee employee)
            {
                return employee.Name.Contains(nameSubSection);
            }
        }

        // Well, we now have a shiny new reusable method to inspect our collection,
        // but fuck me if's not labourious to tweak search parameters.

        // This is an implementation of the Strategy design pattern... but it's a
        // bit too big and bulky for our little requirement here.

        // Creating an interface and then multiple implementations for such a trivial
        // task is going to polute our namespaces with a lot of crap, and in the case
        // of the title checker we may have all sorts of subtle variations on how we
        // want to look for a condition: equals, contains, starts with, ends with...

        // Each different search criteria takes a lot of constructing too,
        // maybe more than doing it all by hand. We need to be able to send our
        // criteria in a more lightweight manner.
    }
}
