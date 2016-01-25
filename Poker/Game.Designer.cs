namespace Poker
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    partial class Game
    {
        private const byte GdiCharSet = 204;
        private const string FontFamily = "Microsoft Sans Serif";
        private const FontStyle FontStyle = System.Drawing.FontStyle.Regular;
        private const GraphicsUnit GraphicsUnit = System.Drawing.GraphicsUnit.Point;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        private Button buttonFold;
        private Button buttonCheck;
        private Button buttonCall;
        private Button buttonRaise;
        private ProgressBar progressbarTimer;
        private Button buttonAddChips;
        private TextBox textBoxAddChips;
        //private TextBox textBoxBot5Chips;
        //private TextBox textBoxBot4Chips;
        //private TextBox textBoxBot3Chips;
        //private TextBox textBoxBot2Chips;
        //private TextBox textBoxBot1Chips;
        private TextBox textBoxGamePot;
        private Button buttonBlindOptions;
        private Button buttonBigBlind;
        private TextBox textBoxSmallBlind;
        private Button buttonSmallBlind;
        private TextBox textBoxBigBlind;
        //private Label bot5Status;
        //private Label bot4Status;
        //private Label bot3Status;
        //private Label bot1Status;
        //private Label bot2Status;
        private Label potLabel;
        private TextBox textBoxRaise;

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
            this.player.Status = new Label();
            this.player.TextBoxChips = new TextBox();
            this.buttonFold = new Button();
            this.buttonCheck = new Button();
            this.buttonCall = new Button();
            this.buttonRaise = new Button();
            this.progressbarTimer = new ProgressBar();
            this.buttonAddChips = new Button();
            this.textBoxAddChips = new TextBox();
             this.bot5.TextBoxChips = new TextBox();
             this.bot4.TextBoxChips = new TextBox();
            this.bot3.TextBoxChips = new TextBox();
            this.bot2.TextBoxChips = new TextBox();
            this.bot1.TextBoxChips = new TextBox();
            this.textBoxGamePot = new TextBox();
            this.buttonBlindOptions = new Button();
            this.buttonBigBlind = new Button();
            this.textBoxSmallBlind = new TextBox();
            this.buttonSmallBlind = new Button();
            this.textBoxBigBlind = new TextBox();
            this.bot5.Status = new Label();
            this.bot4.Status = new Label();
            this.bot3.Status = new Label();
            this.bot1.Status = new Label();
           // this.playerStatus = new Label();
            this.bot2.Status = new Label();
            this.potLabel = new Label();
            this.textBoxRaise = new TextBox();
            this.SuspendLayout();

            // 'Fold' button
            this.buttonFold.Anchor = AnchorStyles.Bottom;
            this.buttonFold.Font = new Font(FontFamily, 17F, FontStyle, GraphicsUnit, GdiCharSet);
            this.buttonFold.Location = new Point(335, 660);
            this.buttonFold.Name = "buttonFold";
            this.buttonFold.Size = new Size(130, 62);
            this.buttonFold.TabIndex = 0;
            this.buttonFold.Text = "Fold";
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new EventHandler(this.buttonFold_Click);

            // 'Check' button
            this.buttonCheck.Anchor = AnchorStyles.Bottom;
            this.buttonCheck.Font = new Font(FontFamily, 16F, FontStyle, GraphicsUnit, GdiCharSet);
            this.buttonCheck.Location = new Point(494, 660);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new Size(134, 62);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new EventHandler(this.buttonCheck_Click);

            // 'Call' button
            this.buttonCall.Anchor = AnchorStyles.Bottom;
            this.buttonCall.Font = new Font(FontFamily, 16F, FontStyle, GraphicsUnit, GdiCharSet);
            this.buttonCall.Location = new Point(667, 661);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new Size(126, 62);
            this.buttonCall.TabIndex = 3;
            this.buttonCall.Text = "Call";
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new EventHandler(this.buttonCall_Click);

            // 'Raise' button
            this.buttonRaise.Anchor = AnchorStyles.Bottom;
            this.buttonRaise.Font = new Font(FontFamily, 16F, FontStyle, GraphicsUnit, GdiCharSet);
            this.buttonRaise.Location = new Point(835, 661);
            this.buttonRaise.Name = "buttonRaise";
            this.buttonRaise.Size = new Size(124, 62);
            this.buttonRaise.TabIndex = 4;
            this.buttonRaise.Text = "Raise";
            this.buttonRaise.UseVisualStyleBackColor = true;
            this.buttonRaise.Click += new EventHandler(this.buttonRaise_Click);

            // Progressbar timer
            this.progressbarTimer.Anchor = AnchorStyles.Bottom;
            this.progressbarTimer.BackColor = SystemColors.Control;
            this.progressbarTimer.Location = new Point(335, 631);
            this.progressbarTimer.Maximum = 1000;
            this.progressbarTimer.Name = "progressbarTimer";
            this.progressbarTimer.Size = new Size(667, 23);
            this.progressbarTimer.TabIndex = 5;
            this.progressbarTimer.Value = 1000;

            // Player chips
            this.player.TextBoxChips.Anchor = AnchorStyles.Bottom;
            this.player.TextBoxChips.Font = new Font(FontFamily, 10F, FontStyle, GraphicsUnit, GdiCharSet);
            this.player.TextBoxChips.Location = new Point(755, 553);
            this.player.TextBoxChips.Name = "playerChips";
            this.player.TextBoxChips.Size = new Size(163, 23);
            this.player.TextBoxChips.TabIndex = 6;
            this.player.TextBoxChips.Text = "Chips : 0";

            // 'Add chips' button 
            this.buttonAddChips.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.buttonAddChips.Location = new Point(12, 697);
            this.buttonAddChips.Name = "buttonAddChips";
            this.buttonAddChips.Size = new Size(75, 25);
            this.buttonAddChips.TabIndex = 7;
            this.buttonAddChips.Text = "AddChips";
            this.buttonAddChips.UseVisualStyleBackColor = true;
            this.buttonAddChips.Click += new EventHandler(this.bAdd_Click);

            // Text box for adding chips
            this.textBoxAddChips.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.textBoxAddChips.Location = new Point(93, 700);
            this.textBoxAddChips.Name = "textBoxAddChips";
            this.textBoxAddChips.Size = new Size(125, 20);
            this.textBoxAddChips.TabIndex = 8;

            // Text box for bot #5 chips 
             this.bot5.TextBoxChips.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
             this.bot5.TextBoxChips.Font = new Font(FontFamily, 10F, FontStyle, GraphicsUnit, GdiCharSet);
             this.bot5.TextBoxChips.Location = new Point(1012, 553);
             this.bot5.TextBoxChips.Name = "textBoxBot5Chips";
             this.bot5.TextBoxChips.Size = new Size(152, 23);
             this.bot5.TextBoxChips.TabIndex = 9;
             this.bot5.TextBoxChips.Text = "Chips : 0";

            // Text box for bot #4 chips
             this.bot4.TextBoxChips.Anchor = AnchorStyles.Top | AnchorStyles.Right;
             this.bot4.TextBoxChips.Font = new Font(FontFamily, 10F, FontStyle, GraphicsUnit.Point, GdiCharSet);
             this.bot4.TextBoxChips.Location = new Point(970, 81);
             this.bot4.TextBoxChips.Name = "textBoxBot4Chips";
             this.bot4.TextBoxChips.Size = new Size(123, 23);
             this.bot4.TextBoxChips.TabIndex = 10;
             this.bot4.TextBoxChips.Text = "Chips : 0";

            // Text box for bot #3 chips
             this.bot3.TextBoxChips.Anchor = AnchorStyles.Top | AnchorStyles.Right;
             this.bot3.TextBoxChips.Font = new Font(FontFamily, 10F, FontStyle, GraphicsUnit, GdiCharSet);
             this.bot3.TextBoxChips.Location = new Point(755, 81);
             this.bot3.TextBoxChips.Name = "textBoxBot3Chips";
             this.bot3.TextBoxChips.Size = new Size(125, 23);
             this.bot3.TextBoxChips.TabIndex = 11;
             this.bot3.TextBoxChips.Text = "Chips : 0";

            // Text box for bot #2 chips
             this.bot2.TextBoxChips.Font = new Font(FontFamily, 10F, FontStyle, GraphicsUnit.Point, GdiCharSet);
             this.bot2.TextBoxChips.Location = new Point(276, 81);
             this.bot2.TextBoxChips.Name = "textBoxBot2Chips";
             this.bot2.TextBoxChips.Size = new Size(133, 23);
             this.bot2.TextBoxChips.TabIndex = 12;
             this.bot2.TextBoxChips.Text = "Chips : 0";

            // Text box for bot #1 chips
            this.bot1.TextBoxChips.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
             this.bot1.TextBoxChips.Font = new Font(FontFamily, 10F, FontStyle, GraphicsUnit, GdiCharSet);
             this.bot1.TextBoxChips.Location = new Point(181, 553);
             this.bot1.TextBoxChips.Name = "textBoxBot1Chips";
             this.bot1.TextBoxChips.Size = new Size(142, 23);
             this.bot1.TextBoxChips.TabIndex = 13;
             this.bot1.TextBoxChips.Text = "Chips : 0";

            // Text box for game pot
            this.textBoxGamePot.Anchor = AnchorStyles.None;
            this.textBoxGamePot.Font = new Font(FontFamily, 10F, FontStyle, GraphicsUnit, GdiCharSet);
            this.textBoxGamePot.Location = new Point(606, 212);
            this.textBoxGamePot.Name = "textBoxGamePot";
            this.textBoxGamePot.Size = new Size(125, 23);
            this.textBoxGamePot.TabIndex = 14;
            this.textBoxGamePot.Text = "0";

            // Blind Options button 
            this.buttonBlindOptions.Font = new Font(FontFamily, 12F, FontStyle, GraphicsUnit, GdiCharSet);
            this.buttonBlindOptions.Location = new Point(12, 12);
            this.buttonBlindOptions.Name = "buttonBlindOptions";
            this.buttonBlindOptions.Size = new Size(75, 36);
            this.buttonBlindOptions.TabIndex = 15;
            this.buttonBlindOptions.Text = "BB/SB";
            this.buttonBlindOptions.UseVisualStyleBackColor = true;
            this.buttonBlindOptions.Click += new EventHandler(this.bOptions_Click);

            // 'Big Blind' button
            this.buttonBigBlind.Location = new Point(12, 254);
            this.buttonBigBlind.Name = "buttonBigBlind";
            this.buttonBigBlind.Size = new Size(75, 23);
            this.buttonBigBlind.TabIndex = 16;
            this.buttonBigBlind.Text = "Big Blind";
            this.buttonBigBlind.UseVisualStyleBackColor = true;
            this.buttonBigBlind.Click += new EventHandler(this.buttonBigBlind_Click);

            // Text box 'Small Blind' 
            this.textBoxSmallBlind.Location = new Point(12, 228);
            this.textBoxSmallBlind.Name = "textBoxSmallBlind";
            this.textBoxSmallBlind.Size = new Size(75, 20);
            this.textBoxSmallBlind.TabIndex = 17;
            this.textBoxSmallBlind.Text = "250";

            // 'Small Blind' button
            this.buttonSmallBlind.Location = new Point(12, 199);
            this.buttonSmallBlind.Name = "buttonSmallBlind";
            this.buttonSmallBlind.Size = new Size(75, 23);
            this.buttonSmallBlind.TabIndex = 18;
            this.buttonSmallBlind.Text = "Small Blind";
            this.buttonSmallBlind.UseVisualStyleBackColor = true;
            this.buttonSmallBlind.Click += new EventHandler(this.buttonSmallBlind_Click);

            // Text box 'Big Blind'
            this.textBoxBigBlind.Location = new Point(12, 283);
            this.textBoxBigBlind.Name = "textBoxBigBlind";
            this.textBoxBigBlind.Size = new Size(75, 20);
            this.textBoxBigBlind.TabIndex = 19;
            this.textBoxBigBlind.Text = "500";

            // Bot #5 status
            this.bot5.Status.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.bot5.Status.Location = new Point(1012, 579);
            this.bot5.Status.Name = "bot5Status";
            this.bot5.Status.Size = new Size(152, 32);
            this.bot5.Status.TabIndex = 26;

            // Bot #4 status
            this.bot4.Status.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.bot4.Status.Location = new Point(970, 107);
            this.bot4.Status.Name = "bot4Status";
            this.bot4.Status.Size = new Size(123, 32);
            this.bot4.Status.TabIndex = 27;

            // Bot #3 status
            this.bot3.Status.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.bot3.Status.Location = new Point(755, 107);
            this.bot3.Status.Name = "bot3Status";
            this.bot3.Status.Size = new Size(125, 32);
            this.bot3.Status.TabIndex = 28;

            // Bot #2 status
            this.bot2.Status.Location = new Point(276, 107);
            this.bot2.Status.Name = "bot2Status";
            this.bot2.Status.Size = new Size(133, 32);
            this.bot2.Status.TabIndex = 31;

            // Bot #1 status
            this.bot1.Status.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.bot1.Status.Location = new Point(181, 579);
            this.bot1.Status.Name = "bot1Status";
            this.bot1.Status.Size = new Size(142, 32);
            this.bot1.Status.TabIndex = 29;

            // Player status
            this.player.Status.Anchor = AnchorStyles.Bottom;
            this.player.Status.Location = new Point(755, 579);
            this.player.Status.Name = "playerStatus";
            this.player.Status.Size = new Size(163, 32);
            this.player.Status.TabIndex = 30;

            // Pot label 
            this.potLabel.Anchor = AnchorStyles.None;
            this.potLabel.Font = new Font(FontFamily, 10F, FontStyle, GraphicsUnit, GdiCharSet);
            this.potLabel.Location = new Point(654, 188);
            this.potLabel.Name = "potLabel";
            this.potLabel.Size = new Size(31, 21);
            this.potLabel.TabIndex = 0;
            this.potLabel.Text = "Pot";

            // Text box 'Raise'
            this.textBoxRaise.Anchor = AnchorStyles.Bottom;
            this.textBoxRaise.Location = new Point(965, 703);
            this.textBoxRaise.Name = "textBoxRaise";
            this.textBoxRaise.Size = new Size(108, 20);
            this.textBoxRaise.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImage = Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new Size(1350, 729);
            this.Controls.Add(this.textBoxRaise);
            this.Controls.Add(this.potLabel);
            this.Controls.Add(this.bot2.Status);
            this.Controls.Add(this.player.Status);
            this.Controls.Add(this.bot1.Status);
            this.Controls.Add(this.bot3.Status);
            this.Controls.Add(this.bot4.Status);
            this.Controls.Add(this.bot5.Status);
            this.Controls.Add(this.textBoxBigBlind);
            this.Controls.Add(this.buttonSmallBlind);
            this.Controls.Add(this.textBoxSmallBlind);
            this.Controls.Add(this.buttonBigBlind);
            this.Controls.Add(this.buttonBlindOptions);
            this.Controls.Add(this.textBoxGamePot);
            this.Controls.Add( this.bot1.TextBoxChips);
            this.Controls.Add( this.bot2.TextBoxChips);
            this.Controls.Add( this.bot3.TextBoxChips);
            this.Controls.Add( this.bot4.TextBoxChips);
            this.Controls.Add( this.bot5.TextBoxChips);
            this.Controls.Add(this.textBoxAddChips);
            this.Controls.Add(this.buttonAddChips);
            this.Controls.Add(this.player.TextBoxChips);
            this.Controls.Add(this.progressbarTimer);
            this.Controls.Add(this.buttonRaise);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonFold);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Layout += new LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}