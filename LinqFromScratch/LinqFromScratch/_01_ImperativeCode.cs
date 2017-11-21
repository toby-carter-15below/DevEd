using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqFromScratch
{
    [TestClass]
    public class _01_ImperativeCode
    {
        // Here we have some very simple examples of creating a filtered collection
        // based on some criteria.

        [TestMethod]
        public void ImperativeSearchForEmployeeByDeparmentId()
        {
            var filteredList = new ArrayList();
            foreach (var employee in SampleData.EmployeeList)
            {
                if (employee.DepartmentId == 1)
                {
                    filteredList.Add(employee);
                }
            }
            Display.ArrayList(filteredList, "DepartmentId==1");

            // And to prod around at the history of .net even more...
            filteredList.Add("Note that this ArrayList content is not even remotely typesafe");
            filteredList.Add(null);
            filteredList.Add("It was all we had prior to generics if we wanted easy adding");
            filteredList.Add(23);
            filteredList.Add("There were plain old arrays, which are safe, but don't resize");
            filteredList.Add(DateTime.Now);
            filteredList.Add("Or you could roll your own typed collection classes");
            filteredList.Add("for every single entity class that you cared about");
            filteredList.Add(new object());
            // arrarys are great though!
            Display.ArrayList(filteredList, "And now with all sorts of extra gubbins.");
        }

        [TestMethod]
        public void ImperativeSearchForEmployeeByName()
        {
            var filteredList = new ArrayList();
            foreach (var employee in SampleData.EmployeeList)
            {
                if (employee.Name.Contains("Van Halen"))
                {
                    filteredList.Add(employee);
                }
            }
            Display.ArrayList(filteredList, "Name contains Van Halen");
        }

        // At this point it should be very obvious that we're duplicating most of the code to search
        // and just changing the condition. Wouldn't it be nice if we could reuse the flow and just
        // provide the condition.

        // Note, you can imagine how a lot of the same searches would be happening all over a codebase.
        // We could have a bunch of similar cut and paste methods with different criteria as another
        // interim step, but there's no point to me coding that up to show. Just think of our data layers.

        // All problems in computer science can be solved with an additional layer of indirection
        // (except too many layers of indirection). If we want to inject little bits of behaviour that
        // we can easily modify in an OO language we can use classes, interfaces, inheritance, and
        // all that good polymorphism stuff.

    }
}
