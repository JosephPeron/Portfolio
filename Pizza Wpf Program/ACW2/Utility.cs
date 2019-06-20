using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACW2
{
    public class Utility
    {      
        public static List<InventoryItem> inventoryData;
        public static List<MenuItem> menuData;

        //reading in the inventory.txt file and storing as variable
        public static void InventoryReader()
        {
            StreamReader reader = new StreamReader("inventory.txt");
            string[] inventoryElements = reader.ReadToEnd().Split(new string[] { ", ", "\r\n" }, StringSplitOptions.None);
            reader.Close();

            inventoryData = new List<InventoryItem>();

            for (int i = 0; i < inventoryElements.Length - 1; i += 4)
            {
                string category = inventoryElements[i];
                string name = inventoryElements[i + 1];
                double costPerUnit = double.Parse(inventoryElements[i + 2]);
                double quantity = double.Parse(inventoryElements[i + 3]);

                InventoryItem item = new InventoryItem(category, name, costPerUnit, quantity);
                inventoryData.Add(item);
            }
        }

        //reading in the menu.txt file and storing as variables
        public static void MenuReader()
        {
            //read the inventory.txt file
            StreamReader reader = new StreamReader("menu.txt");
            //seperates inventory.txt by new lines
            string[] lines = reader.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            reader.Close();

            menuData = new List<MenuItem>();

            //store specific array positions to their corresponding variable
            for (int i = 0; i < lines.Length; i++)
            {
                string[] menuDivider = lines[i].Split(new string[] { ", " }, StringSplitOptions.None);

                //stores first 4 elements as category,name,size,price
                string category = menuDivider[0];
                string name = menuDivider[1];
                string size = menuDivider[2];
                double price = double.Parse(menuDivider[3]);

                List<string> tempIngredients = new List<string>();
                List<InventoryItem> ingredients = new List<InventoryItem>();
                List<double> stockUsed = new List<double>();

                //adds ingredient name and stockused to their corresponding lists
                for (int j = 4; j < menuDivider.Length; j += 2)
                {
                    tempIngredients.Add(menuDivider[j]);
                    stockUsed.Add(double.Parse(menuDivider[j + 1]));
                }

                bool afterSauce = false;
                // loops through amount of ingredient in current menu item
                for (int k = 0; k < tempIngredients.Count; k++)
                {
                    for (int z = 0; z < inventoryData.Count; z++)
                    {
                        if (inventoryData[z].mName == tempIngredients[k])
                        {
                            // checks if sauce has already been found
                            if (afterSauce == true)
                            {
                                inventoryData[z].SetAsTopping();
                            }

                            ingredients.Add(inventoryData[z]);
                        }
                    }
                    if (tempIngredients[k] == "sauce")
                    {
                        afterSauce = true;
                    }
                }

                //stores the variables as an object and sends to menudata
                MenuItem item = new MenuItem(category, name, size, price, ingredients, stockUsed);
                menuData.Add(item);
            }
        }

        //method to update the quantity of an inventory item
        public static bool UpdateQuantity(string nameToFind, double newQuantity)
        {
            //search for the edited class
            for (int i = 0; i < inventoryData.Count; i++)
            {
                string name = inventoryData[i].mName;
                if (nameToFind == name)
                {
                    bool success = inventoryData[i].SetQuantity(newQuantity);
                    return success;
                }
            }
            //triggers if failed to find item
            return false;
        }

    }
}




