using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CountSymbols
{
    private SortedList<char, int> bySymbols;
 //   private Dictionary<char, int> dictionary;

    public CountSymbols()
    {
        this.bySymbols = new SortedList<char, int>();
//        this.dictionary = new Dictionary<char, int>();
    }

    public void Add(char symbol)
    {
        if (!this.bySymbols.ContainsKey(symbol))
        {
            this.bySymbols[symbol] = 0;
        }
        this.bySymbols[symbol]++;
    }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        foreach (var kvp in this.bySymbols)
        {
            result.AppendLine($"{kvp.Key}: {kvp.Value} time/s");
        }
        return result.ToString().TrimEnd();
    }
}
