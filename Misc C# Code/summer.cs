using System;

class Program
{
	static void Main(string[] args)
	{
		int v1, v2, v3, v4;
		
		Console.WriteLine("This is a program sums four numbers");
		Console.WriteLine("Input your numbers");
		Console.WriteLine("Press Enter after each one");
		
		v1 = int.Parse(Console.ReadLine());
		v2 = int.Parse(Console.ReadLine());
		v3 = int.Parse(Console.ReadLine());
		v4 = int.Parse(Console.ReadLine());
		
		int sum = v1 + v2 + v3 + v4;
		Console.WriteLine("The sum is " + sum);
	}
}