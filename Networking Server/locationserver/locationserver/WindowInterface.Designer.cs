namespace locationserver
{
    partial class WindowInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowInterface));
            this.startServerButton = new System.Windows.Forms.Button();
            this.stopServerButton = new System.Windows.Forms.Button();
            this.dictionaryButton = new System.Windows.Forms.Button();
            this.lookupTextBox = new System.Windows.Forms.TextBox();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.lookupLabel = new System.Windows.Forms.Label();
            this.locationLabel = new System.Windows.Forms.Label();
            this.Removebutton = new System.Windows.Forms.Button();
            this.timeoutNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.timeoutLlabel = new System.Windows.Forms.Label();
            this.dictionaryListBox = new System.Windows.Forms.ListBox();
            this.dictionaryLabel = new System.Windows.Forms.Label();
            this.debugCheckbox = new System.Windows.Forms.CheckBox();
            this.serverStatusLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.timeoutButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // startServerButton
            // 
            this.startServerButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.startServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startServerButton.Location = new System.Drawing.Point(12, 18);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(75, 40);
            this.startServerButton.TabIndex = 0;
            this.startServerButton.Text = "START SERVER";
            this.startServerButton.UseVisualStyleBackColor = false;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
            // 
            // stopServerButton
            // 
            this.stopServerButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.stopServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopServerButton.Location = new System.Drawing.Point(99, 18);
            this.stopServerButton.Name = "stopServerButton";
            this.stopServerButton.Size = new System.Drawing.Size(75, 40);
            this.stopServerButton.TabIndex = 1;
            this.stopServerButton.Text = "STOP SERVER";
            this.stopServerButton.UseVisualStyleBackColor = false;
            this.stopServerButton.Click += new System.EventHandler(this.stopServerButton_Click);
            // 
            // dictionaryButton
            // 
            this.dictionaryButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.dictionaryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dictionaryButton.Location = new System.Drawing.Point(160, 64);
            this.dictionaryButton.Name = "dictionaryButton";
            this.dictionaryButton.Size = new System.Drawing.Size(93, 40);
            this.dictionaryButton.TabIndex = 2;
            this.dictionaryButton.Text = "Add To/Edit Dictionary";
            this.dictionaryButton.UseVisualStyleBackColor = false;
            this.dictionaryButton.Click += new System.EventHandler(this.dictionaryButton_Click);
            // 
            // lookupTextBox
            // 
            this.lookupTextBox.Location = new System.Drawing.Point(7, 84);
            this.lookupTextBox.Name = "lookupTextBox";
            this.lookupTextBox.Size = new System.Drawing.Size(70, 20);
            this.lookupTextBox.TabIndex = 3;
            // 
            // locationTextBox
            // 
            this.locationTextBox.Location = new System.Drawing.Point(83, 84);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(71, 20);
            this.locationTextBox.TabIndex = 4;
            // 
            // lookupLabel
            // 
            this.lookupLabel.AutoSize = true;
            this.lookupLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookupLabel.Location = new System.Drawing.Point(12, 68);
            this.lookupLabel.Name = "lookupLabel";
            this.lookupLabel.Size = new System.Drawing.Size(49, 13);
            this.lookupLabel.TabIndex = 5;
            this.lookupLabel.Text = "Lookup";
            // 
            // locationLabel
            // 
            this.locationLabel.AutoSize = true;
            this.locationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locationLabel.Location = new System.Drawing.Point(83, 68);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(56, 13);
            this.locationLabel.TabIndex = 6;
            this.locationLabel.Text = "Location";
            // 
            // Removebutton
            // 
            this.Removebutton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Removebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Removebutton.Location = new System.Drawing.Point(259, 64);
            this.Removebutton.Name = "Removebutton";
            this.Removebutton.Size = new System.Drawing.Size(93, 40);
            this.Removebutton.TabIndex = 7;
            this.Removebutton.Text = "Remove From Dictionary";
            this.Removebutton.UseVisualStyleBackColor = false;
            this.Removebutton.Click += new System.EventHandler(this.Removebutton_Click);
            // 
            // timeoutNumericUpDown
            // 
            this.timeoutNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Location = new System.Drawing.Point(7, 131);
            this.timeoutNumericUpDown.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.timeoutNumericUpDown.Name = "timeoutNumericUpDown";
            this.timeoutNumericUpDown.Size = new System.Drawing.Size(78, 20);
            this.timeoutNumericUpDown.TabIndex = 8;
            this.timeoutNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // timeoutLlabel
            // 
            this.timeoutLlabel.AutoSize = true;
            this.timeoutLlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeoutLlabel.Location = new System.Drawing.Point(0, 115);
            this.timeoutLlabel.Name = "timeoutLlabel";
            this.timeoutLlabel.Size = new System.Drawing.Size(115, 13);
            this.timeoutLlabel.TabIndex = 9;
            this.timeoutLlabel.Text = "Timeout Period(ms)";
            // 
            // dictionaryListBox
            // 
            this.dictionaryListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dictionaryListBox.FormattingEnabled = true;
            this.dictionaryListBox.HorizontalScrollbar = true;
            this.dictionaryListBox.Location = new System.Drawing.Point(169, 128);
            this.dictionaryListBox.Name = "dictionaryListBox";
            this.dictionaryListBox.Size = new System.Drawing.Size(282, 69);
            this.dictionaryListBox.TabIndex = 10;
            this.dictionaryListBox.SelectedValueChanged += new System.EventHandler(this.dictionaryListBox_SelectedValueChanged);
            // 
            // dictionaryLabel
            // 
            this.dictionaryLabel.AutoSize = true;
            this.dictionaryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dictionaryLabel.Location = new System.Drawing.Point(170, 110);
            this.dictionaryLabel.Name = "dictionaryLabel";
            this.dictionaryLabel.Size = new System.Drawing.Size(64, 13);
            this.dictionaryLabel.TabIndex = 11;
            this.dictionaryLabel.Text = "Dictionary";
            // 
            // debugCheckbox
            // 
            this.debugCheckbox.AutoSize = true;
            this.debugCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugCheckbox.Location = new System.Drawing.Point(7, 157);
            this.debugCheckbox.Name = "debugCheckbox";
            this.debugCheckbox.Size = new System.Drawing.Size(70, 17);
            this.debugCheckbox.TabIndex = 12;
            this.debugCheckbox.Text = "Debug?";
            this.debugCheckbox.UseVisualStyleBackColor = true;
            this.debugCheckbox.CheckedChanged += new System.EventHandler(this.debugCheckbox_CheckedChanged);
            // 
            // serverStatusLabel
            // 
            this.serverStatusLabel.AutoSize = true;
            this.serverStatusLabel.Location = new System.Drawing.Point(195, 11);
            this.serverStatusLabel.Name = "serverStatusLabel";
            this.serverStatusLabel.Size = new System.Drawing.Size(97, 13);
            this.serverStatusLabel.TabIndex = 13;
            this.serverStatusLabel.Text = "SERVER STATUS";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.Green;
            this.statusLabel.Location = new System.Drawing.Point(184, 23);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(122, 31);
            this.statusLabel.TabIndex = 14;
            this.statusLabel.Text = "ONLINE";
            // 
            // refreshButton
            // 
            this.refreshButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.Location = new System.Drawing.Point(358, 64);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.refreshButton.Size = new System.Drawing.Size(93, 40);
            this.refreshButton.TabIndex = 15;
            this.refreshButton.Text = "Update Dictionary";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // timeoutButton
            // 
            this.timeoutButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.timeoutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeoutButton.Location = new System.Drawing.Point(88, 131);
            this.timeoutButton.Name = "timeoutButton";
            this.timeoutButton.Size = new System.Drawing.Size(75, 36);
            this.timeoutButton.TabIndex = 16;
            this.timeoutButton.Text = "Update Timeout";
            this.timeoutButton.UseVisualStyleBackColor = false;
            this.timeoutButton.Click += new System.EventHandler(this.timeoutButton_Click);
            // 
            // WindowInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(460, 206);
            this.Controls.Add(this.timeoutButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.serverStatusLabel);
            this.Controls.Add(this.debugCheckbox);
            this.Controls.Add(this.dictionaryLabel);
            this.Controls.Add(this.dictionaryListBox);
            this.Controls.Add(this.timeoutLlabel);
            this.Controls.Add(this.timeoutNumericUpDown);
            this.Controls.Add(this.Removebutton);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.lookupLabel);
            this.Controls.Add(this.locationTextBox);
            this.Controls.Add(this.lookupTextBox);
            this.Controls.Add(this.dictionaryButton);
            this.Controls.Add(this.stopServerButton);
            this.Controls.Add(this.startServerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WindowInterface";
            this.Text = "WindowInterface";
            ((System.ComponentModel.ISupportInitialize)(this.timeoutNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.Button stopServerButton;
        private System.Windows.Forms.Button dictionaryButton;
        private System.Windows.Forms.TextBox lookupTextBox;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.Label lookupLabel;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Button Removebutton;
        private System.Windows.Forms.NumericUpDown timeoutNumericUpDown;
        private System.Windows.Forms.Label timeoutLlabel;
        private System.Windows.Forms.ListBox dictionaryListBox;
        private System.Windows.Forms.Label dictionaryLabel;
        private System.Windows.Forms.CheckBox debugCheckbox;
        private System.Windows.Forms.Label serverStatusLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button timeoutButton;
    }
}