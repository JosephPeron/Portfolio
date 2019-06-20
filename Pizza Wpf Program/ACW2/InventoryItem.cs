using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACW2
{
    public class InventoryItem
    {
        public string mCategory;
        public string mName;
        public double mCostPerUnit;
        public double mQuantity;
        bool topping = false;

        //class constuctor
        public InventoryItem(string pCategory, string pName, double pCostPerUnit, double pQuantity)
        {
            mCategory = pCategory;
            mName = pName;
            mCostPerUnit = pCostPerUnit;
            mQuantity = pQuantity;
        }

        //method to be true is inventory name is topping
        public bool IsTopping()
        {
            return topping;
        }

        //method to return true if inventory name is topping
        public void SetAsTopping()
        {
            topping = true;
        }

        //method to print catetory, name, costperunit and quantity
        public override string ToString()
        {
            return (mCategory + ", " + mName + ", " + mCostPerUnit + ", " + mQuantity);
        }

        //method to update previous quantity value with new value
        public bool SetQuantity(double pQuantity)
        {
            mQuantity = pQuantity;
            return true;
        }

    }
 }


