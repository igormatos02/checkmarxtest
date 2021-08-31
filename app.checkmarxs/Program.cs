using System;

namespace app.checkmarxs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select a screen number");
            Console.WriteLine("1 - New Order");
            Console.WriteLine("2 - Order Queue");
            var key = Console.ReadKey();
            if (key.KeyChar == '1')
            {
                Console.WriteLine("Type Waiter Id");
                Console.WriteLine("Type Table Number");
                Console.WriteLine("Select Function");
                Console.WriteLine("1 - Save Order");
                Console.WriteLine("2 - Add Dish 1");
                Console.WriteLine("2 - Add Dish 2");
            } else if (key.KeyChar == '2')
            {
                Console.WriteLine("Type Chef Id");
               
                Console.WriteLine("Select Function");
                Console.WriteLine("1 - Take Next Order");
                Console.WriteLine("2 - Deliver Order");
               
            }
        }
    }
}
