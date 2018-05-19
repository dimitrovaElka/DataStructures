using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private OrderedBag<LinkedListNode<T>> byAscending;
    private OrderedBag<LinkedListNode<T>> byDescending;
    private LinkedList<T> byInsertion;

    public FirstLastList()
    {
        this.byInsertion = new LinkedList<T>();
        this.byAscending = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this.byDescending = new OrderedBag<LinkedListNode<T>>((x, y) => y.Value.CompareTo(x.Value));
    }
    public int Count
    {
        get
        {
            return this.byInsertion.Count;
        }
    }

    public void Add(T element)
    {
        LinkedListNode<T> node = new LinkedListNode<T>(element);
        byInsertion.AddLast(node);
        this.byAscending.Add(node);
        this.byDescending.Add(node);
    }

    public void Clear()
    {
        this.byInsertion.Clear();
        this.byAscending.Clear();
        this.byDescending.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        if (!CountIsInBounds(count))
        {
            throw new ArgumentOutOfRangeException();
        }
        LinkedListNode<T> current = this.byInsertion.First;

        int iterator = 0;
        while (iterator < count)
        {
            yield return current.Value;
            current = current.Next;
            iterator++;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        if (!CountIsInBounds(count))
        {
            throw new ArgumentOutOfRangeException();
        }
        LinkedListNode<T> current = this.byInsertion.Last;

        int iterator = 0;
        while (iterator < count)
        {
            yield return current.Value;
            current = current.Previous;
            iterator++;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        if (!CountIsInBounds(count))
        {
            throw new ArgumentOutOfRangeException();
        }


        var maxElements = byDescending.Take(count);

        return maxElements.Select(x => x.Value);
    }

    public IEnumerable<T> Min(int count)
    {
        if (!CountIsInBounds(count))
        {
            throw new ArgumentOutOfRangeException();
        }
        var minElements = byAscending.Take(count);
        return minElements.Select(x => x.Value);
    }

    public int RemoveAll(T element)
    {
        LinkedListNode<T> node = new LinkedListNode<T>(element);
        foreach (var item in
            this.byAscending.Range(node, true, node, true))
        {
            this.byInsertion.Remove(item);
        }

        int countRemoved = byAscending.RemoveAllCopies(node);
        byDescending.RemoveAllCopies(node);

        return countRemoved;
    }

    private bool CountIsInBounds(int count)
    {
        return count >= 0 && count <= this.Count;
    }

}
