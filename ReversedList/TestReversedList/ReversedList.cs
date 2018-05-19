using System;
using System.Collections.Generic;

public class RevList<T>
{
    private T[] data;
    public RevList()
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
        T item = this.data[index];
        for (int i = index; i < this.Count; i++)
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
}


