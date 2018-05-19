using System;

class Program
{
    static void Main(string[] args)
    {
        AVL<int> avl = new AVL<int>();
        for (int i = 1; i < 10; i++)
        {
            avl.Insert(i);
        }

        avl.Delete(4);

        Console.WriteLine();
    }
}
