namespace location
{
    partial class Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.Sendbutton = new System.Windows.Forms.Button();
            this.debugCheckBox = new System.Windows.Forms.CheckBox();
            this.lookupTextBox = new System.Windows.Forms.TextBox();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.lookupLabel = new System.Windows.Forms.Label();
            this.locationLabel = new System.Windows.Forms.Label();
            this.timeoutLabel = new System.Windows.Forms.Label();
            this.whoisRadioButton = new System.Windows.Forms.RadioButton();
            this.h9RadioButton = new System.Windows.Forms.RadioButton();
            this.h0RadioButton = new System.Windows.Forms.RadioButton();
            this.h1RadioButton = new System.Windows.Forms.RadioButton();
            this.timeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.debugLabel = new System.Windows.Forms.Label();
            this.protocolLabel = new System.Windows.Forms.Label();
            this.serverAddressLabel = new System.Windows.Forms.Label();
            this.serverAddressTextBox = new System.Windows.Forms.TextBox();
            this.portNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.portNumberLabel = new System.Windows.Forms.Label();
            this.RepsonselistBox = new System.Windows.Forms.ListBox();
            this.responseLabel = new System.Windows.Forms.Label();
            this.locationCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // Sendbutton
            // 
            this.Sendbutton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Sendbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Sendbutton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Sendbutton.Location = new System.Drawing.Point(177, 186);
            this.Sendbutton.Name = "Sendbutton";
            this.Sendbutton.Size = new System.Drawing.Size(87, 23);
            this.Sendbutton.TabIndex = 0;
            this.Sendbutton.Text = "Send Data";
            this.Sendbutton.UseVisualStyleBackColor = false;
            this.Sendbutton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // debugCheckBox
            // 
            this.debugCheckBox.AutoSize = true;
            this.debugCheckBox.Location = new System.Drawing.Point(177, 30);
            this.debugCheckBox.Name = "debugCheckBox";
            this.debugCheckBox.Size = new System.Drawing.Size(63, 17);
            this.debugCheckBox.TabIndex = 1;
            this.debugCheckBox.Text = "Debug";
            this.debugCheckBox.UseVisualStyleBackColor = true;
            // 
            // lookupTextBox
            // 
            this.lookupTextBox.Location = new System.Drawing.Point(15, 28);
            this.lookupTextBox.Name = "lookupTextBox";
            this.lookupTextBox.Size = new System.Drawing.Size(116, 20);
            this.lookupTextBox.TabIndex = 2;
            // 
            // locationTextBox
            // 
            this.locationTextBox.Enabled = false;
            this.locationTextBox.Location = new System.Drawing.Point(14, 91);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(116, 20);
            this.locationTextBox.TabIndex = 3;
            // 
            // lookupLabel
            // 
            this.lookupLabel.AutoSize = true;
            this.lookupLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupLabel.Location = new System.Drawing.Point(14, 12);
            this.lookupLabel.Name = "lookupLabel";
            this.lookupLabel.Size = new System.Drawing.Size(49, 13);
            this.lookupLabel.TabIndex = 5;
            this.lookupLabel.Text = "Lookup";
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locationLabel.Location = new System.Drawing.Point(14, 75);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(56, 13);
            this.locationLabel.TabIndex = 6;
            this.locationLabel.Text = "Location";
            // 
            // timeoutLabel
            // 
            this.timeoutLabel.AutoSize = true;
            this.timeoutLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeoutLabel.Location = new System.Drawing.Point(174, 52);
            this.timeoutLabel.Name = "timeoutLabel";
            this.timeoutLabel.Size = new System.Drawing.Size(75, 13);
            this.timeoutLabel.TabIndex = 7;
            this.timeoutLabel.Text = "Timeout(ms)";
            // 
            // whoisRadioButton
            // 
            this.whoisRadioButton.AutoSize = true;
            this.whoisRadioButton.Checked = true;
            this.whoisRadioButton.Location = new System.Drawing.Point(177, 107);
            this.whoisRadioButton.Name = "whoisRadioButton";
            this.whoisRadioButton.Size = new System.Drawing.Size(65, 17);
            this.whoisRadioButton.TabIndex = 8;
            this.whoisRadioButton.TabStop = true;
            this.whoisRadioButton.Text = "Who Is";
            this.whoisRadioButton.UseVisualStyleBackColor = true;
            // 
            // h9RadioButton
            // 
            this.h9RadioButton.AutoSize = true;
            this.h9RadioButton.Location = new System.Drawing.Point(176, 125);
            this.h9RadioButton.Name = "h9RadioButton";
            this.h9RadioButton.Size = new System.Drawing.Size(80, 17);
            this.h9RadioButton.TabIndex = 9;
            this.h9RadioButton.Text = "HTTP 0.9";
            this.h9RadioButton.UseVisualStyleBackColor = true;
            // 
            // h0RadioButton
            // 
            this.h0RadioButton.AutoSize = true;
            this.h0RadioButton.Location = new System.Drawing.Point(176, 145);
            this.h0RadioButton.Name = "h0RadioButton";
            this.h0RadioButton.Size = new System.Drawing.Size(80, 17);
            this.h0RadioButton.TabIndex = 10;
            this.h0RadioButton.Text = "HTTP 1.0";
            this.h0RadioButton.UseVisualStyleBackColor = true;
            // 
            // h1RadioButton
            // 
            this.h1RadioButton.AutoSize = true;
            this.h1RadioButton.Location = new System.Drawing.Point(176, 162);
            this.h1RadioButton.Name = "h1RadioButton";
            this.h1RadioButton.Size = new System.Drawing.Size(80, 17);
            this.h1RadioButton.TabIndex = 11;
            this.h1RadioButton.Text = "HTTP 1.1";
            this.h1RadioButton.UseVisualStyleBackColor = true;
            // 
            // timeoutNumericUpDown
            // 
            this.timeoutNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Location = new System.Drawing.Point(176, 68);
            this.timeoutNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Name = "timeoutNumericUpDown";
            this.timeoutNumericUpDown.Size = new System.Drawing.Size(117, 20);
            this.timeoutNumericUpDown.TabIndex = 12;
            this.timeoutNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugLabel.Location = new System.Drawing.Point(173, 12);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(86, 13);
            this.debugLabel.TabIndex = 13;
            this.debugLabel.Text = "Debug Mode?";
            // 
            // protocolLabel
            // 
            this.protocolLabel.AutoSize = true;
            this.protocolLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.protocolLabel.Location = new System.Drawing.Point(174, 91);
            this.protocolLabel.Name = "protocolLabel";
            this.protocolLabel.Size = new System.Drawing.Size(54, 13);
            this.protocolLabel.TabIndex = 14;
            this.protocolLabel.Text = "Protocol";
            // 
            // serverAddressLabel
            // 
            this.serverAddressLabel.AutoSize = true;
            this.serverAddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverAddressLabel.Location = new System.Drawing.Point(14, 127);
            this.serverAddressLabel.Name = "serverAddressLabel";
            this.serverAddressLabel.Size = new System.Drawing.Size(93, 13);
            this.serverAddressLabel.TabIndex = 15;
            this.serverAddressLabel.Text = "Server Address";
            // 
            // serverAddressTextBox
            // 
            this.serverAddressTextBox.Location = new System.Drawing.Point(14, 142);
            this.serverAddressTextBox.Name = "serverAddressTextBox";
            this.serverAddressTextBox.Size = new System.Drawing.Size(116, 20);
            this.serverAddressTextBox.TabIndex = 16;
            this.serverAddressTextBox.Text = "localhost";
            // 
            // portNumericUpDown
            // 
            this.portNumericUpDown.Location = new System.Drawing.Point(15, 181);
            this.portNumericUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.portNumericUpDown.Name = "portNumericUpDown";
            this.portNumericUpDown.Size = new System.Drawing.Size(117, 20);
            this.portNumericUpDown.TabIndex = 17;
            this.portNumericUpDown.Value = new decimal(new int[] {
            43,
            0,
            0,
            0});
            // 
            // portNumberLabel
            // 
            this.portNumberLabel.AutoSize = true;
            this.portNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portNumberLabel.Location = new System.Drawing.Point(14, 166);
            this.portNumberLabel.Name = "portNumberLabel";
            this.portNumberLabel.Size = new System.Drawing.Size(77, 13);
            this.portNumberLabel.TabIndex = 18;
            this.portNumberLabel.Text = "Port Number";
            // 
            // RepsonselistBox
            // 
            this.RepsonselistBox.FormattingEnabled = true;
            this.RepsonselistBox.HorizontalScrollbar = true;
            this.RepsonselistBox.Location = new System.Drawing.Point(299, 28);
            this.RepsonselistBox.Name = "RepsonselistBox";
            this.RepsonselistBox.Size = new System.Drawing.Size(242, 173);
            this.RepsonselistBox.TabIndex = 23;
            // 
            // responseLabel
            // 
            this.responseLabel.AutoSize = true;
            this.responseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.responseLabel.Location = new System.Drawing.Point(296, 12);
            this.responseLabel.Name = "responseLabel";
            this.responseLabel.Size = new System.Drawing.Size(63, 13);
            this.responseLabel.TabIndex = 20;
            this.responseLabel.Text = "Response";
            // 
            // locationCheckBox
            // 
            this.locationCheckBox.AutoSize = true;
            this.locationCheckBox.Location = new System.Drawing.Point(15, 55);
            this.locationCheckBox.Name = "locationCheckBox";
            this.locationCheckBox.Size = new System.Drawing.Size(127, 17);
            this.locationCheckBox.TabIndex = 21;
            this.locationCheckBox.Text = "Update Location?";
            this.locationCheckBox.UseVisualStyleBackColor = true;
            this.locationCheckBox.CheckedChanged += new System.EventHandler(this.locationCheckBox_CheckedChanged);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(557, 221);
            this.Controls.Add(this.locationCheckBox);
            this.Controls.Add(this.responseLabel);
            this.Controls.Add(this.RepsonselistBox);
            this.Controls.Add(this.portNumberLabel);
            this.Controls.Add(this.portNumericUpDown);
            this.Controls.Add(this.serverAddressTextBox);
            this.Controls.Add(this.serverAddressLabel);
            this.Controls.Add(this.protocolLabel);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.timeoutNumericUpDown);
            this.Controls.Add(this.h1RadioButton);
            this.Controls.Add(this.h0RadioButton);
            this.Controls.Add(this.h9RadioButton);
            this.Controls.Add(this.whoisRadioButton);
            this.Controls.Add(this.timeoutLabel);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.lookupLabel);
            this.Controls.Add(this.locationTextBox);
            this.Controls.Add(this.lookupTextBox);
            this.Controls.Add(this.debugCheckBox);
            this.Controls.Add(this.Sendbutton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(500, 250);
            this.Name = "Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Window Interface";
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Sendbutton;
        private System.Windows.Forms.CheckBox debugCheckBox;
        private System.Windows.Forms.TextBox lookupTextBox;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.Label lookupLabel;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Label timeoutLabel;
        private System.Windows.Forms.RadioButton whoisRadioButton;
        private System.Windows.Forms.RadioButton h9RadioButton;
        private System.Windows.Forms.RadioButton h0RadioButton;
        private System.Windows.Forms.RadioButton h1RadioButton;
        private System.Windows.Forms.NumericUpDown timeoutNumericUpDown;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.Label protocolLabel;
        private System.Windows.Forms.Label serverAddressLabel;
        private System.Windows.Forms.TextBox serverAddressTextBox;
        private System.Windows.Forms.NumericUpDown portNumericUpDown;
        private System.Windows.Forms.Label portNumberLabel;
        private System.Windows.Forms.ListBox RepsonselistBox;
        private System.Windows.Forms.Label responseLabel;
        private System.Windows.Forms.CheckBox locationCheckBox;
    }
}

