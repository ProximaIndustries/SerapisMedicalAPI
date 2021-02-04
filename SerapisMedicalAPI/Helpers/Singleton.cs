using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerapisMedicalAPI.Helpers
{
    public class Singleton
    {
        private Singleton() { }

        public static Singleton _instance;
        public string Value { get; set; }


        // We now have a lock object that will be used to synchronize threads
        // during first access to the Singleton.
        private static readonly object _lock = new object();

        public static Singleton GetInstance(string value)
        {
            // This conditional is needed to prevent threads stumbling over the
            // lock once the instance is ready.
            if (_instance == null)
            {
                lock (_lock)
                {
                    // The first thread to acquire the lock, reaches this
                    // conditional, goes inside and creates the Singleton
                    // instance. Once it leaves the lock block, a thread that
                    // might have been waiting for the lock release may then
                    // enter this section. But since the Singleton field is
                    // already initialized, the thread won't create a new
                    // object.

                    _instance = new Singleton();
                    _instance.Value = value;

                }
            }

            return _instance;
        }

        //public static void TestSingleton(string value)
        //{
        //    Singleton singleton = Singleton.GetInstance(value);
        //    Console.WriteLine(singleton.Value);
        //}
    }
}
