using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class Instock : IProductStock
{
    private readonly List<Product> byInsertion;
    private readonly Dictionary<string, Product> products;
    private readonly OrderedDictionary<string, Product> byLabel;
   // private readonly List<Product> byQuantity;
    private readonly Dictionary<int, HashSet<Product>> byQuantity;

    public Instock()
    {
        this.byInsertion = new List<Product>();
        this.products = new Dictionary<string, Product>();
        this.byLabel = new OrderedDictionary<string, Product>();
        this.byQuantity = new Dictionary<int, HashSet<Product>>();
       // this.byPrice = new OrderedDictionary<double, HashSet<Product>>((a, b) => a.CompareTo(b));
    }

    public int Count => this.byInsertion.Count;

    public void Add(Product product)
    {
        byInsertion.Add(product);
        if (!this.products.ContainsKey(product.Label))
        {
            products.Add(product.Label, product);
            byLabel[product.Label] = product;
            if (!this.byQuantity.ContainsKey(product.Quantity))
            {
                this.byQuantity[product.Quantity] = new HashSet<Product>();
            }
            byQuantity[product.Quantity].Add(product);
        }
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!this.products.ContainsKey(product))
        {
            throw new ArgumentException();
        }
       
        var seekProduct = this.byLabel[product];

        if (!this.byQuantity.ContainsKey(quantity))
        {
            this.byQuantity[quantity] = new HashSet<Product>();
        }
        if (this.byQuantity.ContainsKey(quantity))
        {
            this.byQuantity[seekProduct.Quantity].Remove(seekProduct);
        }
        seekProduct.Quantity = quantity;
        this.byQuantity[quantity].Add(seekProduct);
        
    }

    public bool Contains(Product product)
    {
        return products.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new IndexOutOfRangeException();
        }

        return byInsertion[index];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        return this.byInsertion.Where(x => x.Price == price).ToList();
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        var result = new List<Product>();
        if (this.byQuantity.ContainsKey(quantity))
        {
            result = this.byQuantity[quantity].ToList();
        }

        return result;
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        // List<Product> inRange = byPrice.Range(new Product("", lo, 0), false, new Product("", hi, 0), true).OrderByDescending(x => x.Price).ToList();
        return this.byLabel.Where(x => x.Value.Price > lo && x.Value.Price <= hi).OrderByDescending(x => x.Value.Price).Select(x => x.Value).ToList();
    }

    public Product FindByLabel(string label)
    {
        if (!products.ContainsKey(label))
        {
            throw new ArgumentException();
        }
        return products[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (count < 0 || count > this.Count)
        {
            throw new ArgumentException();
        }
        return this.byLabel.Take(count).Select(x => x.Value).ToList();
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (byInsertion.Count < count || count < 0)
        {
            throw new ArgumentException();
        }
        //  return this.byPrice.SelectMany(p => p.Value).OrderByDescending(p => p).Take(count);
        var result = this.byLabel.OrderByDescending(x => x.Value.Price).Take(count).Select(x => x.Value).ToList();
        return result;
    }

    public IEnumerator<Product> GetEnumerator()
    {
        // return byInsertion.GetEnumerator();
        foreach (var product in this.products)
        {
            yield return product.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
