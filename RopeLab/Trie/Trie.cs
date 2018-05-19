using System;
using System.Collections.Generic;

public class Trie<Value>
{
    private Node root;

    private class Node
    {
        public Value val;
        public bool isTerminal;
        public Dictionary<char, Node> next = new Dictionary<char, Node>();
    }

    public Value GetValue(string key)
    {
        var x = GetNode(root, key, 0);
        if (x == null || !x.isTerminal)
        {
            throw new InvalidOperationException();
        }

        return x.val;
    }

    public bool Contains(string key)
    {
        var node = GetNode(this.root, key, 0);
        return node != null && node.isTerminal;
    }

    public void Insert(string key, Value val)
    {
        root = Insert(root, key, val, 0);
    }

    public IEnumerable<string> GetByPrefix(string prefix)
    {
        var results = new Queue<string>();
        var x = GetNode(root, prefix, 0);

        this.Collect(x, prefix, results);
        
        return results;
    }

    private Node GetNode(Node x, string key, int d)
    {
        if (x == null)
        {
            return null;
        }

        if (d == key.Length)
        {
            return x;
        }

        Node node = null;
        char c = key[d];

        if (x.next.ContainsKey(c))
        {
            node = x.next[c];
        }

        return GetNode(node, key, d + 1);
    }

    private Node Insert(Node x, string key, Value val, int d)
    {
        // Check if the given node is null and create and assign a new node if it is
        if (x == null)
        {
            x = new Node();
        }
        if (d == key.Length)  // Check if you are at the last symbol of the key 
        {
            x.val = val; // Assign the value
            // Make the node terminal
            x.isTerminal = true;
            return x;
        }
        Node node = null;
        char c = key[d];

        // Check if the symbol you're at is in the children of the node
        if (x.next.ContainsKey(c))
        {
            node = x.next[c];  // Move to the next node if it is
        }

        // Recursively call the insert for the next child of our node
        x.next[c] = Insert(node, key, val, d + 1);
        return x;  // Return the node
    }

    private void Collect(Node x, string prefix, Queue<string> results)
    {
        if (x == null)
        {
            return;
        }

        if (x.val != null && x.isTerminal)
        {
            results.Enqueue(prefix);
        }

        foreach (var c in x.next.Keys)
        {
            Collect(x.next[c], prefix + c, results);
        }
    }
}