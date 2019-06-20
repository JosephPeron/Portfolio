using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Bank
{

    string randName;
    int randNumber;
    private static Random rand = new Random(DateTime.Now.Second);
    private List<Account> Accounts = new List<Account>();

    public void addAccount(Account account)
    {
        Accounts.Add(account);
    }

    public void RandomName()
    {
        string[] maleNames = { "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian" };
        string[] femaleNames = { "abby", "abigail", "adele", "adrian" };
        string[] lastNames = { "abbott", "acosta", "adams", "adkins", "aguilar" };

        Random rand = new Random(DateTime.Now.Second);
        if (rand.Next(1, 2) == 1)
        {
            randName = maleNames[rand.Next(0, maleNames.Length - 1)];
        }
        else
        {
            randName = femaleNames[rand.Next(0, femaleNames.Length - 1)];
        }

    }

    public int RandomNumber()
    {
        Random random = new Random();
        int randomNumber = random.Next(0, 1000);
        randNumber = randomNumber;
        return randNumber;
    }
}

