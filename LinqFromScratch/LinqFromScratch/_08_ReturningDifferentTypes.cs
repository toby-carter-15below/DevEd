﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqFromScratch.V8
{
    [TestClass]
    public class _08_ReturningDifferentTypes
    {
        [TestMethod]
        public void AlteredStructure()
        {
            // Here we're using Anonymous Types to create a structure that we've not explicitly
            // defined elsewhere in the code, with a combination of a straight mapping of one
            // property & some manipulation of another.
            var projectedType = SampleData.EmployeeList.Project(x => new
            {
                NewId = x.EmployeeId,
                FirstName = x.Name.Substring(0, x.Name.IndexOf(' ')),
                LastName = x.Name.Substring(x.Name.IndexOf(' '))
            });

            // As mentioned in the session, this is statically typed, not dynamic, the output
            // will show the anonymous type being used, and hacking around this foreach loop
            // will let you see the intellisense for the returned data.
            foreach (var funkyNewType in projectedType)
            {
                var intellisenseMeddling = funkyNewType.FirstName;
            }

            Display.List(projectedType, "NewId, FirstName & LastName");
        }
    }

    public static class Extensions
    {
        // Now we're going for Linq's Select method which performs an operation known as
        // projection in the database world - limiting the returned data to the fields we want.
        public static IEnumerable<TReturn> Project<T, TReturn>(this IEnumerable<T> originalList,
            Func<T, TReturn> conversion)
        {
            foreach (var item in originalList)
            {
                yield return conversion(item);
            }
        }
        // Damn, that was easy.

        // Time to change gear then. We're used to thinking of using Linq to objects over
        // IEnumerables but we also have other places we can use the syntax, Linq to Xml,
        // Linq to Sql/Entities etc. which all work on IQueryable instead. Here we're not
        // just iterating over a loop, and we want to be a bit more clever with the way we
        // build and execute queries when hitting DBs etc. Sadly, this is bloody hard, and
        // basically means writing a compiler, so that's a good place to stop :)
    }
}
