using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LYNC
{
    [TestClass]
    public class _04_EnterGenerics
    {
        [TestMethod]
        public void GenericSearching()
        {
            // FilterProperties just becomes Filter
            var filteredProperties = Filter(SampleData.PropertyList, x => x.AgentId == 1);
            Display.List(filteredProperties, "AgentId==1");

            // We can provide the generic type to the method, but we don't need to
            filteredProperties = Filter<Property>(SampleData.PropertyList, x => x.Title.Contains("third"));
            Display.List(filteredProperties, "Title contains third");

            // And now we can use it on different types. Hurrah.
            var filteredAgents = Filter(SampleData.AgentList, x => !x.Name.Contains("alone"));
            Display.List(filteredAgents, "Name doesn't contain alone");

            filteredAgents = Filter(SampleData.AgentList, x => x.HeadOfficeId == null);
            Display.List(filteredAgents, "HeadOfficeId is null");
        }

        // The code changes basically involve swapping out mentions of Property for the generic T
        // and making the delegate and method themselves generics
        public delegate bool ConditionChecker<T>(T item);

        public IEnumerable<T> Filter<T>(IEnumerable<T> originalList, ConditionChecker<T> check)
        //public IEnumerable<T> Filter<T>(IEnumerable<T> originalList, Func<T,bool> check)
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


        // So now we can call Filter(SomeCollection, BySomeCriteria), which is ok for a single action
        // but if you want to string things together the outside in approach looks nasty. Imagine we add
        // ordering in a similar manner, we'd currently have to do something like:

        // var bob = Order(Filter(collection, filterCriteria), orderCriteria);

        // Yuck. Linq gives us the nice fluent syntax that allows us to chain things together nicely:

        // var bob = collection.Filter(criteria).OrderBy(differentCriteria);
    }
}
