
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    public class ReversedList<T> : IEnumerable<T>
{
    private T[] data;
    public ReversedList()
    {
        this.data = new T[2];
    }
    public int Count
    {
        get;
        private set;
    }

    public int Capacity()
    {
        return this.data.Length;
    }
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return this.data[this.Count - 1 - index];
        }

        set
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.data[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.data.Length <= this.Count)
        {
            this.Resize();
        }
        this.data[this.Count++] = item;
    }

    private void Resize()
    {
        T[] newArray = new T[this.Count * 2];
        Array.Copy(this.data, newArray, this.Count);
        this.data = newArray;
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        T item = this.data[this.Count - 1 - index];
        
        for (int i = this.Count - 1 - index; i < this.Count - 1; i++)
        {
            this.data[i] = this.data[i + 1];
        }
        this.Count--;
        if (this.Count <= this.data.Length / 4)
        {
            this.Shrink();
        }
        return item;
    }
    private void Shrink()
    {
        T[] newArray = new T[this.data.Length / 2];
        Array.Copy(this.data, newArray, this.Count);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
        {
            yield return this.data[i];

        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
class Program
    {
        static void Main()
        {
            var list = new ReversedList<int>();

            list.Add(5);
            list.Add(3);
            list.Add(2);
            list.Add(10);
            list.Add(7);
            list.Add(8);
            list.Add(9);
        Console.WriteLine("Count = {0}", list.Count);

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("--------------------");

            list.RemoveAt(1);
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("--------------------");

        list.RemoveAt(1);
        list.RemoveAt(2);

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("--------------------");
        }
    }

