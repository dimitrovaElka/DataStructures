using System;

class Example
{
    static void Main()
    {
        HashSet<int> firstSet = new HashSet<int>();
        HashSet<int> secondSet = new HashSet<int>();
        firstSet.Add(4);
        firstSet.Add(100);
        firstSet.Add(2);
        firstSet.Add(200);
        firstSet.Add(7);

        secondSet.Add(4);
        secondSet.Add(40);
        secondSet.Add(100);
        secondSet.Add(1);
        secondSet.Add(7);

        HashSet<int> unionSet = firstSet.UnionWith(secondSet);
        Console.WriteLine("union:" + string.Join(" ", unionSet).ToString());

        HashSet<int> intersectSet = firstSet.IntersectWith(secondSet);
        Console.WriteLine("intersection:" + string.Join(" ", intersectSet).ToString());

        HashSet<int> exceptSet = firstSet.Except(secondSet);
        Console.WriteLine("except:" + string.Join(" ", exceptSet).ToString());

        HashSet<int> symetric = firstSet.SymmetricExcept(secondSet);
        Console.WriteLine("symetric except:" + string.Join(" ", symetric).ToString());


        //HashTable<string, int> grades = new HashTable<string, int>();

        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");

        //grades.Add("Peter", 3);
        //grades.Add("Maria", 6);
        //grades["George"] = 5;
        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");

        //grades.AddOrReplace("Peter", 33);
        //grades.AddOrReplace("Tanya", 4);
        //grades["George"] = 55;
        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");

        //Console.WriteLine("Keys: " + string.Join(", ", grades.Keys));
        //Console.WriteLine("Values: " + string.Join(", ", grades.Values));
        //Console.WriteLine("Count = " + string.Join(", ", grades.Count));
        //Console.WriteLine("--------------------");

        //grades.Remove("Peter");
        //grades.Remove("George");
        //grades.Remove("George");
        //Console.WriteLine("Grades:" + string.Join(",", grades));
        //Console.WriteLine("--------------------");

        //Console.WriteLine("ContainsKey[\"Tanya\"] = " + grades.ContainsKey("Tanya"));
        //Console.WriteLine("ContainsKey[\"George\"] = " + grades.ContainsKey("George"));
        //Console.WriteLine("Grades[\"Tanya\"] = " + grades["Tanya"]);
        //Console.WriteLine("--------------------");
    }
}
