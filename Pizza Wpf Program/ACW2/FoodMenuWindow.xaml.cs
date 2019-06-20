using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ACW2
{
    /// <summary>
    /// Interaction logic for FoodMenuWindow.xaml
    /// </summary>
    public partial class FoodMenuWindow : Window
    {
        public string lastSearchCat = "all";
        public string lastSearchSize = "all";

        //initialises window and sets combobox values
        public FoodMenuWindow()
        {
            InitializeComponent();

            categoryComboBox.Items.Add("All");
            categoryComboBox.Items.Add("Pizza");
            categoryComboBox.Items.Add("Burger");
            categoryComboBox.Items.Add("Sundry");
            categoryComboBox.SelectedValue = "All";


            sizeComboBox.Items.Add("All");
            sizeComboBox.Items.Add("Regular");
            sizeComboBox.Items.Add("Large");
            sizeComboBox.Items.Add("Extra-Large");
            sizeComboBox.SelectedValue = "All";

        }

        //event to check if category combo box is changed and relays the changed value to updatelistbox method
        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedcat = (string)categoryComboBox.SelectedItem;
            selectedcat = selectedcat.ToLower();

            if (selectedcat == "all")
            {
                UpdateListBox("all", lastSearchSize);
            }
            else if (selectedcat == "pizza")
            {
                UpdateListBox("pizza", lastSearchSize);
            }
            else if (selectedcat == "burger")
            {
                UpdateListBox("burger", lastSearchSize);
            }
            else if (selectedcat == "sundry")
            {
                UpdateListBox("sundry", lastSearchSize);
            }
        }

        //event to check if size combo box is changed and relays the changed value to updatelistbox method
        private void sizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSize = (string)sizeComboBox.SelectedItem;
            selectedSize = selectedSize.ToLower();

            if (selectedSize == "all")
            {
                UpdateListBox(lastSearchCat, "all");
            }
            else if (selectedSize == "regular")
            {
                UpdateListBox(lastSearchCat, "regular");
            }
            else if (selectedSize == "large")
            {
                UpdateListBox(lastSearchCat, "large");
            }
            else if (selectedSize == "extra-large")
            {
                UpdateListBox(lastSearchCat, "extra-large");
            }

        }

        //method to check through all menu items and if the category and size of the menuitem matchs the selected value from previous events, add to the listbox
        private void UpdateListBox(string lookingForCat, string lookingForSize)
        {
            lastSearchCat = lookingForCat;
            lastSearchSize = lookingForSize;
            listBoxInfo.Items.Clear();

            for (int i = 0; i < Utility.menuData.Count; i++)
            {
                if (lookingForCat == (string)Utility.menuData[i].mCategory)
                {
                    if (lookingForSize == (string)Utility.menuData[i].mSize)
                    {
                        MenuItem currentItem = Utility.menuData[i];
                        listBoxInfo.Items.Add(currentItem);
                    }
                    else if (lookingForSize == "all")
                    {
                        MenuItem currentItem = Utility.menuData[i];
                        listBoxInfo.Items.Add(currentItem);
                    }
                }
                else if (lookingForCat == "all")
                {
                    if (lookingForSize == "all")
                    {
                        MenuItem currentItem = Utility.menuData[i];
                        listBoxInfo.Items.Add(currentItem);
                    }
                    else if (lookingForSize == (string)Utility.menuData[i].mSize)
                    {
                        MenuItem currentItem = Utility.menuData[i];
                        listBoxInfo.Items.Add(currentItem);
                    }
                }
                //  attempt at changing colour 

                //    for (int j = 0; j < listBoxInfo.Items.Count; j++)
                //    {
                //        MenuItem currentListBox = Utility.menuData[j];
                //        listBoxInfo.SelectAll();

                //        if (currentListBox.mIngredients[j].mQuantity < 100 & currentListBox.mIngredients[j].mQuantity > 0)
                //        {
                //            listBoxInfo.SelectedItems.Background = Brushes.Yellow;
                //        }
                //        else if (currentListBox.mIngredients[i].mQuantity == 0)
                //        {
                //           listBoxInfo.SelectedItems.Background = Brushes.Red;
                //        }
                //     }
            }
        }

        //event to check if listboxinfo selection is changed and print ingredients from current selections menu to the ingredients listbox and print prices in the text box
        private void listBoxinfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxInfo.SelectedItem != null)
            {
                listBoxIngredients.Items.Clear();

                string listBoxText = listBoxInfo.SelectedItem.ToString();
                string[] listBoxElements = listBoxText.Split(new string[] { ", "}, StringSplitOptions.None);

                string currentSelectionName = listBoxElements[1];
                string currentSelectionSize = listBoxElements[2];
                for (int i = 0; i < Utility.menuData.Count; i++)
                {
                    MenuItem currentMenuItem = Utility.menuData[i];

                    if (currentMenuItem.mName == currentSelectionName && currentMenuItem.mSize == currentSelectionSize)
                    {
                        int ingredientCount = currentMenuItem.mIngredients.Count;
                        double ingredrientCost = new double();
                        double tempIngredientCost = new double();

                        for (int j = 0; j < ingredientCount; j++)
                        {
                            listBoxIngredients.Items.Add(currentMenuItem.mIngredients[j].mName + ", " + currentMenuItem.mStockUsed[j].ToString() + ", " + currentMenuItem.mIngredients[j].mCostPerUnit);
                            tempIngredientCost = (double)currentMenuItem.mStockUsed[j] * (double)currentMenuItem.mIngredients[j].mCostPerUnit;
                            ingredrientCost = ingredrientCost + tempIngredientCost;
                        }


                        textBoxItemPrice.Text = currentMenuItem.mPrice.ToString();
                        textBoxIngredientCost.Text = ingredrientCost.ToString();
                        double grossProfit = currentMenuItem.mPrice - ingredrientCost;
                        textBoxGrossProfit.Text = grossProfit.ToString();

                        }

                        
                    }

                }
                
            }
        }

       
}
        

    


            
            
            
    



