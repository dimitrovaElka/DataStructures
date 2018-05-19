using System;

public class KdTree
{
    private const int K = 2;
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(Point2D point)
    {
        Node current = this.Root;

        int depth = 0;
        while (current != null)
        {
            if (current.Point.CompareTo(point) == 0)
            {
                break;
            }
            int compare = depth % 2;

            if (compare == 0)
            {
                int compareX = current.Point.X.CompareTo(point.X);
                if (compareX > 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }
            else
            {
                int compareY = current.Point.Y.CompareTo(point.Y);
                if (compareY > 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }
            depth++;
        }
        return current != null;
    }

    public void Insert(Point2D point)
    {
        this.root = Insert(root, point, 0);
    }

    private Node Insert(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return new Node(point);
        }
        int compare = depth % K;

        if (compare == 0)
        {
            int compareX = node.Point.X.CompareTo(point.X);
            if (compareX > 0)
            {
                node.Left = this.Insert(node.Left, point, depth + 1);
            }
            else
            {
                node.Right = this.Insert(node.Right, point, depth + 1);
            }
        }
        else
        {
            int compareY = node.Point.Y.CompareTo(point.Y);
            if (compareY > 0)
            {
                node.Left = this.Insert(node.Left, point, depth + 1);
            }
            else
            {
                node.Right = this.Insert(node.Right, point, depth + 1);
            }
        }

        return node;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }

    public void GetPoints(Action<Point2D> action, Rectangle rectangle, Rectangle space, int depth = 0)
    {
        this.EachInOrder(this.Root, action, rectangle, space, depth);
    }

    private void EachInOrder(Node node, Action<Point2D> action, Rectangle rectangle, Rectangle space, int depth)
    {
        if (node == null)
        {
            return;
        }

        if (node.Point.IsInRectangle(rectangle))
        {
            action(node.Point);
        }

        Rectangle leftRect;
        Rectangle rightRect;

        if (depth % 2 == 0)
        {
            leftRect = new Rectangle(space.X1, node.Point.X, space.Y1, space.Y2);
            rightRect = new Rectangle(node.Point.X, space.X2, space.Y1, space.Y2);
        }
        else
        {
            leftRect = new Rectangle(space.X1, space.X2, space.Y1, node.Point.Y);
            rightRect = new Rectangle(space.X1, space.X2, node.Point.Y, space.Y2);
        }

        if (rectangle.Intersects(leftRect))
        {
            this.EachInOrder(node.Left, action, rectangle, leftRect, depth + 1);
        }

        if (rectangle.Intersects(rightRect))
        {
            this.EachInOrder(node.Right, action, rectangle, rightRect, depth + 1);
        }
    }
}
