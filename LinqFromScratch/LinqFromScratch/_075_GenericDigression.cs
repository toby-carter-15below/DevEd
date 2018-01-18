using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqFromScratch.V75
{
    [TestClass]
    public class _075_GenericDigression
    {
        // For the previous page, I wanted to output collections of ints in the same way
        // that I'd been displaying the more complex businessy types of object. I ran
        // into an issue with passing ints into IEnumerable<object>

        private readonly IEnumerable<object> objects = SampleData.DepartmentList;
        private readonly IEnumerable<int> numbers = SampleData.IntList;
        private readonly IEnumerable<DateTime> stuff = new[]
        {
            DateTime.MinValue, DateTime.MaxValue,
        };

        [TestMethod]
        public void _1_CollectionsOfObjectArentCollectionsOfValueTypes()
        {
            // IEnumerable<object> is fine here
            Display.List(objects, "Objects");

            // Display.List(numbers, "Numbers");
            // But IEnumerable<int> doesn't work here even though everything in .Net inherits
            // from object. Value types and reference types are handled differently, so whilst
            // you can cast int to object individually, you can't go straight from
            // IEnumerable<int> to IEnumerable<object>
            Display.ListOfInts(numbers, "Numbers");
            // Or any other value types
            // Display.List(stuff, "some stuff...");
        }

        [TestMethod]
        public void _2_ValueCollectionsCanBeExplicitlyConvertedToObjectCollections()
        {
            // You can just cast each item in turn and add them to a collection of objects
            IEnumerable<object> num2 = numbers.Select(n => n as object);
            Display.List(num2, "Numbers as objects");

            IEnumerable<object> num3 = numbers.Cast<object>();
            Display.List(num3, "Numbers as objects, in different ways");
        }

        [TestMethod]
        public void _3_GenericCollectionWin()
        {
            // Swapping our display method to being a generic method itself allows it to work
            // for both. The JIT compiler will create concrete versions of generic methods as it
            // needs them.
            Display.AnyList(objects, "Objects");
            Display.AnyList(numbers, "Numbers");
            Display.AnyList(stuff, "Stuff");
        }
    }
}
