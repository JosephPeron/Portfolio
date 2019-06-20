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
using System.IO;

namespace ACW2
{
    /// <summary>
    /// Interaction logic for InventoryWindow.xaml
    /// </summary>
    ///

    public partial class InventoryWindow : Window
    {
        string lastSearch = "all";
        
        //initialises window and sets combobox values
        public InventoryWindow()
        {
            InitializeComponent();
            
            categoryComboBox.Items.Add("All");
            categoryComboBox.Items.Add("Pizza");
            categoryComboBox.Items.Add("Burger");
            categoryComboBox.Items.Add("Sundry");

            categoryComboBox.SelectedValue = "All";

        }

        //event to check when category combo box is changed and what value its change to and relays the value to updatelistbox mehtod
        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = (string)categoryComboBox.SelectedItem;
            selected = selected.ToLower();

            if (selected == "all")
            {
                UpdateListBox("all");
            }
            if (selected == "pizza")
            {
                UpdateListBox("pizza");
            }
            if (selected == "burger")
            {
                UpdateListBox("burger");
            }
            if (selected == "sundry")
            {
                UpdateListBox("sundry");
            }
        }

        //method  to update listbox depending on the category value it is given by the categoryselectionhanged event
        private void UpdateListBox(string lookingFor)
        {
            lastSearch = lookingFor;
            listItems.Items.Clear();

            for (int i = 0; i < Utility.inventoryData.Count; i++)
            {
                if ((string)Utility.inventoryData[i].mCategory == lookingFor)
                {
                    ListBoxItem listboxitemInventory = new ListBoxItem();

                    listboxitemInventory.Content = (Utility.inventoryData[i].ToString());

                    if (Utility.inventoryData[i].mQuantity == 0)
                    {
                        listboxitemInventory.Background = Brushes.Red;
                    }
                    else if (Utility.inventoryData[i].mQuantity > 0 && Utility.inventoryData[i].mQuantity <= 100)
                    {
                        listboxitemInventory.Background = Brushes.Yellow;
                    }
                    else
                    {
                        listboxitemInventory.Background = Brushes.White;
                    }

                    listItems.Items.Add(listboxitemInventory);
                }
                else if (lookingFor == "all")
                {

                    ListBoxItem listboxitemInventory = new ListBoxItem();

                    listboxitemInventory.Content = (Utility.inventoryData[i].ToString());

                    if (Utility.inventoryData[i].mQuantity == 0)
                    {
                        listboxitemInventory.Background = Brushes.Red;
                    }
                    else if (Utility.inventoryData[i].mQuantity > 0 && Utility.inventoryData[i].mQuantity <= 100)
                    {
                        listboxitemInventory.Background = Brushes.Yellow;
                    }
                    else
                    {
                        listboxitemInventory.Background = Brushes.White;
                    }

                    listItems.Items.Add(listboxitemInventory);
                }



            }
        }

        //event to check which listbox item is selected and draws quantity value and inputs it in the quantity in stock textbox
        public void listItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listItems.SelectedItem != null)
            {
                string ListBoxText = listItems.SelectedItem.ToString();
                string[] textElements = ListBoxText.Split(new string[] { ", "}, StringSplitOptions.None);
                string ListQuantity = textElements[3];
                textBox.Text = ListQuantity;
            }

        }

        //event to draw value out of quantity in stock textbox and name of selected listbox value and send them to another method to change quantity values
        private void button_Click(object sender, RoutedEventArgs e)
        {
            double changedValue = double.Parse(textBox.Text);

            bool success = false;

            if (changedValue >= 0)

            {
                string ListBoxText = listItems.SelectedItem.ToString();
                string[] textElements = ListBoxText.Split(new string[] { ", "}, StringSplitOptions.None);

                string name = textElements[1];

                success = Utility.UpdateQuantity(name, changedValue);
            }

            if (success == true)
            {
                UpdateListBox(lastSearch);
            }
            else
            {
                MessageBox.Show("Quantity update failed, item not found");
            }

        }

        //methid to update the inventory.txt with listbox values
        private void UpdateInventoryFile()
        {

            StreamWriter InventoryWriter = new StreamWriter("inventory.txt");
            for (int i = 0; i < Utility.inventoryData.Count; i++)
            {
                InventoryWriter.WriteLine(Utility.inventoryData[i].mCategory + ", " + Utility.inventoryData[i].mName + ", " + Utility.inventoryData[i].mCostPerUnit + ", " + Utility.inventoryData[i].mQuantity);
            }

            InventoryWriter.Flush();
            InventoryWriter.Close();
        }

        //event to call update inventory.txt file
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UpdateInventoryFile();
        }
    }
}

    


