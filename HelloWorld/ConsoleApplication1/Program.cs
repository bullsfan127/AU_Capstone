using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
       static void Main(string[] args)
        {
            //testing
            Console.WriteLine("Hello World");
           // No idea why it keeps using my other github name for commits
            Console.WriteLine("Hi, Computer.");
            sayHi(7);
        }

        
       /// <summary>
       /// Says Hi
       /// </summary>
       /// <param name="a">A is concat to the end</param>
 
       public static void sayHi(int a)
       {

           Console.WriteLine("Hi" + a);
       
       }
    }
}
