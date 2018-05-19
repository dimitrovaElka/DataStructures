using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static void Main()
    {
        // reads some text from the console and counts the occurrences of each character in it. Print the results in alphabetical (lexicographical) order
        string input = Console.ReadLine();
        var dict = new CountSymbols();
        foreach (char symbol in input)
        {
            dict.Add(symbol);
        }

        Console.WriteLine(dict);
    }
}
