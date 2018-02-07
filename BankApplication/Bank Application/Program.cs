using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bank_Application
{
    class BankClass
    {
        private static Random letterGen = new Random();
        private List<Account> Accounts = new List<Account>();

        public void addAccount(Account account)
        {
            Accounts.Add(account);
        }

        public void RandomString()
        {

            for (int i = 0; i < 50; i++)
            {
                var chars = "abcdefghijklmnopqrstuvwxyz";
                char randomLetter;

                randomLetter = chars[letterGen.Next(chars.Length)];
                string randname = randomLetter.ToString();

                Console.WriteLine(randname);
            }

        }

        public void RandomNumber()
        {
        
        }
    }
    class Account
    {
        private string AccountName;
        private int AccountNumber;
        private static int BankAccountNumber;
        private decimal AccountBalance;
        public static bool finish;
        private List<string> Transactions = new List<string>();

        public Account(string name, decimal pBalance)
        {
            AccountName = name;
            AccountBalance = pBalance;

            AccountNumber = BankAccountNumber;
            BankAccountNumber++;
        }
        public void TransLog()
        {
            StreamWriter writer = new StreamWriter(AccountName + "Log.txt");
            for (int i = 0; i < Transactions.Count; i++)
            {
                writer.WriteLine("Log" + i + Transactions[i]);
            }

            writer.Flush();
            writer.Close();
        }

        public void GetAction()
        {
            Console.WriteLine("Do you want to withdraw money or deposit money");
            string action = Console.ReadLine();
            if (action == "withdraw")
            {
                Console.Write("How much money do you want to withdraw?: ");
                decimal pamount = int.Parse(Console.ReadLine());
                withdrawFunds(pamount);
            }
            else if (action == "deposit")
            {
                Console.Write("How much money do you want to deposit?: ");
                decimal pamount = int.Parse(Console.ReadLine());
                depositFunds(pamount);
            }
            else
            {
                Console.WriteLine("Please input withdraw or deposit");
                GetAction();
            }
        }

        public void withdrawFunds(decimal amount)
        {

            if (amount > 0)
            {
                if (AccountBalance - amount > 0)
                {
                    AccountBalance = AccountBalance - amount;
                    Transactions.Add("Withdrew £" + amount);
                    Console.WriteLine("You have £" + AccountBalance + "  in your account");

                }
                else
                {
                    Console.WriteLine("Not enough funds");
                    Transactions.Add("Attempted to withdraw £" + amount + " : Not enough funds");
                }
            }
            else
            {
                Transactions.Add("Attempted to steal by withdrawing a negative number");
            }
            Transactions.Add("New balance is : £" + AccountBalance);
        }

        public void depositFunds(decimal amount)
        {
            AccountBalance = AccountBalance + amount;
            Transactions.Add("Deposited £" + amount);
            Console.WriteLine("You have £" + AccountBalance + "  in your account");
            Transactions.Add("New balance is : £" + AccountBalance);

        }

        public void getvalues()
        {
            do
            {
                Console.WriteLine("What is account holder's name?: ");
                AccountName = Console.ReadLine();

            } while (string.IsNullOrEmpty(AccountName));


            Console.WriteLine(AccountName + "'s account has this much money: £" + AccountBalance);
        }

        public static void finishcheck()
        {
            Console.Write("Do you want to perform another request (yes or no): ");
            string checkerstring = Console.ReadLine();

            if (checkerstring == "yes")
            {
                finish = false;
            }
            else if (checkerstring == "no")
            {
                finish = true;
            }
            else
            {
                Console.WriteLine("Please input a vaild answer");
                finishcheck();
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Account newAccount = new Account("Joe", 100000);
                BankClass newBank = new BankClass();

                newBank.RandomString();
                newAccount.getvalues();

                do
                {
                    newAccount.GetAction();
                    newAccount.TransLog();
                    finishcheck();

                } while (finish == false);










                Console.ReadKey();
            }
        }
    }
}


