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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ACW2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Utility.InventoryReader();
            Utility.MenuReader();
        }

        private void inventoryButton_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow wnd = new InventoryWindow();
            wnd.ShowDialog();
        }

        private void foodMenuButton_Click(object sender, RoutedEventArgs e)
        {
            FoodMenuWindow wnd = new FoodMenuWindow();
            wnd.ShowDialog();
        }

        private void newOrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow wnd = new OrderWindow();
            wnd.ShowDialog();
        }

        private void completedOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            CompletedOrdersWindow wnd = new CompletedOrdersWindow();
            wnd.ShowDialog();
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateInventoryList();
            this.Close();
        }
      
        private void UpdateInventoryList()
        {

            StreamWriter InventoryWriter = new StreamWriter("inventory.txt");
            for (int i = 0; i < Utility.inventoryData.Count; i++)
            {
                InventoryWriter.WriteLine(Utility.inventoryData[i].mCategory + ", " + Utility.inventoryData[i].mName + ", " + Utility.inventoryData[i].mCostPerUnit + ", " + Utility.inventoryData[i].mQuantity);
            }

            InventoryWriter.Flush();
            InventoryWriter.Close();
        }
    }
}
