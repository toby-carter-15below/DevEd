using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LYNC
{
    [TestClass]
    public class _03_UsingDelegatesForCriteria
    {
        [TestMethod]
        public void SearchForPropertyUsingCommonCodeAndDelegatesToCompare()
        {
            // Ooh, we've skipped a few years of C#'s evolution here and gone straight to
            // using a lambda to populate our delegate. Sweet.
            // This is starting to look like the linq we know and love, but...
            var filteredList = FilterProperties(SampleData.PropertyList, x => x.AgentId == 1);
            Display.List(filteredList, "AgentId==1");

            filteredList = FilterProperties(SampleData.PropertyList, x => x.Title.Contains("third"));
            Display.List(filteredList, "Title contains third");

            filteredList = FilterProperties(SampleData.PropertyList, x => x.PropertyId == 2);
            Display.List(filteredList, "PropertyId==2");
        }

        public delegate bool PropertyConditionChecker(Property property);

        // this hardly changes, the type in the method arguments changes from an interface to a delegate...
        public Property[] FilterProperties(IEnumerable<Property> originalList, PropertyConditionChecker check)
        {
            var filteredList = new ArrayList();
            foreach (var property in originalList)
            {
                // ...and executing our check is a little shorter as we're not selecting a method on an object,
                // just running the method we're given
                if (check(property))
                {
                    filteredList.Add(property);
                }
            }
            return (Property[])filteredList.ToArray(typeof(Property));
        }

        // Our core searching code is still completely bound to collections of Property.
        // We need to try and make things a bit more generic, otherwise we'll have to write the same code for
        // every different entity type we might want to use, such as the Agent that is set up and waiting.

    }
}
