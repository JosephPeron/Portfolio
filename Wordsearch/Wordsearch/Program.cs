using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Wordsearch
{
    class Program
    {
        public static int col = 0;
        public static int row = 0;
        public static int numWords = 0;
        public static string[,] grid;
        public static List<wordStruct> wordData;
        public static bool empty = true;
        public struct wordStruct
        {
            public string word;
            public int StartxPos;
            public int StartyPos;
            public string direction;
            public int endXPos;
            public int endYPos;
            public bool found;
        }
        public static int startXinput;
        public static int startYinput;
        public static int endXinput;
        public static int endYinput;
        public static bool winner = true;

        private static Random letterGen = new Random();

        static void Main(string[] args)
        {
            string filePath = "";
            // loading the file from command line argument and handle expections
            #region loadfile

            if (args.Length > 0)
            {
                filePath = args[0];
                try
                {
                    ReadFile(filePath);
                }
                catch
                {
                    Console.WriteLine("Failed to read file");
                    Console.ReadLine();
                }

                DrawGrid();
            }
            else
            {
                Console.WriteLine("No arguments were given");
                Console.ReadLine();

                return;
            }

            #endregion 

            do
            {
                winner = GridChecking();
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                DrawGrid();
            } while (winner == false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

          }

        static void ReadFile(string filePath)
        {
            // reading the file line by line, seperating it in an array by every comma and new line
            StreamReader reader = File.OpenText(filePath);
            string[] elements = reader.ReadToEnd().Split(new string[] { ",", "\r\n" }, StringSplitOptions.None);
            reader.Close();

            wordData = new List<wordStruct>();
            // using the read values from the file and declaring them as corresponding variables
            Int32.TryParse(elements[0], out col);
            Int32.TryParse(elements[1], out row);
            Int32.TryParse(elements[2], out numWords);

            int startPosition = 3;

            for (int z = startPosition; z < elements.Length - 1; z += 4)
            {
                wordStruct tempStruct = new wordStruct();
                tempStruct.word = elements[z];
                tempStruct.StartxPos = int.Parse(elements[z + 1]);
                tempStruct.StartyPos = int.Parse(elements[z + 2]);
                tempStruct.direction = elements[z + 3];

                tempStruct.found = false;

                if (tempStruct.direction == "right")
                {
                    tempStruct.endXPos = tempStruct.StartxPos + tempStruct.word.Length - 1;
                }

                else if (tempStruct.direction == "left")
                {
                    tempStruct.endXPos = tempStruct.StartxPos - tempStruct.word.Length + 1;
                }
                else if (tempStruct.direction == "up")
                {
                    tempStruct.endYPos = tempStruct.StartyPos - tempStruct.word.Length - 1;
                }
                else if (tempStruct.direction == "down")
                {
                    tempStruct.endYPos = tempStruct.StartyPos + tempStruct.word.Length - 1;
                }
                else if (tempStruct.direction == "leftup")
                {
                    tempStruct.endXPos = tempStruct.StartxPos - tempStruct.word.Length + 1;
                    tempStruct.endYPos = tempStruct.StartyPos - tempStruct.word.Length - 1;
                }
                else if (tempStruct.direction == "leftdown")
                {
                    tempStruct.endXPos = tempStruct.StartxPos - tempStruct.word.Length + 1;
                    tempStruct.endYPos = tempStruct.StartyPos - tempStruct.word.Length - 1;
                }
                else if (tempStruct.direction == "rightup")
                {
                    tempStruct.endXPos = tempStruct.StartxPos + tempStruct.word.Length - 1;
                    tempStruct.endYPos = tempStruct.StartyPos - tempStruct.word.Length - 1;
                }
                else if (tempStruct.direction == "rightdown")
                {
                    tempStruct.endXPos = tempStruct.StartxPos + tempStruct.word.Length - 1;
                    tempStruct.endYPos = tempStruct.StartyPos - tempStruct.word.Length - 1;
                }

                wordData.Add(tempStruct);
            }

        }

        static void DrawGrid()
        {
            grid = new string[row, col];

            //create the grid
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    grid[i, j] = " ";
                }
            }

            //fill the grid with random letters
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    PlaceRandomLetter(i, j);
                }
            }

            //place the words over the random letters
            //-------
            //loop through the list of words
            for (int x = 0; x < numWords; x++)
            {
                int tempXpos = wordData[x].StartxPos;
                int tempYpos = wordData[x].StartyPos;
                string tempDirection = wordData[x].direction;
                //find where they start and their direction

                //for (to loop through letters) loop j 
                for (int j = 0; j < wordData[x].word.Length; j++)
                {
                    grid[tempYpos, tempXpos] = wordData[x].word[j].ToString();

                    switch (tempDirection)
                    {
                        case "down":
                            tempYpos++;
                            break;
                        case "up":
                            tempYpos--;
                            break;
                        case "left":
                            tempXpos--;
                            break;
                        case "right":
                            tempXpos++;
                            break;
                        case "leftdown":
                            tempXpos--;
                            tempYpos++;
                            break;
                        case "leftup":
                            tempXpos--;
                            tempYpos--;
                            break;
                        case "rightdown":
                            tempXpos++;
                            tempYpos++;
                            break;
                        case "rightup":
                            tempXpos++;
                            tempYpos--;
                            break;
                    }

                    //endfor
                }
            }


            //draws the grid
            Console.Write(" ");
            for (int counter = 0; counter < col; counter++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" " + (counter));
            }
            Console.WriteLine();
            for (int i = 0; i < row; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(i + " ");
                for (int j = 0; j < col; j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(grid[i, j] + " ");
                }

                Console.WriteLine();

            }
            
            Console.WriteLine();

            for (int b = 0; b < numWords; b++)
            {
                Console.WriteLine();
                if (wordData[b].found == true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(wordData[b].word);

                Console.ForegroundColor = ConsoleColor.White;
                
            }

            Console.WriteLine("\n");
        }

        static void PromptStartCoordinates()
        {
            // method to get a x and y start coordinate from the user
            Console.WriteLine("Input the co-ordinates you want the start at:");
            Console.WriteLine("X Co-ordinate: ");
            int numberHolder;
            numberHolder = int.Parse(Console.ReadLine());
            if (numberHolder >= 0 || numberHolder <= col)
            {
                startXinput = numberHolder;
            }
            else
            {
                Console.WriteLine("Invalid x given");
            }
            Console.WriteLine("Y Co-ordinate: ");
            numberHolder = int.Parse(Console.ReadLine());
            if (numberHolder >= 0 || numberHolder <= row)
            {
                startYinput = numberHolder;
            }
            else
            {
                Console.WriteLine("Invalid y given");
            }
        }

        static void PromptEndCoordinates()
        {
            // method to get a x and y end coordinate from the user
            Console.WriteLine("Input the co-ordinates you want the end at:");
            Console.WriteLine("X Co-ordinate: ");
            endXinput = int.Parse(Console.ReadLine());
            Console.WriteLine("Y Co-ordinate: ");
            endYinput = int.Parse(Console.ReadLine());
        }

        private static void PlaceRandomLetter(int i, int j)
        {
            // generates random letter to be placed in the grid
            var chars = "abcdefghijklmnopqrstuvwxyz";
            char randomLetter;

            randomLetter = chars[letterGen.Next(chars.Length)];

            grid[i, j] = randomLetter.ToString();
        }

        static Boolean GridChecking()
        {
            PromptStartCoordinates();

            PromptEndCoordinates();

            // checks if the inputted values match word coordinates
            for (int q = 0; q < numWords; q++)
            {
                bool startFound = false;
                bool endFound = false;

                if ((startXinput == wordData[q].StartxPos) && (startYinput == wordData[q].StartyPos))
                {
                    Console.WriteLine("Correct Start Position found!");
                    startFound = true;
                }
                if ((endXinput == wordData[q].endXPos) && (endYinput == wordData[q].endYPos))
                {
                    Console.WriteLine("Correct End Position found!");
                    endFound = true;
                }

                if (startFound == true && endFound == true)
                {
                    Console.WriteLine("word has been found: " + wordData[q].word);

                    wordStruct tempStruct = new wordStruct();
                    tempStruct.word = wordData[q].word;
                    tempStruct.StartxPos = wordData[q].StartxPos;
                    tempStruct.StartyPos = wordData[q].StartyPos;
                    tempStruct.direction = wordData[q].direction;

                    tempStruct.found = true; //updates so word is known to be found

                    wordData[q] = tempStruct;
                }
            }
            Boolean gameState =  WinnerChecker();
            return gameState;
        }

        static bool WinnerChecker()
        {
            // checker to see if all words have been found 
            winner = false;
            for (int p = 0; p < numWords; p++)
            {
                if (wordData[p].found == true)
                {
                    winner = true;
                }
            }
            return winner;

        }
    }
}


