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

namespace WpfBankApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Bank wpfBank;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            {
                string name = null;
                decimal balance = 0;
                try
                {
                    //Get the text from the accountName text box
                    name = accountname.Text;
                    //Get the text from the accountBalance text box and convert it
                    //to a double.
                    balance = decimal.Parse(accountbalance.Text);
                    //if an instance of the Bank class has not been constructed, do
                    //that here
                    if (wpfBank == null)
                    {
                        //Create the bank
                        wpfBank = new Bank();
                    }
                    //Create an instance of the account with the data from the text
                    //boxes
                    Account newAccount = new Account(name, balance);
                    //Add the account to the Bank object
                    wpfBank.addAccount(newAccount);
                    //Show the name of the account and the balance
                    MessageBox.Show(name + "'s account has been created with a balance  of" + balance.ToString());

                }
                //If something went wrong...
                catch (Exception exception)
                {
                    //Show the user what exactly the error message was
                    MessageBox.Show("Account not created. Here is the problem : " +
                   exception.ToString());
                }

            }

        }
    }
}
