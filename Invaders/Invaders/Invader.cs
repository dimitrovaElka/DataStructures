using System;

public class Invader : IInvader
{
    public bool isDestroyed;
    public Invader(int damage, int distance)
    {
        this.Damage = damage;
        this.Distance = distance;
        this.isDestroyed = false;
    }
    
    public int Damage { get; set; }
    public int Distance { get; set; }

    public int CompareTo(IInvader other)
    {
        int compare = this.Distance.CompareTo(other.Distance);
        if (compare == 0)
        {
            compare = other.Damage.CompareTo(this.Damage);
        }
        return compare;
    }
}
