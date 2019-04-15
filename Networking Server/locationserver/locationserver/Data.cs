using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace locationserver
{
    public class Data
    {
        public static Dictionary<string, string> dictionary = new Dictionary<string, string>();
               
        //checks it lookup is in the dictionary
        public bool checkDictionary(string lookup)
        {
            if (dictionary.ContainsKey(lookup))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //adds a lookup to the dictionary and replaces value if key already is in 
        public void addToDictionary(string lookup, string location)
        {
            if (dictionary.ContainsKey(lookup))
            {
                dictionary.Add(lookup, location);
            }
            else
            {
                changeDictionary(lookup, location);
            }
        }

        //updates a value of a key in the dictionary
        public void changeDictionary(string lookup, string update)
        {
            dictionary[lookup] = update;
        }

        //get the the value from a key in the dictionary 
        public string getLocation(string lookup)
        {
            string location = dictionary[lookup];

            return location;
        }

    }
}
