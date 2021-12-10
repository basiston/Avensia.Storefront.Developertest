using System;
using StructureMap;

namespace Avensia.Storefront.Developertest
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new DefaultRegistry());

            var productListVisualizer = container.GetInstance<ProductListVisualizer>();

            var shouldRun = true;
            DisplayOptions();
            int i = 0;

            while (shouldRun)
            {
                Console.Write("Enter an option: ");
               
                var input = Console.ReadKey();
                Console.WriteLine("\n");
                Console.WriteLine("Currency :");
                string currency = Console.ReadLine();
                Console.WriteLine("\n");
                switch (input.Key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        Console.WriteLine("Printing all products");
                        productListVisualizer.OutputAllProduct(currency);
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        Console.WriteLine("\n");
                        Console.WriteLine("Start:");
                        int start = Convert.ToInt32( Console.ReadLine());
                        Console.WriteLine("\n");
                        Console.WriteLine("Page Size :");
                        int size = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Printing paginated products");
                        productListVisualizer.OutputPaginatedProducts(start,size,currency);
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        Console.WriteLine("\n");
                        Console.WriteLine("Price From:");
                        double from = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\n");
                        Console.WriteLine("Price To:");
                        double to = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Printing products grouped by price");
                        productListVisualizer.OutputProductGroupedByPriceSegment(currency, from,to);
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        Console.WriteLine("Printing products in chunk of five");
                        productListVisualizer.ListProductInChunk(i,currency);
                        i = i + 5;
                        break;
                    case ConsoleKey.Q:
                        shouldRun = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine();
                DisplayOptions();
            }

            Console.Write("\n\rPress any key to exit!");
            Console.ReadKey();
        }

        private static void DisplayOptions()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1 - Print all products");
            Console.WriteLine("2 - Print paginated products");
            Console.WriteLine("3 - Print products grouped by price");
            Console.WriteLine("4 - Print products chunk of 5");
            Console.WriteLine("q - Quit");
        }
    }
}
