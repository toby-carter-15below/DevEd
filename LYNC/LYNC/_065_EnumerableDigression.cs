using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LYNC.V65
{
    [TestClass]
    public class _065_EnumerableDigression
    {
        [TestMethod]
        public void _1_WhatIsForEach()
        {
            Console.WriteLine("Using foreach:");
            foreach (Agent agent in SampleData.AgentList)
            {
                Console.WriteLine(agent.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("What happens behind the scenes:");
            // The compiler's foreach structure is syntactic sugar around this construct:
            using (var enumerator = SampleData.AgentList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Agent agent = enumerator.Current;
                    // then we have our code from the loop body
                    Console.WriteLine(agent.ToString());
                }
            }
        }

        [TestMethod]
        public void _2_AndWhatDoesThatEnumeratorDo()
        {
            EnumerableThingy intCollection = new EnumerableThingy();

            Console.WriteLine("First run");
            foreach (int value in intCollection)
            {
                Console.WriteLine(value.ToString());
            }

            Console.WriteLine("Second run, note the different contents");
            foreach (int value in intCollection)
            {
                Console.WriteLine(value.ToString());
            }
        }

        public class EnumerableThingy : IEnumerable<int>
        {
            // We're just using a nice simple array as the backing data structure here
            // but it could be any sort of structure, both 1 dimensional things like
            // lists, queues, stacks etc. or 2 dimensional like trees as long as we
            // have a sensible concept of walking through the structure one item at a
            // time.
            private int[] _internalDataStructure = new int[] { 1, 2, 3, 4, 5 };

            public IEnumerator<int> GetEnumerator()
            {
                EnumeratorThingy enumerator = new EnumeratorThingy(_internalDataStructure);
                // copying the data structure byval means that a change to the data 
                // like this won't break the iteration. 
                _internalDataStructure = new int[] {1,2,3};
                return enumerator;
            }

            // Throwback from pre-generics code
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        public class EnumeratorThingy : IEnumerator<int>
        {
            private readonly int[] _values;
            private int _index = -1;

            public EnumeratorThingy(int[] values)
            {
                _values = values;
            }

            public int Current
            {
                get { return _values[_index]; }
            }
            // MoveNext needs to be called before any data can be read. This means
            // that it copes fine with collections containing no data
            public bool MoveNext()
            {
                _index++;
                return _index < _values.Length;
            }
            ///////
            public void Dispose()
            {
                return;
            }
            /////// We're not concerned about these methods either
            public void Reset()
            {
                throw new NotImplementedException();
            }
            object System.Collections.IEnumerator.Current
            {
                get { throw new NotImplementedException(); }
            }

        }
    }
}