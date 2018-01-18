using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqFromScratch.V6
{

    [TestClass]
    public class _06_LazyEvaluation
    {
        [TestMethod]
        public void _1_EagerEvaluation()
        {
            // This loops through everything, that's fine and what we'd expect.
            var filtered = SampleData.EmployeeList.Filter(x => x.DepartmentId == 3);
            Console.WriteLine("NOTE: The filtering is already done.");
            Display.List(filtered, "DepartmentId==3");

            // But what if we just want to get the first item?
            var firstMatch =
                SampleData.EmployeeList
                .Filter(x => x.DepartmentId == 3)
                .JustTheFirst();
            Display.Item(firstMatch, "First item where DepartmentId==3");
        }

        [TestMethod]
        public void _2_LazyEvaluation()
        {
            // This still loops through everything, that's still fine and what we'd expect.
            var filtered = SampleData.EmployeeList.LazyFilter(x => x.DepartmentId == 3);
            Console.WriteLine("NOTE: But this time it has not happened yet.");
            Display.List(filtered, "DepartmentId==3 (Lazy)");

            // But now, magic happens
            var firstMatch =
                SampleData.EmployeeList
                .LazyFilter(x => x.DepartmentId == 3)
                .JustTheFirst();
            Display.Item(firstMatch, "First item where DepartmentId==3 (Lazy)");


            firstMatch =
                SampleData.EmployeeList
                .LazyFilterTidied(x => x.DepartmentId == 3)
                .JustTheFirst();
            Display.Item(firstMatch, "First item where DepartmentId==3 (LazyTidied)");
        }
    }

    public static class Extensions
    {
        // this is identical to before except for some tracing code to show how much it's used
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> originalList,
            Predicate<T> check)
        {
            var filteredList = new List<T>();
            var counter = 0;
            Console.WriteLine("Running filter");
            foreach (var item in originalList)
            {
                counter++;
                if (check(item))
                {
                    Console.WriteLine("filtered item " + counter + " and check was true");
                    filteredList.Add(item);
                }
                else
                    Console.WriteLine("filtered item " + counter + " and check was false");
            }
            Console.WriteLine("Finished filter");
            return filteredList;
        }

        public static T JustTheFirst<T>(this IEnumerable<T> originalList)
        {
            IEnumerator<T> enumerator = originalList.GetEnumerator();
            enumerator.MoveNext(); // we're not being careful about null values here, nevermind.
            return enumerator.Current;
            // oops, I forgot to dispose of enumerator, ho hum.
        }

        public static IEnumerable<T> LazyFilter<T>(this IEnumerable<T> originalList,
            Predicate<T> check)
        {
            // lets ditch this line
            // var filteredList = new List<T>();
            var counter = 0;
            Console.WriteLine("Running filter");
            foreach (var item in originalList)
            {
                counter++;
                if (check(item))
                {
                    // and this
                    //filteredList.Add(item);
                    Console.WriteLine("filtered item " + counter + " and check was true");
                    // and this breaks us out of the loop early,
                    // but lets us get back in at the same point later on
                    yield return item;
                }
                else
                    Console.WriteLine("filtered item " + counter + " and check was false");
            }
            Console.WriteLine("Finished filter");
        }

        // without all the tracing shit in the way, this is now very nice and neat,
        // and not at all dissimilar to Linq's Where method.
        public static IEnumerable<T> LazyFilterTidied<T>(this IEnumerable<T> originalList,
            Predicate<T> check)
        {
            foreach (var item in originalList)
            {
                if (check(item))
                {
                    yield return item;
                }
            }
        }

        // And for reference, this is the old filter method
        public static IEnumerable<T> OriginalFilterTidied<T>(this IEnumerable<T> originalList,
            Predicate<T> check)
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

        // This sort of lazyness could potentially save us quite a bit of memory if working
        // on large collections, as we don't have to create new intermediate collections,
        // especially if chaining multiple steps together. And as we've seen with our
        // JustTheFirst method, it means that we can skip a lot of unneeded work in some cases.

        // The flip side to that is that there's a bunch of work to do each time we iterate
        // over our IEnumerable, hence the resharper warnings.
    }
}