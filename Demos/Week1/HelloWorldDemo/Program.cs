using System;

namespace HelloWorldDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Please enter a word.");
            string myRespose = Console.ReadLine();
            
            Console.WriteLine($"The input was: {myRespose}" );
        }
    }
}
