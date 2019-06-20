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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public double burgerPreviousPrice = 0;
        public double previousTotalPrice = 0;
        public double pizzaPreviousPrice = 0;
        
        //initialises window and populates the pizza name combo box, burger name combo box and sundry listbox if items are instock
        public OrderWindow()
        {
            Utility.InventoryReader();
            Utility.MenuReader();
            InitializeComponent();

            string previousPizzaName = " ";
            string previousBurgerName = " ";
            string previousSundryName = " ";

            for (int i = 0; i < Utility.menuData.Count; i++)
            {
                MenuItem currentMenuItem = Utility.menuData[i];

                if (previousPizzaName != currentMenuItem.mName && currentMenuItem.mCategory == "pizza")
                {
                    pizzaNameComboBox.Items.Add(currentMenuItem.mName);

                    previousPizzaName = currentMenuItem.mName;
                }
                // if menu.category == pizza && quantity != 0
                else if (previousBurgerName != currentMenuItem.mName && currentMenuItem.mCategory == "burger")
                {
                    burgerNameComboBox.Items.Add(currentMenuItem.mName);
                    previousBurgerName = currentMenuItem.mName;
                }

                else if (previousSundryName != currentMenuItem.mName && currentMenuItem.mCategory == "sundry")
                {
                    int tempEnoughIngredients = 0;
                    int enoughIngredients = 0;
                    for (int j = 0; j < currentMenuItem.mIngredients.Count; j++)
                    {
                        if (currentMenuItem.mIngredients[j].mQuantity - currentMenuItem.mStockUsed[j] > 0)
                        {
                            tempEnoughIngredients = tempEnoughIngredients + 1;
                        }
                    }
                    enoughIngredients = tempEnoughIngredients;
                    tempEnoughIngredients = 0;
                    if (enoughIngredients == currentMenuItem.mIngredients.Count)
                    {
                        sundryListBox.Items.Add(currentMenuItem.mName);
                        previousSundryName = currentMenuItem.mName;

                    }
                }
            }
        }

        //event to check if pizza name combo box is changed and sends the selected name to UpdatePizzaSize method
        private void pizzaNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedPizzaName = (string)pizzaNameComboBox.SelectedItem;
            selectedPizzaName = selectedPizzaName.ToLower();

            UpdatePizzaComboBoxSize(selectedPizzaName);
            pizzaToppingsListBox.Items.Clear();
            pizzaPreviousPrice = 0;
            pizzaPriceUpdate(pizzaPreviousPrice);
        }

        //method to update the pizza size combo box with size if the size requirements are in stock
        private void UpdatePizzaComboBoxSize(string pizzaName)
        {
            int enoughIngredients = 0;
            int tempEnoughIngredients = 0;
            pizzaSizeComboBox.Items.Clear();

            for (int i = 0; i < Utility.menuData.Count; i++)
            {
                MenuItem currentMenuItem = Utility.menuData[i];

                if (pizzaName == currentMenuItem.mName)
                {
                    for (int j = 0; j < currentMenuItem.mIngredients.Count; j++)
                    {
                        if (currentMenuItem.mIngredients[j].mQuantity - currentMenuItem.mStockUsed[j] > 0)
                        {
                            tempEnoughIngredients = tempEnoughIngredients + 1;
                        }
                    }
                    enoughIngredients = tempEnoughIngredients;
                    tempEnoughIngredients = 0;
                    if (enoughIngredients == currentMenuItem.mIngredients.Count)
                    {
                        pizzaSizeComboBox.Items.Add(currentMenuItem.mSize);
                    }
                }
            }
        }

        //event to check if pizza size combo box is changed and adds relevant price to pizza price total
        private void pizzaSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                PopulateExtraToppings();

                pizzaPreviousPrice = 0;
                pizzaPriceUpdate(pizzaPreviousPrice);
                if (pizzaNameComboBox.SelectedItem != null && pizzaSizeComboBox.SelectedItem != null)
                {
                    for (int j = 0; j < Utility.menuData.Count; j++)
                    {
                        MenuItem currentItem = Utility.menuData[j];
                        if (currentItem.mName == pizzaNameComboBox.SelectedItem.ToString())
                        {
                            if (currentItem.mSize == pizzaSizeComboBox.SelectedItem.ToString())
                            {
                                pizzaPriceUpdate(currentItem.mPrice);
                            }
                        }
                    }
                }
            }         
        }

        //method to populate the toppings listbox if the selected pizza ingredient and topping costperunit > 0 
        private void PopulateExtraToppings()
        {
            pizzaToppingsListBox.Items.Clear();
            if (pizzaSizeComboBox.SelectedItem != null)
            {
                string sizeOption = pizzaSizeComboBox.SelectedItem.ToString();
                double toppingUnitAddition = 0;

                if (sizeOption == "regular")
                {
                    toppingUnitAddition = toppingUnitAddition + 0.25;
                }
                if (sizeOption == "large")
                {
                    toppingUnitAddition = toppingUnitAddition + 0.35;
                }
                if (sizeOption == "extra-large")
                {
                    toppingUnitAddition = toppingUnitAddition + 0.75;
                }
                if (pizzaSizeComboBox.SelectedItem != null)
                {
                    for (int q = 0; q < Utility.menuData.Count; q++)
                    {
                        MenuItem currentItem = Utility.menuData[q];

                        if (currentItem.mName == pizzaNameComboBox.Text && currentItem.mSize == sizeOption)
                        {
                            for (int i = 0; i < Utility.inventoryData.Count; i++)
                            {
                                if (Utility.inventoryData[i].IsTopping() == true)
                                {
                                    bool inRecipe = false;
                                    double recipeCost = 0;
                                    for (int w = 0; w < currentItem.mIngredients.Count; w++)
                                    {
                                        if (Utility.inventoryData[i].mName == currentItem.mIngredients[w].ToString())
                                        {
                                            inRecipe = true;
                                        }
                                    }
                                    if (inRecipe == true)
                                    {
                                        recipeCost = currentItem.mStockUsed[i];
                                    }
                                    else
                                    {
                                        recipeCost = 0;
                                    }
                                    if (Utility.inventoryData[i].mQuantity - recipeCost - toppingUnitAddition > 0)
                                    {
                                        pizzaToppingsListBox.Items.Add(Utility.inventoryData[i].mName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //event to check if sundry add to order is pressed and adds the selected item to order summary listbox along with its price to total price textbox
        private void SundryAddButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedSundry = sundryListBox.SelectedItem.ToString();

            orderSummaryListBox.Items.Add(selectedSundry + " £" + sundryPriceTextBox.Text);
            double orderPrice = double.Parse(sundryPriceTextBox.Text);
            UpdateTotalPrice(orderPrice);
        }

        //event to check if sundry listbox selectiion is chagned and adds the price for the item to the sundrypricetext box
        private void sundryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSundryName = sundryListBox.SelectedItem.ToString();

            for (int i = 0; i < Utility.menuData.Count; i++)
            {
                MenuItem currentMenuItem = Utility.menuData[i];

                if (selectedSundryName == currentMenuItem.mName)
                {
                    sundryPriceTextBox.Text = currentMenuItem.mPrice.ToString();
                }
            }
        }

        //event to check if the burgeraddtoorder button is pressed and adds the item details to order sumary and price to totalprice text box
        private void burgerAddButton_Click(object sender, RoutedEventArgs e)
        {

            if (burgerNameComboBox != null)
            {
                string chosenBurger = burgerNameComboBox.SelectedItem.ToString();
                string burgerOrder = "";
                burgerOrder = burgerOrder + chosenBurger;
                if (quaterpoundRadioButton.IsChecked == true || halfpoundRadioButton.IsChecked == true)
                {
                    if (quaterpoundRadioButton.IsChecked == true)
                    {
                        burgerOrder = burgerOrder + " " + "1/4lb";
                    }
                    if (halfpoundRadioButton.IsChecked == true)
                    {
                        burgerOrder = burgerOrder + " " + "1/2lb";
                    }
                    if (saladCheckBox.IsChecked == true)
                    {
                        burgerOrder = burgerOrder + " " + "salad";
                    }
                    if (cheeseCheckBox.IsChecked == true)
                    {
                        burgerOrder = burgerOrder + " " + "cheese";
                    }
                    burgerOrder = burgerOrder + " £" + burgerPriceTextBox.Text;
                    orderSummaryListBox.Items.Add(burgerOrder);
                    double orderPrice = double.Parse(burgerPriceTextBox.Text);
                    UpdateTotalPrice(orderPrice);
                }
            }
        }

        //method to update the burgerprice  with the newly selected item
        public void burgerPriceUpdate(double priceUpdate)
        {
            double burgerAddition = priceUpdate;
            double NewPrice = priceUpdate + burgerPreviousPrice;
            burgerPriceTextBox.Text = NewPrice.ToString();
            burgerPreviousPrice = NewPrice;
        }

        //method to update the pizzaprive with the newly selected item
        public void pizzaPriceUpdate(double priceUpdate)
        {
            double pizzaAddition = priceUpdate;
            double NewPrice = priceUpdate + pizzaPreviousPrice;
            pizzaPriceTextBox.Text = NewPrice.ToString();
            pizzaPreviousPrice = NewPrice;
        }

        //checks if quaterpound is selected
        private void quaterpoundRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (burgerNameComboBox.SelectedItem != null)
            {
                double chosenBurgerPrice = 0;
                string chosenBurgerSize = "regular";
                string chosenBurger = burgerNameComboBox.SelectedItem.ToString();

                for (int i = 0; i < Utility.menuData.Count; i++)
                {
                    MenuItem currentItem = Utility.menuData[i];

                    if (currentItem.mName == chosenBurger)
                    {
                        if (currentItem.mSize.ToString() == chosenBurgerSize)
                        {
                            chosenBurgerPrice = chosenBurgerPrice + currentItem.mPrice;
                            burgerPriceUpdate(chosenBurgerPrice);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a burger name first!");
                quaterpoundRadioButton.IsChecked = false;
            }
        }

        //checks if half pound is selected
        private void halfpoundRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (burgerNameComboBox.SelectedItem != null)
            {
                double chosenBurgerPrice = 0;
                string chosenBurgerSize = "large";
                string chosenBurger = burgerNameComboBox.SelectedItem.ToString();

                for (int i = 0; i < Utility.menuData.Count; i++)
                {
                    MenuItem currentItem = Utility.menuData[i];

                    if (currentItem.mName == chosenBurger)
                    {
                        if (currentItem.mSize.ToString() == chosenBurgerSize)
                        {
                            chosenBurgerPrice = chosenBurgerPrice + currentItem.mPrice;
                            burgerPriceUpdate(chosenBurgerPrice);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a burger name first!");

                halfpoundRadioButton.IsChecked = false;
            }
        }

        //checks if salad is selected
        private void saladCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Utility.inventoryData.Count; i++)
            {
                if (Utility.inventoryData[i].mName == "salad")
                {
                    if (Utility.inventoryData[i].mQuantity - 0.5 > 0)
                    {
                        double saladPrice = 0.5 * Utility.inventoryData[i].mCostPerUnit;
                        burgerPriceUpdate(saladPrice);
                    }
                }
            }
        }

        //checks if cheese is selected
        private void cheeseCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Utility.inventoryData.Count; i++)
            {
                if (Utility.inventoryData[i].mName == "cheddar")
                {
                    if (Utility.inventoryData[i].mQuantity - 0.2 > 0)
                    {
                        double cheesePrice = 0.2 * Utility.inventoryData[i].mCostPerUnit;
                        burgerPriceUpdate(cheesePrice);
                    }
                }
            }
        }

        //checks if salad is deselected
        private void saladCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Utility.inventoryData.Count; i++)
            {
                if (Utility.inventoryData[i].mName == "salad")
                {
                    double cheesePrice = -0.5 * Utility.inventoryData[i].mCostPerUnit;
                    burgerPriceUpdate(cheesePrice);
                }
            }
        }

        //checks if cheese is deselected
        private void cheeseCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Utility.inventoryData.Count; i++)
            {
                if (Utility.inventoryData[i].mName == "cheddar")
                {
                    double cheesePrice = -0.2 * Utility.inventoryData[i].mCostPerUnit;
                    burgerPriceUpdate(cheesePrice);
                }
            }
        }

        //checks if quaterpound is deselected
        private void quaterpoundRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (burgerNameComboBox.SelectedItem != null)
            {
                double chosenBurgerPrice = 0;
                string chosenBurgerSize = "regular";
                string chosenBurger = burgerNameComboBox.SelectedItem.ToString();

                for (int i = 0; i < Utility.menuData.Count; i++)
                {
                    MenuItem currentItem = Utility.menuData[i];

                    if (currentItem.mName == chosenBurger)
                    {
                        if (currentItem.mSize.ToString() == chosenBurgerSize)
                        {
                            chosenBurgerPrice = chosenBurgerPrice + -currentItem.mPrice;
                            burgerPriceUpdate(chosenBurgerPrice);
                        }
                    }
                }
            }
        }

        //checks if halfpound is deselected
        private void halfpoundRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (burgerNameComboBox.SelectedItem != null)
            {
                double chosenBurgerPrice = 0;
                string chosenBurgerSize = "large";
                string chosenBurger = burgerNameComboBox.SelectedItem.ToString();

                for (int i = 0; i < Utility.menuData.Count; i++)
                {
                    MenuItem currentItem = Utility.menuData[i];

                    if (currentItem.mName == chosenBurger)
                    {
                        if (currentItem.mSize.ToString() == chosenBurgerSize)
                        {
                            chosenBurgerPrice = chosenBurgerPrice + -currentItem.mPrice;
                            burgerPriceUpdate(chosenBurgerPrice);
                        }
                    }
                }
            }
        }

        //method to update the total price of order if item is added to ordersummary
        public void UpdateTotalPrice(double priceToAdd)
        {
            double Addition = priceToAdd;
            double TotalPrice = previousTotalPrice + Addition;
            TotalPriceTextBox.Text = TotalPrice.ToString();
            previousTotalPrice = TotalPrice;


        }

        //checks if remove button is pressed and takes away selected item price from total
        private void RemoveItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (orderSummaryListBox.SelectedItem != null)
            {
                string orderPriceRemoval = orderSummaryListBox.SelectedItem.ToString();
                string[] RemovalElements = orderPriceRemoval.Split(new string[] { "£" }, StringSplitOptions.None);
                {
                    double removalCost = double.Parse(RemovalElements[1]);
                    double CurrentTotalPrice = double.Parse(TotalPriceTextBox.Text);
                    double NewValue = CurrentTotalPrice - removalCost;

                    TotalPriceTextBox.Text = NewValue.ToString();
                }
                orderSummaryListBox.Items.Remove(orderSummaryListBox.SelectedItem);
            }
        }

        //prevents pizza size being selected first
        private void pizzaSizeComboBox_DropDownOpened(object sender, EventArgs e)
        {
            if(pizzaNameComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a pizza name first!");
            }
        }

        //check if topping is selected
        private void pizzaToppingsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pizzaToppingsListBox.SelectedItems.Count <= 5)
                {
                if (pizzaToppingsListBox.SelectedItem != null)
                {
                    string selectedTopping = pizzaToppingsListBox.SelectedItem.ToString();
                    if (pizzaToppingsListBox.SelectedIndex == 1)
                    {
                        for (int i = 0; i < Utility.inventoryData.Count; i++)
                        {
                            if (selectedTopping == Utility.inventoryData[i].mName)
                            {
                                if (pizzaSizeComboBox.SelectedItem.ToString() == "regular")
                                {
                                    double toppingPrice = Utility.inventoryData[i].mCostPerUnit * 0.25;
                                    pizzaPriceUpdate(toppingPrice);
                                }
                                if (pizzaSizeComboBox.SelectedItem.ToString() == "large")
                                {
                                    double toppingPrice = Utility.inventoryData[i].mCostPerUnit * 0.35;
                                    pizzaPriceUpdate(toppingPrice);
                                }
                                if (pizzaSizeComboBox.SelectedItem.ToString() == "extra-large")
                                {
                                    double toppingPrice = Utility.inventoryData[i].mCostPerUnit * 0.75;
                                    pizzaPriceUpdate(toppingPrice);
                                }
                            }
                        }
                    }
                }
            }
        }

        //checks if pizza addto order is pressed and adds to order summary and price to totalprice
        private void pizzaAddButton_Click(object sender, RoutedEventArgs e)
        {
            string pizzaOrder = "";
            string chosenpizza = pizzaNameComboBox.SelectedItem.ToString();
            string chosensize = pizzaSizeComboBox.SelectedItem.ToString();
            pizzaOrder = pizzaOrder + chosenpizza + " " + chosensize;
            if (stuffedCrustCheckBox.IsChecked == true)
            {
                pizzaOrder = pizzaOrder + " " + "stuffedcrust";
            }
            pizzaOrder = pizzaOrder + " £" + pizzaPriceTextBox.Text;
            double pizzaOrderPrice = double.Parse(pizzaPriceTextBox.Text);
            orderSummaryListBox.Items.Add(pizzaOrder);
            UpdateTotalPrice(pizzaOrderPrice);

        }
    }
}

    

