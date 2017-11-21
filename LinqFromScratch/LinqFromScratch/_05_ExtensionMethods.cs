using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// The namespaces start getting tweaked from here on in, so that I can have extension methods specific to each demo.
namespace LinqFromScratch.V5
{
    [TestClass]
    public class _05_ExtensionMethods
    {
        [TestMethod]
        public void FluentSyntax()
        {
            var filteredEmployees = SampleData.EmployeeList.Filter(x => x.DepartmentId == 1);
            Display.List(filteredEmployees, "Department==1");

            filteredEmployees = SampleData.EmployeeList.Filter(x => !x.Name.Contains("Halen"));
            Display.List(filteredEmployees, "Name doesn't contain Halen");

            // Obviously just chaining filters like this would be better served with both tests in a single criteria,
            // but it does the job for now, and saves having to think about other types of operation to code up, yet...
            filteredEmployees = SampleData.EmployeeList.Filter(x => x.DepartmentId != 1).Filter(x => !x.Name.Contains("lex"));
            Display.List(filteredEmployees, "Department!=1, then Name doesn't contain lex");

            // We can still call the method in the traditional manner as before too, but who wants to do that?
        }
    }

    // Extension methods need to sit in static classes
    public static class Extensions
    {
        // They are static methods, and have that odd looking "this" keyword.
        // Other than that, it's identical to our last function

        // We know that statics are evil when it comes to testing, but there are levels of evil at work.
        // This method only looks at the parameters that it's given, and only affects variables that it
        // creates and returns, so wider system state neither affects or is effected by it. Essentially
        // calling this method is an implementation detail of other code.
        // In Functional programming terms it is "pure" and we like that sorta stuff in F#
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> originalList, Func<T, bool> check)
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

        // This process of writing methods that return the same (or a similar) type so that other
        // methods can be chained on tends to be refered to as a fluent interface
    }
}