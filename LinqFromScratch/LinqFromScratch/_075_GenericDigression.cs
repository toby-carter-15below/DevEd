using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqFromScratch.V75
{
    [TestClass]
    public class _075_GenericDigression
    {
        // For the previous page, I wanted to output collections of ints in the same way
        // that I'd been displaying the more complex businessy types of object. I ran
        // into an issue with passing ints into IEnumerable<object>

        [TestMethod]
        public void _075_GenericCollectionWin()
        {
            var objects = SampleData.DepartmentList;
            var numbers = SampleData.IntList;
            // IEnumerable<object> is fine here
            Display.List(objects, "Objects");
            // But doesn't work here even though everything in .Net inherits from object
            // This is co/contra variance on collections in action. You can cast int to
            // object, but not IE<int> to IE<object> due to int being a value type rather
            // than a reference type...
            Console.WriteLine("TOBZHAXX - prolly spent a bit more time here...");
            Display.List(numbers, "Numbers");



            IEnumerable<object> num2 = System.Linq.Enumerable.Select(numbers, n => n as object);
            Display.List(num2, "Numbers as objects");

            IEnumerable<DateTime> stuff = new[] {DateTime.MinValue, DateTime.MaxValue, };
            //Display.List(stuff, "some stuff...");

            // Swapping our display method to being a generic method itself allows it to work for both
            Display.AnyList(objects, "Objects");
            Display.AnyList(numbers, "Numbers");
        }
    }
}
