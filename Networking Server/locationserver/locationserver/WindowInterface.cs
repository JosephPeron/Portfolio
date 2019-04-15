using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace locationserver
{
    public partial class WindowInterface : Form
    {
        Server _server;
        Data myData = new Data();

        Dictionary<string, string> d = Data.dictionary;

        public WindowInterface(Server myServer)
        {
            InitializeComponent();
            _server = myServer;
            updateDictionaryListbox();
        }

        //updates the listbox to the most recent dictionary
        public void updateDictionaryListbox()
        {
            Dictionary<string, string> d = Data.dictionary;
            foreach(KeyValuePair<string, string> kv in d)
            {
                dictionaryListBox.Items.Add(kv.Key + " " + kv.Value);
            }
        }

        //adds the values in the lookup and location to manual add to the dictionary and refreshs the dictionary listbox
        private void dictionaryButton_Click(object sender, EventArgs e)
        {
            string lookup = lookupTextBox.Text;
            string location = locationTextBox.Text;
            if (d.ContainsKey(lookup))
            {
                d[lookup] = location;
            }
            else
            {
                d.Add(lookup, location);
            }
            dictionaryListBox.Items.Clear();
            updateDictionaryListbox();
        }

        //removes a key from the dictionary and refreshs the dictionary listbox 
        private void Removebutton_Click(object sender, EventArgs e)
        {
            d.Remove(lookupTextBox.Text);
            dictionaryListBox.Items.Clear();
            updateDictionaryListbox();
            lookupTextBox.Clear();
            locationTextBox.Clear();
        }
        
        //allows the listener to accept requests
        public void startServerButton_Click(object sender, EventArgs e)
        {
            _server.startServer();
            statusLabel.Text = "ONLINE";
            statusLabel.ForeColor = Color.Green;
        }

        //stops the listener from accepting requests
        private void stopServerButton_Click(object sender, EventArgs e)
        {
            _server.stopServer();
            statusLabel.Text = "OFFLINE";
            statusLabel.ForeColor = Color.Red;
        }

        //input data into the lookup and location text boxes when an item is selected in the listbox
        private void dictionaryListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string valuekey = dictionaryListBox.SelectedItem.ToString();
            string[] split = valuekey.Split(new string[] { " " }, StringSplitOptions.None);
            string lookup = split[0];
            string location = split[1];
            for(int i = 2; i < split.Length; i++)
            {
                location += split[i];
            }
            lookupTextBox.Text = lookup;
            locationTextBox.Text = location;
        }

        //refreshes the dictionary listbox to the most update to data dictionary
        private void refreshButton_Click(object sender, EventArgs e)
        {
            dictionaryListBox.Items.Clear();
            updateDictionaryListbox();
        }

        //sets the debug mode to true or false if check box is checked or not 
        private void debugCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(debugCheckbox.Checked)
            {
                _server.debugMode = true;
            }
            else
            {
                _server.debugMode = false;
            }
        }

        //get the int value for hte numericupdown and sets it to the timeout value
        private void timeoutButton_Click(object sender, EventArgs e)
        {
            int newTimeout = (int)timeoutNumericUpDown.Value;
            _server.changeTimeout(newTimeout);
        }
    }
}
