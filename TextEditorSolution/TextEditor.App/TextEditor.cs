using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

public class TextEditor : ITextEditor
{
    private Trie<BigList<char>> users;
    private Dictionary<string, Stack<string>> cache;

    public TextEditor()
    {
        this.users = new Trie<BigList<char>>();
        this.cache = new Dictionary<string, Stack<string>>();
    }

    public void Clear(string username)
    {
        this.Cache(username);
        this.users.GetValue(username).Clear();
    }

    public void Delete(string username, int startIndex, int length)
    {
        this.Cache(username);
        var list = this.users.GetValue(username);
        list.RemoveRange(startIndex, length);

        //string oldValue = string.Join("", users.GetValue(username));

        //if (oldValue.Length >= startIndex + length)
        //{
        //    string newValue = oldValue.Substring(0, startIndex) + oldValue.Substring(startIndex + length);
        //    users.GetValue(username).Clear();
        //    users.GetValue(username).Add(newValue);
        //}

    }

    public void Insert(string username, int index, string str)
    {
        this.Cache(username);
        var strToChars = str.ToCharArray();
        int count = strToChars.Length;
        var list = this.users.GetValue(username);
        if (index < list.Count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Insert(index + i, strToChars[i]);
            }
        }
        else
        {
            list.AddRange(strToChars);
        }
        
    }

    public int Length(string username)
    {
        var str = users.GetValue(username);
        int result = str.Count;
        return result;
    }

    public void Login(string username)
    {
        this.users.Insert(username, new BigList<char>());
        this.cache[username] = new Stack<string>();
    }

    public void Logout(string username)
    {
        this.cache.Remove(username);
      //  this.users.GetByPrefix(username).
    }

    public void Prepend(string username, string str)
    {
        this.Cache(username);
        this.users.GetValue(username).AddRangeToFront(str.ToCharArray());
    }

    public string Print(string username)
    {
        // var str = users.GetValue(username).ToString();
        // string result = str.TrimEnd('}').TrimStart('{');
        string result = string.Join("", users.GetValue(username));
        return result;
    }

    public void Substring(string username, int startIndex, int length)
    {
        this.Cache(username);
        var list = this.users.GetValue(username).GetRange(startIndex, length);
        this.users.Insert(username, list);

        //StringBuilder builder = new StringBuilder();
        //for (int i = startIndex; i < startIndex + length; i++)
        //{
        //    builder.Append(list[i]);
        //}
        //list = new BigList<char>(builder.ToString().Select(x => x.ToString()));
    }

    public void Undo(string username)
    {
        if (this.cache.Count == 0)
        {
            return;
        }

        this.users.Insert(username, new BigList<char>(this.cache[username].Pop()));
    }

    public IEnumerable<string> Users(string prefix = "")
    {
        if (prefix == "")
        {
            return cache.Keys;
        }
        var list = cache.Keys.Where(u => u.StartsWith(prefix)).ToList();
        return list;
    }

    private void Cache(string username)
    {
        var list = this.users.GetValue(username);
        this.cache[username].Push(string.Join("", list));
    }
}

