using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameObject 
{
    private const double Width = 10.0;
    private const double Height = 10.0;
    public GameObject(string name, double x1, double y1)
    {
        this.Name = name;
        this.X1 = x1;
        this.Y1 = y1;
    }

    public string Name { get; private set; }
    public double X1 { get; private set; }
    public double X2 => this.X1 + Width;
    public double Y1 { get; private set; }
    public double Y2 => this.Y1 + Height;

    public void Move(double newX1, double newY1)
    {
        this.X1 = newX1;
        this.Y1 = newY1;
    }

    public bool DetectCollision(GameObject other)
    {
        // X1 is less or equal to the current object's X2
        return other.X1 <= this.X2 && other.Y1 <= this.Y2 && other.Y2 >= this.Y1;
    }
}
