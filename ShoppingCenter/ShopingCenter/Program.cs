using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        int commandsNumber = int.Parse(Console.ReadLine());
        ShoppingCenter center = new ShoppingCenter();
        for (int i = 0; i < commandsNumber; i++)
        {

            string line = Console.ReadLine();

            int firstSpace = line.IndexOf(" ");

            string command = line.Substring(0, firstSpace);
            string[] args = line.Substring(firstSpace + 1).Split(';');
            List<Product> result = new List<Product>();
            switch (command)
            {
                case "AddProduct":
                    string name = args[0];
                    double price = double.Parse(args[1]);
                    string producer = args[2];

                    Product p = new Product(name, price, producer);
                    center.Add(p);
                    Console.WriteLine("Product added");
                    break;
                case "DeleteProducts":
                    int count = 0;
                    if (args.Length == 1)  // delete producer
                    {
                        count = center.DeleteProductsByProducer(args[0]);
                    }
                    else
                    {
                        count = center.DeleteProductsByProducerAndName(
                            args[0],
                            args[1]);
                    }
                    if (count == 0)
                    {
                        Console.WriteLine("No products found");
                    }
                    else
                    {
                        Console.WriteLine(count + " products deleted");
                    }
                    break;
                case "FindProductsByName":
                    result = center.FindProductsByName(args[0]).ToList();
                    break;
                case "FindProductsByProducer":
                    result = center.FindProductsByProducer(args[0]).ToList();
                    break;
                case "FindProductsByPriceRange":
                    result = center.FindProductsByPriceRange(
                                double.Parse(args[0]),
                                double.Parse(args[1]))
                                .OrderBy(x => x)
                                .ToList();
                    break;

            }
            if (command.StartsWith("Find"))
            {
                if (result.Count != 0)
                {
                    Console.WriteLine(string.Join("\n",result));
                }
                else
                {
                    Console.WriteLine("No products found");
                }
            }
        }
    }
}
