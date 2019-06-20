using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACW2
{
    public class MenuItem
    {
        public string mCategory;
        public string mName;
        public string mSize;
        public double mPrice;
        public List<InventoryItem> mIngredients;
        public List<double> mStockUsed;

        //class constuctor 
        public MenuItem(string pCategory, string pName, string pSize, double pPrice, List<InventoryItem> pIngredients, List<double> pStockUsed)
        {
            mCategory = pCategory;
            mName = pName;
            mSize = pSize;
            mPrice = pPrice;
            mIngredients = pIngredients;
            mStockUsed = pStockUsed;
        }

        //method to print category, name, size and price
        public override string ToString()
        {
            return (mCategory + ", " + mName + ", " + mSize + ", " + mPrice);
        }

    }
}
