using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqFromScratch
{
    [TestClass]
    public class _03_UsingDelegatesForCriteria
    {
        [TestMethod]
        public void SearchForEmployeeUsingCommonCodeAndDelegatesToCompare()
        {
            // Here we define the criteria in-line with the call as a delegate instance
            // This is _starting_ to look a bit more like LINQ
            var filteredList = FilterEmployees(SampleData.EmployeeList, delegate(Employee emp)
                { return emp.DepartmentId == 1; });
            Display.List(filteredList, "DepartmentId==1");

            filteredList = FilterEmployees(SampleData.EmployeeList, delegate(Employee emp)
                { return emp.Name.Contains("Sam"); });
            Display.List(filteredList, "Name contains Sam");

            // So now it's much less effort to add in variations on searches
            filteredList = FilterEmployees(SampleData.EmployeeList, delegate(Employee emp)
                { return emp.DepartmentId != 1; });
            Display.List(filteredList, "DepartmentId *NOT EQUAL TO* 1");

            // Or on new fields
            filteredList = FilterEmployees(SampleData.EmployeeList, delegate(Employee emp)
                { return emp.EmployeeId == 3; });
            Display.List(filteredList, "EmployeeId==3");
        }

        public delegate bool EmployeeConditionChecker(Employee employee);

        // this hardly changes, the type in the method arguments changes from an interface to
        // a delegate...
        public Employee[] FilterEmployees(IEnumerable<Employee> originalList,
            EmployeeConditionChecker check)
        {
            var filteredList = new ArrayList();
            foreach (var employee in originalList)
            {
                // ...and executing our check is a little shorter as we're not selecting a
                // method on an object, just running the method we're given
                if (check(employee))
                {
                    filteredList.Add(employee);
                }
            }
            return (Employee[])filteredList.ToArray(typeof(Employee));
        }

        // Our core searching code is still completely bound to collections of Employee.
        // We need to try and make things a bit more generic, otherwise we'll have to write
        // the same code for every different entity type we might want to use, such as the
        // Department that is set up and waiting.
    }
}
