using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqFromScratch
{
    [TestClass]
    public class _03_UsingDelegatesForCriteria
    {
        [TestMethod]
        public void SearchForEmployeeUsingCommonCodeAndDelegatesToCompare()
        {
            // Ooh, we've skipped a few years of C#'s evolution here and gone straight to
            // using a lambda to populate our delegate. Sweet.
            // This is starting to look like the linq we know and love
            var filteredList = FilterEmployees(SampleData.EmployeeList, x => x.DepartmentId == 1);
            Display.List(filteredList, "DepartmentId==1");

            filteredList = FilterEmployees(SampleData.EmployeeList, x => x.Name.Contains("Sam"));
            Display.List(filteredList, "Name contains Sam");

            // So now it's much less effort to add in variations on searches
            filteredList = FilterEmployees(SampleData.EmployeeList, x => x.DepartmentId != 1);
            Display.List(filteredList, "DepartmentId *NOT EQUAL TO* 1");
            // Or on new fields
            filteredList = FilterEmployees(SampleData.EmployeeList, x => x.EmployeeId == 2);
            Display.List(filteredList, "EmployeeId==2");
        }

        public delegate bool EmployeeConditionChecker(Employee employee);

        // this hardly changes, the type in the method arguments changes from an interface to a delegate...
        public Employee[] FilterEmployees(IEnumerable<Employee> originalList, EmployeeConditionChecker check)
        {
            var filteredList = new ArrayList();
            foreach (var employee in originalList)
            {
                // ...and executing our check is a little shorter as we're not selecting a method on an object,
                // just running the method we're given
                if (check(employee))
                {
                    filteredList.Add(employee);
                }
            }
            return (Employee[])filteredList.ToArray(typeof(Employee));
        }

        // Our core searching code is still completely bound to collections of Employee.
        // We need to try and make things a bit more generic, otherwise we'll have to write the same code for
        // every different entity type we might want to use, such as the Department that is set up and waiting.

    }
}
