using System;

class Sums
{
	static void Main()
	{
		Console.WriteLine("This program adds two numbers together");
		Console.WriteLine("First  Number : ");
		string number1Text = Console.ReadLine();
		int number1 = int.Parse(number1Text);
		Console.WriteLine("Second Number : ");
		string number2Text = Console.ReadLine();
		int number2 = int.Parse(number2Text);
		int result = number1 + number2;
		Console.WriteLine("Sum is  : " + result);
	}
}