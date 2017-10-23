using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LYNC
{
    [TestClass]
    public class _02_SearchWithCommonCode
    {
        // This is an implementation of the Strategy design pattern...

        [TestMethod]
        public void SearchForPropertyUsingCommonCodeAndStrategyPatternToCompare()
        {
            // We create an object instance that knows how to look for a condition that fits a simple interface definition
            IPropertyConditionChecker agentIdPropertyChecker = new AgentIdPropertyChecker(1);
            // And use that in a call to a more generic (not in a generics sense) method
            var filteredList = FilterProperties(SampleData.PropertyList, agentIdPropertyChecker);

            Display.List(filteredList, "AgentId==1");

            // Then use a different checker for a different search
            IPropertyConditionChecker titlePropertyChecker = new TitlePropertyChecker("third");
            filteredList = FilterProperties(SampleData.PropertyList, titlePropertyChecker);

            Display.List(filteredList, "Title contains third");
        }

        public Property[] FilterProperties(IEnumerable<Property> originalList, IPropertyConditionChecker check)
        {
            var filteredList = new ArrayList();
            foreach (var property in originalList)
            {
                if (check.IsMatch(property))
                {
                    filteredList.Add(property);
                }
            }
            return (Property[]) filteredList.ToArray(typeof(Property));
        }


        public interface IPropertyConditionChecker
        {
            bool IsMatch(Property property);
        }

        public class AgentIdPropertyChecker : IPropertyConditionChecker
        {
            private readonly int _agentId;
            public AgentIdPropertyChecker(int agentId)
            {
                _agentId = agentId;
            }
            public bool IsMatch(Property property)
            {
                return property.AgentId == _agentId;
            }
        }

        public class TitlePropertyChecker : IPropertyConditionChecker
        {
            private readonly string _titleSection;
            public TitlePropertyChecker(string titleSection)
            {
                _titleSection = titleSection;
            }
            public bool IsMatch(Property property)
            {
                return property.Title.Contains(_titleSection);
            }
        }

        // Well, we now have a shiny new reusable method to inspect our collection,
        // but fuck me if's not labourious to tweak search parameters.

        // This is an implementation of the Strategy design pattern... but it's a
        // bit too big and bulky for our little requirement here.

        // Creating an interface and then multiple implementations for such a trivial
        // task is going to polute our namespaces with a lot of crap, and in the case
        // of the title checker we may have all sorts of subtle variations on how we
        // want to look for a condition: equals, contains, starts with, ends with...

        // Each different search criteria takes a lot of constructing too,
        // maybe more than doing it all by hand. We need to be able to send our
        // criteria in a more lightweight manner.
    }
}
