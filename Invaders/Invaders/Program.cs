public class Program
{
    public static void Main(string[] args)
    {
        Computer computer = new Computer(100);

        for (int i = 3; i < 10; i++)
        {
            computer.AddInvader(new Invader(2 * i, i));
        }

        computer.Skip(1);

        computer.DestroyTargetsInRadius(5);

        System.Console.WriteLine();
    }
}
