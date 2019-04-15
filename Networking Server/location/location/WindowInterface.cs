using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace location
{
    public partial class Window : Form
    {
        Logic _logic;

        public Window(Logic myLogic)
        {
            InitializeComponent();
            _logic = myLogic;
        }

        //gets all the data form each item when the user presses button to send request
        public void sendButton_Click(object sender, EventArgs e)
        {
            bool serverAccepted = false;
            if (whoisRadioButton.Checked == true)
            {
                _logic.protocolRequest = "whois";
            }
            else if (h9RadioButton.Checked == true)
            {
                _logic.protocolRequest = "h9";
            }
            else if (h0RadioButton.Checked == true)
            {
                _logic.protocolRequest = "h0";
            }
            else if (h1RadioButton.Checked == true)
            {
                _logic.protocolRequest = "h1";
            }
                _logic.lookup = lookupTextBox.Text;
                _logic.locationSet = true;
            
            if (locationCheckBox.Checked == true)
            {
                    _logic.location = locationTextBox.Text;
                    _logic.locationSet = true;
            }
            else
            {
                _logic.locationSet = false;
            }

            if(debugCheckBox.Checked == true)
            {
                _logic.debugMode = true;
            }

            _logic.timeoutPeriod = (int)timeoutNumericUpDown.Value;

            _logic.portNumber = (int)portNumericUpDown.Value;

            if(serverAddressTextBox.Text != "") {
                _logic.serverAddress = serverAddressTextBox.Text;
                serverAccepted = true;
            }
            else
            {
                serverAccepted = false;
            }
            if (serverAccepted == true)
            {
                string response = _logic.clientInitialiser();
                printToScreen(response);
            }
            else
            {
                    MessageBox.Show("Please input a server address!");
            }

        }

        //prints repsonse in the listbox 
        public void printToScreen(string message)
        {
            RepsonselistBox.Items.Add(message);
        }

        //check to see if the user wants to request get location or update location of a lookup
        private void locationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(locationCheckBox.Checked == true)
            {
                locationTextBox.Enabled = true;
            }
            else
            {
                locationTextBox.Enabled = false;
            }
        }
    }
}
