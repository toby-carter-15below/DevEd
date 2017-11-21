using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LinqFromScratch
{
    [TestClass]
    public class _035_DelegateDigression
    {
        // We saw use of some delegates in the last page, so this starts with a second look at the
        // same sort of thing, before taking them in a different direction.

        // Here is a simple delegate that we'll be using. It takes a string and returns void, simples.
        // It is a stupid, nonsensical example, we'd not want to do this in production code, but it
        // does show how we are passing around functions rather than variables.
        // Couldn't we use an Action<string> instead? Yes we could...
        public delegate void DisplayMessage(string message);

        // And a simple method that invokes the provided DisplayMessage delegate with a value.
        public void DelegateRunner(DisplayMessage displayer)
        {
            Console.WriteLine("About to call...");
            displayer("this is the message");
            Console.WriteLine("Finished call.");
        }

        [TestMethod]
        public void _1_SimpleDelegateCallWithLambda()
        {
            DisplayMessage displayer = x => Console.WriteLine(x);
            DelegateRunner(displayer);
        }

        [TestMethod]
        public void _2_SimpleDelegateCallWithMethod()
        {
            // We don't always need to write lambdas, we can use a method too.
            // Remember, we're still passing the function, not its result though.
            DelegateRunner(BackwardsDisplayingMethod);
        }

        public void BackwardsDisplayingMethod(string somethingToDisplay)
        {
            Console.WriteLine(somethingToDisplay.Reverse().ToArray());
        }

        // As mentioned, we can use the generic Action<T...> and Func<T...> for a lot of cases and cut down on
        // the number of declarations we make. But the signature becomes a little less clear, not just the
        // proliference of generic types, but you can probably deduce what a DisplayMessage delegate is intended
        // for much more easily than an Action<string>
        // LINQ relies on generics, the Where method's delegate is typed from the collection it's processing, but
        // if you're not writing a truly generic type then there is an argument for being explicit
        public void ActionRunner(Action<string> displayer)
        {
            Console.WriteLine("About to call...");
            displayer("this is the message");
            Console.WriteLine("Finished call.");
        }

        [TestMethod]
        public void _3_SimpleDelegateCallFromMethod()
        {
            Action<string> displayer = x => Console.WriteLine(x);
            ActionRunner(displayer);
        }



        // Delegates are key to events, and events are key to a bunch of stuff in the .Net framework. Here we have a
        // simple class that raises events to signify a percentage completion as that fits nicely into a test
        // framework, but the most common events are click handlers and the like which ultimately boil down to the
        // same sort of process.
        public class ImportantProcess
        {
            // this delegate type could be defined anywhere, not just in this class
            public delegate void PercentageCompleteDelegate(int percentage);

            public event PercentageCompleteDelegate PercentageComplete;
            //public event Action<int> PercentageComplete;

            // Passing our percentage value as a parameter almost seems backwards as we are kind of trying to return
            // values to the code that called us, but remember that we're staying within our control flow and calling
            // back to them, rather than returning. The term Callback may be one that you recognise.

            public void Go()
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 10 == 0)
                    {
                        // fire the event every 10 iterations
                        PercentageComplete.Invoke(i);
                    }
                }
            }
        }

        [TestMethod]
        public void _4_EventExample()
        {
            var importantProcess = new ImportantProcess();
            // Register a couple of handlers, both with lambdas and methods.
            // Multiple different objects, potentially in different threads could register for the events
            importantProcess.PercentageComplete += x => Console.WriteLine("Done " + x + " percent");
            importantProcess.PercentageComplete += ProgressUpdated;

            importantProcess.Go();
        }

        private void ProgressUpdated(int percentageComplete)
        {
            Console.WriteLine(percentageComplete + "%");
        }


        // The "event" keyword gives us some syntactic sugar around the underlying delegate mechanism,
        // both for the subscriber code registering its handler, and the provider code doing the same.
        [TestMethod]
        public void _5_EventExampleWithWorkingsExposed()
        {
            var manualImportantProcess = new ManualImportantProcess();
            // Here we'll register the events by calling methods that we've created manually, rather than letting
            // the compiler do something similar in the background.
            manualImportantProcess.RegisterPercentageCompleteEvent(x => Console.WriteLine("Done " + x + " percent"));
            manualImportantProcess.RegisterPercentageCompleteEvent(ProgressUpdated);
            manualImportantProcess.Go();

            Console.WriteLine("\r\n And to prove a point...\r\nNote the add and remove PercentageComplete methods:");
            foreach (var method in new ImportantProcess().GetType().GetMethods())
            {
                Console.WriteLine("  found method: " + method.Name);
            }
        }

        public class ManualImportantProcess
        {
            public delegate void PercentageCompleteDelegate(int percentage);
            // public event PercentageCompleteDelegate PercentageComplete;
            // ditch the event keyword and make it private
            private PercentageCompleteDelegate PercentageComplete;

            // then create a method to register for events
            public void RegisterPercentageCompleteEvent(PercentageCompleteDelegate handler)
            {
                PercentageComplete = (PercentageCompleteDelegate)Delegate.Combine(PercentageComplete, handler);
            }

            public void Go()
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i % 10 == 0)
                    {
                        PercentageComplete.Invoke(i);
                    }
                }
            }
        }
    }
}
