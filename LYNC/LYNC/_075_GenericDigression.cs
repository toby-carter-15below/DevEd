using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LYNC.V75
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
            var objects = SampleData.AgentList;
            var numbers = SampleData.IntList;
            // IEnumerable<object> is fine here
            Display.List(objects, "Objects");
            // But doesn't work here even though everything in .Net should inherit from object
            Display.List(numbers, "Numbers");

            // Swapping our display method to being a generic method itself allows it to work for both
            Display.AnyList(objects, "Objects");
            Display.AnyList(numbers, "Numbers");
        }
    }
}
