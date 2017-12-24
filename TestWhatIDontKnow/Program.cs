using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWhatIDontKnow
{
    class Program
    {
        static void Main(string[] args)
        {
            bool valor = true;
            var demo = new AsyncAwaitDemo();

            valor = demo.DoStuff().Result;

            while (valor)
           {
               Console.WriteLine("Doing Stuff on the Main Thread...................");
               Console.ReadKey();
           }
        }
    }

    public class AsyncAwaitDemo
    {
        public async Task<bool> DoStuff()
        {
            return await LongRunningOperation();
        }

        public static Task<bool> LongRunningOperation()
        {
            int counter;

           return Task.Run(() =>
            {
                for (counter = 0; counter < 50000; counter++)
                {
                    Console.WriteLine(counter);
                }

                return false;

            });     
            
        }
    }
}
