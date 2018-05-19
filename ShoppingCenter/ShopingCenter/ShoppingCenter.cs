using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;


public class ShoppingCenter
{
    Dictionary<string, List<Product>> byProducer;
    Dictionary<string, List<Product>> byName;
    Dictionary<string, List<Product>> byNameAndProducer;
    OrderedBag<Product> byPrice;

    public ShoppingCenter()
    {
        this.byProducer = new Dictionary<string, List<Product>>();
        this.byName = new Dictionary<string, List<Product>>();
        this.byNameAndProducer = new Dictionary<string, List<Product>>(); 
        this.byPrice = new OrderedBag<Product>(
            (x, y) => x.Price.CompareTo(y.Price));
    }

    public void Add(Product product)
    {
        var producer = product.Producer;
        if (!this.byProducer.ContainsKey(producer))
        {
            this.byProducer[producer] = new List<Product>();
        }
        this.byProducer[producer].Add(product);

        var productName = product.Name;
        if (!this.byName.ContainsKey(productName))
        {
            this.byName[productName] = new List<Product>();
        }
        this.byName[productName].Add(product);
        this.byPrice.Add(product);
        string nameAndProducer = $"{product.Name}|{product.Producer}";
        if (!this.byNameAndProducer.ContainsKey(nameAndProducer))
        {
            this.byNameAndProducer[nameAndProducer] = new List<Product>();
        }
        this.byNameAndProducer[nameAndProducer].Add(product);
    }

    public int DeleteProductsByProducer(string producer)
    {
        if (!this.byProducer.ContainsKey(producer))
        {
            return 0;
        }

        IEnumerable<Product> productsToRemove = this.byProducer[producer];
        int count = 0;
        byProducer.Remove(producer);
        foreach (Product p in productsToRemove)
        {
            string key = $"{p.Name}|{p.Producer}";
            string name = p.Name;
            this.byName[name].Remove(p);
            this.byPrice.Remove(p);
            this.byNameAndProducer[key].Remove(p);
            count++;
        }
            return count;
    }

    public int DeleteProductsByProducerAndName(string name, string producer)
    {
        string key = $"{name}|{producer}";
        if (!this.byNameAndProducer.ContainsKey(key))
        {
            return 0;
        }

        List<Product> result = this.byNameAndProducer[key];
        int count = result.Count;

        foreach (var product in result)
        {
            this.byProducer[product.Producer].Remove(product);
            this.byPrice.Remove(product);
            this.byName[product.Name].Remove(product);
        }
        this.byNameAndProducer.Remove(key);

        return count;
    }

    public IEnumerable<Product> FindProductsByName(string name)
    {
        if (!this.byName.ContainsKey(name))
        {
            return Enumerable.Empty<Product>();
        }
        return byName[name].OrderBy(x => x); 
    }

    public IEnumerable<Product> FindProductsByProducer(string producer)
    {
        if (!this.byProducer.ContainsKey(producer))
        {
            return Enumerable.Empty<Product>();
        }
        return byProducer[producer].OrderBy(x => x);
    }

    public IEnumerable<Product> FindProductsByPriceRange(double low, double high)
    {
        return this.byPrice
            .Range(new Product("", low, ""), true, new Product("", high, ""), true).OrderBy(x => x);
    }
}
