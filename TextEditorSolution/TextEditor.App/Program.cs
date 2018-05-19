using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        ITextEditor editor = new TextEditor();
        Dictionary<string, bool> users = new Dictionary<string, bool>();

        string line = string.Empty;
        Regex regex = new Regex("\"(.*)\"");
        while ((line = Console.ReadLine()) != "end")
        {
            var commandArgs = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Match match = regex.Match(line);
            try
            {
                switch (commandArgs[0])
                {
                    case "login":
                        if (!users.ContainsKey(commandArgs[1]))
                        {
                            users.Add(commandArgs[1], true);
                        }
                        editor.Login(commandArgs[1]);
                        break;
                    case "logout":
                        users[commandArgs[1]] = false;
                        editor.Logout(commandArgs[1]);
                        break;
                    case "users":
                        string prefix = "";
                        if (commandArgs.Length == 2)
                        {
                            prefix = commandArgs[1];
                        }
                        var list = editor.Users(prefix);
                        foreach (var item in list)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    default:
                        break;
                }
                string username = commandArgs[0];
                if (!(users.ContainsKey(username) && users[username]))
                {
                    continue;
                }

                string str = match.Groups[1].Value;
                switch (commandArgs[1])
                {
                    case "insert":
                        editor.Insert(username, int.Parse(commandArgs[2]), str);
                        break;
                    case "prepend":
                        editor.Prepend(username, str);
                        break;
                    case "substring":
                        editor.Substring(username, int.Parse(commandArgs[2]), int.Parse(commandArgs[3]));
                        break;
                    case "delete":
                        editor.Delete(username, int.Parse(commandArgs[2]), int.Parse(commandArgs[3]));
                        break;
                    case "clear":
                        editor.Clear(username);
                        break;
                    case "length":
                        Console.WriteLine(editor.Length(username));
                        break;
                    case "print":
                        Console.WriteLine(editor.Print(username));
                        break;
                    case "undo":
                        editor.Undo(username);
                        break;
                }
            }
            catch (Exception)
            {

            }

        }

    }
}