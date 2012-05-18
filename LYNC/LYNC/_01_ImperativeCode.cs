using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LYNC
{
    [TestClass]
    public class _01_ImperativeCode
    {
        // Here we have some very simple examples of creating a filtered collection
        // based on some criteria.

        [TestMethod]
        public void ImperativeSearchForPropertyId()
        {
            var filteredList = new List<Property>();
            foreach (var property in SampleData.PropertyList)
            {
                if (property.AgentId == 1)
                {
                    filteredList.Add(property);
                }
            }
            Display.List(filteredList, "AgentId==1");
        }

        [TestMethod]
        public void ImperativeSearchForPropertyTitles()
        {
            var filteredList = new List<Property>();
            foreach (var property in SampleData.PropertyList)
            {
                if (property.Title.Contains("third"))
                {
                    filteredList.Add(property);
                }
            }
            Display.List(filteredList, "Title contains third");
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
