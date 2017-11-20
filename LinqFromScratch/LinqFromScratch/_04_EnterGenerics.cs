using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqFromScratch
{
    [TestClass]
    public class _04_EnterGenerics
    {
        [TestMethod]
        public void GenericSearching()
        {
            // FilterEmployees just becomes Filter
            var filteredEmployees = Filter(SampleData.EmployeeList, x => x.DepartmentId == 1);
            Display.List(filteredEmployees, "DepartmentId==1");

            // We can provide the generic type to the method, but we don't need to
            filteredEmployees = Filter<Employee>(SampleData.EmployeeList, x => x.Name.Contains("David"));
            Display.List(filteredEmployees, "Name contains David");

            // And now we can use it on different types. Hurrah.
            var filteredDepartments = Filter(SampleData.DepartmentList, x => !x.Name.Contains("front"));
            Display.List(filteredDepartments, "Name doesn't contain front");

            filteredDepartments = Filter(SampleData.DepartmentList, x => x.ParentId == null);
            Display.List(filteredDepartments, "ParentId is null");
        }

        // The code changes basically involve swapping out mentions of Employee for the generic T
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
