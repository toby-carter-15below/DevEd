using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LYNC.V7
{

    [TestClass]
    public class _07_AddingMoreFeatures
    {
        [TestMethod]
        public void _1_Skip()
        {
            var highNumbers = SampleData.IntList.Skip(5);
            Display.List(highNumbers, "Skip 5");
        }

        [TestMethod]
        public void _2_Take()
        {
            var lowNumbers = SampleData.IntList.Take(5);
            Display.List(lowNumbers, "Take 5");
            
            lowNumbers = SampleData.IntList.Take(10);
            Display.List(lowNumbers, "Take 10");
        }

        [TestMethod]
        public void _3_Combinations()
        {
            var someNumbers = SampleData.IntList.Filter(x => x > 1 && x < 10);
            Display.List(someNumbers, "Just filtered");

            // The lazy execution means that we don't need to filter all of the numbers once we've 
            // skipped the first 2 and taken the next 3
            // This sort of stuff can be important for performance with larger data sets
            someNumbers = SampleData.IntList.Filter(x => x > 1 && x < 10).Skip(2).Take(3);
            Display.List(someNumbers, "Filtered then paged");

            // Obviously, ordering Skips and Takes is important
            someNumbers = SampleData.IntList.Filter(x => x > 1 && x < 10).Take(3).Skip(2);
            Display.List(someNumbers, "Filtered then paged");    
        }

    }

    public static class Extensions
    {
        // unchanged
        public static T First<T>(this IEnumerable<T> originalList)
        {
            IEnumerator<T> enumerator = originalList.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current;
        }
        
        // unchanged
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> originalList, Func<T, bool> check)
        {
            foreach (var item in originalList)
            {
                if (check(item))
                {
                    Console.WriteLine("Filter matched " + item.ToString());
                    yield return item;
                }
                else
                {
                    Console.WriteLine("Filter didn't match " + item.ToString());
                }
            }
            yield break;
        }

        public static IEnumerable<T> Skip<T>(this IEnumerable<T> originalList, int skipCount)
        {
            int count = 0;
            foreach (var item in originalList)
            {
                Console.WriteLine("In Skip: " + count);
                // With more intelligent use of GetEnumerator we could quickly loop through the skip count
                // then just yield all the rest without the comparison and increment
                if (count++ >= skipCount)
                {
                    yield return item;
                }
            }
            Console.WriteLine("Finished Skip");
            yield break;
        }

        public static IEnumerable<T> Take<T>(this IEnumerable<T> originalList, int takeCount)
        {
            int count = 0;
            foreach (var item in originalList)
            {
                Console.WriteLine("In Take: " + count);
                if (count++ < takeCount)
                {
                    yield return item;
                }
                else
                {
                    Console.WriteLine("Quick exit from Take");
                    yield break;
                }
            }
            Console.WriteLine("Finished Take");
            yield break;
        }
    }
}
