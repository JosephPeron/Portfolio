using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaEntry
{
    class Program
    {
        static void Main(string[] args)
        {

            string doWeGoRoundAgain;
            string doWeGoRoundAgainUpper;

            do
            {

                Console.WriteLine("Welcome to our Multiplex");
                Console.WriteLine("We are presently showing: ");
                Console.WriteLine("1. Legend (18)");
                Console.WriteLine("2. Macbeth (15)");
                Console.WriteLine("3. Everest (12A)");
                Console.WriteLine("4. A Walk in the Woods (15)");
                Console.WriteLine("5. Hotel Transylvania (U)");
                int filmnumber;

                do
                {
                    Console.Write("Enter the number of the film you wish to see: ");
                    filmnumber = int.Parse(Console.ReadLine());
                } while ((filmnumber < 1) || (filmnumber > 5));

                switch (filmnumber)
                {

                    case 1:
                        Console.Write("Enter your age: ");
                        string age1 = Console.ReadLine();
                        int numberage1 = int.Parse(age1);

                        if (numberage1 < 18)
                        {
                            Console.WriteLine("Access Denied - You are too young!");
                            Console.WriteLine("Press Enter to Continue");
                        }
                        else if (numberage1 >= 18)
                        {
                            Console.WriteLine("Enjoy the film!");
                            Console.WriteLine("Press Enter to Continue");
                        }
                        break;

                    case 2:
                        Console.Write("Enter your age: ");
                        string age2 = Console.ReadLine();
                        int numberage2 = int.Parse(age2);

                        if (numberage2 < 15)
                        {
                            Console.WriteLine("Access Denied - You are too young!");
                            Console.WriteLine("Press Enter to Continue");
                        }
                        else if (numberage2 >= 15)
                        {
                            Console.WriteLine("Enjoy the film!");
                            Console.WriteLine("Press Enter to Continue");
                        }
                        break;

                    case 3:
                        Console.Write("Enter your age: ");
                        string age3 = Console.ReadLine();
                        int numberage3 = int.Parse(age3);

                        if (numberage3 < 12)

                        {
                            Console.WriteLine("Access Denied - You are too young!");
                            Console.WriteLine("Press Enter to Continue");
                        }
                        else if (numberage3 >= 12)
                        {
                            Console.WriteLine("Enjoy the film!");
                            Console.WriteLine("Press Enter to Continue");
                        }
                        break;

                    case 4:
                        Console.Write("Enter your age: ");
                        string age4 = Console.ReadLine();
                        int numberage4 = int.Parse(age4);

                        if (numberage4 < 15)
                        {
                            Console.WriteLine("Access Denied - You are too young!");
                            Console.WriteLine("Press Enter to Continue");
                        }
                        else if (numberage4 >= 15)
                        {
                            Console.WriteLine("Enjoy the film!");
                            Console.WriteLine("Press Enter to Continue");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Enjoy the film!");
                        Console.WriteLine("Press Enter to Continue");
                        break;

                }
            Console.ReadLine();
            Console.WriteLine("Another customer? (Y/N): ");
            doWeGoRoundAgain = Console.ReadLine();
            doWeGoRoundAgainUpper = doWeGoRoundAgain.ToUpper();

            } while (doWeGoRoundAgainUpper == "Y");
        }
    }
}
	
