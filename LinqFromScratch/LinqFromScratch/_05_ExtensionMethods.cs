using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// The namespaces start getting tweaked from here on in, so that I can have extension methods
// specific to each demo.
namespace LinqFromScratch.V5
{
    [TestClass]
    public class _05_ExtensionMethods
    {
        [TestMethod]
        public void _1_SingleCallWithFluentSyntax()
        {
            // Firstly, some simple fluent calls
            var filteredEmployees = SampleData.EmployeeList.Filter(x => x.DepartmentId == 1);
            Display.List(filteredEmployees, "Department==1");

            // We can still call the method in the traditional manner as before too, but who
            // wants to do that?
            var meh = Extensions.Filter(SampleData.EmployeeList, x => true);
        }

        [TestMethod]
        public void _2_ChainingCallsWithFluentSyntax()
        {
            // Obviously just chaining filters like this would be better served with both tests
            // in a single criteria, but it does the job for now, and saves having to think
            // about other types of operation to code up, yet...
            var filteredEmployees =
                SampleData.EmployeeList
                .Filter(x => x.DepartmentId != 1)
                .Filter(x => !x.Name.Contains("lex"));
            Display.List(filteredEmployees, "Department!=1, then Name doesn't contain lex");
        }
    }

    // Extension methods need to sit in static classes
    public static class Extensions
    {
        // They are static methods, and have that odd looking "this" keyword.
        // Other than that, it's identical to our last function

        // We know that statics are evil when it comes to testing, but there are levels of evil
        // at work. This method only looks at the parameters that it's given, and only affects
        // variables that it creates and returns, so wider system state neither affects or is
        // effected by it. Essentially calling this method is an implementation detail of other
        // code. In Functional programming terms it is "pure" and we like that sorta stuff in F#
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> originalList,
            Predicate<T> check)
        {
            var filteredList = new List<T>();
            foreach (var item in originalList)
            {
                if (check(item))
                {
                    filteredList.Add(item);
                }
            }
            return filteredList;
        }
    }
}