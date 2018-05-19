using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Computer : IComputer
{
    private List<Invader> byInsertion;
    private OrderedDictionary<int, List<Invader>> byDistance;
    public Computer(int energy)
    {
        if (energy < 0)
        {
            throw new ArgumentException();
        }
        this.Energy = energy;
        this.byInsertion = new List<Invader>();
        this.byDistance = new OrderedDictionary<int, List<Invader>>();
    }

    public int Energy
    {
        get; private set;
    }

    public void Skip(int turns)
    {
        // Skips the given turns in time. Every invader moves a single step closer to the ship with each turn. All invaders that reach the ship are destroyed, even if the ship is already destroyed.
        foreach (var i in byInsertion.Where(i => i.isDestroyed == false))
        {
            i.Distance -= turns;
            if (i.Distance <= 0)
            {
                i.isDestroyed = true;
                this.Energy -= i.Damage;
                if (this.Energy <= 0)
                {
                    break;
                }
            }
        }
    }

    public void AddInvader(Invader invader)
    {
        byInsertion.Add(invader);
        if (!byDistance.ContainsKey(invader.Distance))
        {
            byDistance[invader.Distance] = new List<Invader>();
        }
        byDistance[invader.Distance].Add(invader); 
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        // Destroys the given count of targets prioritizing them first by distance, then by damage
        int i = 0;
        foreach (var item in this.byDistance)
        {
            foreach (var inv in item.Value.OrderByDescending(x => x.Damage))
            {
                if (i > count)
                {
                    break;
                }
                inv.isDestroyed = true;
                i++;
            }
        }
    }

    public void DestroyTargetsInRadius(int radius)
    {
        // Destroys all targets in the given radius (all invaders such that distance ≤ radius)
        var toDestroy = byDistance.Where(i => i.Key <= radius);
        if (toDestroy.Count() == 0)
        {
            return;
        }
        foreach (var item in toDestroy)
        {
            foreach (var inv in item.Value)
            {
                inv.isDestroyed = true;
            }
        }
    }

    public IEnumerable<Invader> Invaders()
    {
        // returns a collection with all remaining invaders ordered by their appearance on the radar
        return byInsertion.Where(i => i.isDestroyed == false).ToList<Invader>();
    }
}
