using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node root;

    public BinarySearchTree()
    {
    }
    private BinarySearchTree(Node node)
    {
        this.Copy(node);
    }

    private void Copy(Node node)
    {
        if (node == null)
        {
            return;
        }
        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    public void Insert(T value)
    {
        this.root = this.Insert(this.root, value);
    }
    private Node Insert(Node node, T value)
    {
        if (node == null)
        {
            return new Node(value);
        }
        int compare = node.Value.CompareTo(value);
        if (compare > 0)
        {
            node.Left = this.Insert(node.Left, value);
        }
        else if (compare < 0)
        {
            node.Right = this.Insert(node.Right, value);
        }
        return node;
    }
    public bool Contains(T value)
    {
        Node current = this.root;
        while (current != null)
        {
            if (value.CompareTo(current.Value) < 0)
            {
                current = current.Left;
            }
            else if (value.CompareTo(current.Value) > 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }
        return current != null;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }
        if (this.root.Left == null && this.root.Right == null)
        {
            this.root = null;
            return;
        }
        Node parent = null;
        Node current = this.root;

        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }
        if (current.Right != null)
        {
            parent.Left = current.Right;
        }
        else
        {
            parent.Left = null;
        }
    }

    public BinarySearchTree<T> Search(T item)
    {
        Node current = this.root;
       
        while (current != null)
        {
            int compare = current.Value.CompareTo(item);
            if (compare > 0)
            {
                current = current.Left;
            }
            else if (compare < 0)
            {
                current = current.Right;
            }
            else
            {
                return new BinarySearchTree<T>(current);
            }
        }
        return null;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        List<T> result = new List<T>();
        this.Range(this.root, result, startRange, endRange);
        return result;
    }

    private void Range(Node node, List<T> result, T start, T end)
    {
        if (node == null)
        {
            return;
        }
        int compareLow = node.Value.CompareTo(start);
        int compareHigh = node.Value.CompareTo(end);
        if (compareLow > 0)
        {
            this.Range(node.Left, result, start, end);
        }
        if (compareLow >= 0 && compareHigh <= 0)
        {
            result.Add(node.Value);
        }
        if (compareHigh < 0)
        {
            this.Range(node.Right, result, start, end);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }
        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
            this.Left = null;
            this.Right = null;

        }
        public T Value { get; set; }
        public int Count { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);

        //  bst.Delete(5);
        BinarySearchTree<int> search = bst.Search(8);
        search.Insert(9);
        Console.WriteLine(bst.Contains(9));
        //List<int> result = new List<int>();
        //bst.EachInOrder(result.Add);
        //Console.WriteLine(string.Join(" ", result));
    }
}
