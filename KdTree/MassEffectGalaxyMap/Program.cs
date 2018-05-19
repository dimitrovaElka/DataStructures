using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        KdTree tree = new KdTree();

        int count = int.Parse(Console.ReadLine());
        int reportsCount = int.Parse(Console.ReadLine());
        int size = int.Parse(Console.ReadLine());
        Rectangle space = new Rectangle(0, size, 0, size);

        List<Point2D> starClusters = new List<Point2D>();
        for (int i = 0; i < count; i++)
        {
            var tokens = Console.ReadLine().Split();

            Point2D point = new Point2D(double.Parse(tokens[1]), double.Parse(tokens[2]));

            if (point.IsInRectangle(space))
            {
                starClusters.Add(point);
            }
        }

        starClusters.Sort((x, y) => x.X.CompareTo(y.X));
        foreach (var star in starClusters)
        {
            tree.Insert(star);
        }
        starClusters.Clear();

        for (int i = 0; i < reportsCount; i++)
        {
            var tokens = Console.ReadLine().Split().Skip(1).Select(double.Parse).ToArray();
            Rectangle rect = new Rectangle(tokens[0], tokens[0] + tokens[2], tokens[1], tokens[1] + tokens[3]);

            tree.GetPoints(starClusters.Add, rect, space);

            Console.WriteLine(starClusters.Count);
            starClusters.Clear();
        }
    }
}
