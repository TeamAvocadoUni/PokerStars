using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Models;

namespace Poker
{
    public partial class Game : Form
    {
        #region Variables

        private Character player;
        private Character bot1;
        private Character bot2;
        private Character bot3;
        private Character bot4;
        private Character bot5;
        private double currentRaise;

        //ProgressBar asd = new ProgressBar();
        public int Nm;
        //Panel pPanel = new Panel(); // Player panel
        //Panel b1Panel = new Panel(); // Bot1 panel
        //Panel b2Panel = new Panel(); // Bot2 panel
        //Panel b3Panel = new Panel(); // Bot3 panel
        //Panel b4Panel = new Panel(); // Bot4 panel
        //Panel b5Panel = new Panel(); // Bot5 panel

        int call = 500, foldedPlayers = 5;

        //public int Chips = 10000; // Palyer chips
        //private int bot1Chips = 10000; // Bot1 chips
        //private int bot2Chips = 10000; // Bot2 chips
        //private int bot3Chips = 10000; // Bot3 chips
        //private int bot4Chips = 10000; // Bot4 chips
        //private int bot5Chips = 10000; // Bot5 chips

        private double type, rounds = 0;

        //private double b1Power = 0; // Bot1 power
        //private double b2Power; // Bot2 power
        //private double b3Power; // Bot3 power
        //private double b4Power; // Bot4 power
        //private double b5Power; // Bot5 power
        //private double pPower = 0; // Player power

        //double pType = -1; Player type
        /*Raise = 0,*/
        //private double b1Type = -1; // Bot1 type
        //private double b2Type = -1; // Bot2 type
        //private double b3Type = -1; // Bot3 type
        //private double b4Type = -1; // Bot4 type
        //private double b5Type = -1; // Bot5 type

        //private bool B1turn = false; // Bot1 turn
        //private bool B2turn = false; // Bot2 turn
        //private bool B3turn = false; // Bot3 turn
        //private bool B4turn = false; // Bot4 turn
        //private bool B5turn = false; // Bot5 turn

        //private bool B1Fturn = false; // Bot1 fold turn
        //private bool B2Fturn = false; // Bot2 fold turn
        //private bool B3Fturn = false; // Bot3 fold turn
        //private bool B4Fturn = false; // Bot4 fold turn
        //private bool B5Fturn = false; // Bot5 fold turn

        //private bool pFolded; // Player folded
        //private bool b1Folded; // Bot1 folded
        //private bool b2Folded; // Bot2 folded
        //private bool b3Folded; // Bot3 folded
        //private bool b4Folded; // Bot4 folded
        //private bool b5Folded; // Bot5 folded

        bool intsadded, changed;

        //private int pCall = 0; // Player call
        //private int b1Call = 0; // Bot1 call
        //private int b2Call = 0; // Bot2 call
        //private int b3Call = 0; // Bot3 call
        //private int b4Call = 0; // Bot4 call
        //private int b5Call = 0; // Bot5 call

        //private int pRaise = 0; // Player raise
        //private int b1Raise = 0; // Bot1 raise
        //private int b2Raise = 0; // Bot2 raise
        //private int b3Raise = 0; // Bot3 raise
        //private int b4Raise = 0; // Bot4 raise
        //private int b5Raise = 0; // Bot5 raise

        int height, width, winners = 0, Flop = 1, Turn = 2, River = 3, End = 4, maxLeft = 6;
        int last = 123, raisedTurn = 1;
        List<bool?> bools = new List<bool?>();
        List<Type> Win = new List<Type>();
        List<string> CheckWinners = new List<string>();
        List<int> ints = new List<int>();
        //private bool PFturn = false; // Player fold turn
        //private bool Pturn = true; // Player turn
        private bool restart = false;
        private bool hasRaising; // Raised option was not enabled
        Poker.Type sorted;
        string[] ImgLocation = Directory.GetFiles("..\\..\\Resources\\Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        int[] Reserve = new int[17];
        Image[] Deck = new Image[52];
        PictureBox[] Holder = new PictureBox[52];
        Timer timer = new Timer();
        Timer Updates = new Timer();
        int t = 60, i, bb = 500, sb = 250, up = 10000000, turnCount = 0;
        #endregion
        public Game()
        {
            this.player = new Player(new Panel(), 10000, false, 0, 0, 0, -1, true, false);
            this.bot1 = new Bot(new Panel(), 10000, false, 0, 0, 0, -1, false, false);
            this.bot2 = new Bot(new Panel(), 10000, false, 0, 0, 0, -1, false, false);
            this.bot3 = new Bot(new Panel(), 10000, false, 0, 0, 0, -1, false, false);
            this.bot4 = new Bot(new Panel(), 10000, false, 0, 0, 0, -1, false, false);
            this.bot5 = new Bot(new Panel(), 10000, false, 0, 0, 0, -1, false, false);
            this.currentRaise = 0;

            this.hasRaising = false;

            call = bb;
            MaximizeBox = false;
            MinimizeBox = false;
            Updates.Start();
            InitializeComponent();
            width = this.Width;
            height = this.Height;
            Shuffle();
            textBoxGamePot.Enabled = false;
            playerChips.Enabled = false;
            textBoxBot1Chips.Enabled = false;
            textBoxBot2Chips.Enabled = false;
            textBoxBot3Chips.Enabled = false;
            textBoxBot4Chips.Enabled = false;
            textBoxBot5Chips.Enabled = false;
            playerChips.Text = "Chips : " + player.Chips;
            textBoxBot1Chips.Text = "Chips : " + this.bot1.Chips;
            textBoxBot2Chips.Text = "Chips : " + this.bot2.Chips;
            textBoxBot3Chips.Text = "Chips : " + this.bot3.Chips;
            textBoxBot4Chips.Text = "Chips : " + this.bot4.Chips;
            textBoxBot5Chips.Text = "Chips : " + this.bot5.Chips;
            timer.Interval = (1 * 1 * 1000);
            timer.Tick += timer_Tick;
            Updates.Interval = (1 * 1 * 100);
            Updates.Tick += Update_Tick;
            textBoxBigBlind.Visible = true;
            textBoxSmallBlind.Visible = true;
            buttonBigBlind.Visible = true;
            buttonSmallBlind.Visible = true;
            textBoxBigBlind.Visible = true;
            textBoxSmallBlind.Visible = true;
            buttonBigBlind.Visible = true;
            buttonSmallBlind.Visible = true;
            textBoxBigBlind.Visible = false;
            textBoxSmallBlind.Visible = false;
            buttonBigBlind.Visible = false;
            buttonSmallBlind.Visible = false;
            textBoxRaise.Text = (bb * 2).ToString();
        }
        async Task Shuffle()
        {
            bools.Add(this.player.FoldTurn);
            bools.Add(this.bot1.FoldTurn);
            bools.Add(this.bot2.FoldTurn);
            bools.Add(this.bot3.FoldTurn);
            bools.Add(this.bot4.FoldTurn);
            bools.Add(this.bot5.FoldTurn);
            buttonCall.Enabled = false;
            buttonRaise.Enabled = false;
            buttonFold.Enabled = false;
            buttonCheck.Enabled = false;
            MaximizeBox = false;
            MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap("..\\..\\Resources\\Assets\\Back\\Back.png");
            int horizontal = 580, vertical = 480;
            Random r = new Random();
            for (i = ImgLocation.Length; i > 0; i--)
            {
                int j = r.Next(i);
                var k = ImgLocation[j];
                ImgLocation[j] = ImgLocation[i - 1];
                ImgLocation[i - 1] = k;
            }
            for (i = 0; i < 17; i++)
            {
                Deck[i] = Image.FromFile(ImgLocation[i]);
                var charsToRemove = new string[] { "..\\..\\Resources\\Assets\\Cards\\", ".png" };
                foreach (var c in charsToRemove)
                {
                    ImgLocation[i] = ImgLocation[i].Replace(c, string.Empty);
                }
                Reserve[i] = int.Parse(ImgLocation[i]) - 1;
                Holder[i] = new PictureBox();
                Holder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                Holder[i].Height = 130;
                Holder[i].Width = 80;
                this.Controls.Add(Holder[i]);
                Holder[i].Name = "pb" + i.ToString();
                await Task.Delay(200);
                #region Throwing Cards
                if (i < 2)
                {
                    if (Holder[0].Tag != null)
                    {
                        Holder[1].Tag = Reserve[1];
                    }
                    Holder[0].Tag = Reserve[0];
                    Holder[i].Image = Deck[i];
                    Holder[i].Anchor = (AnchorStyles.Bottom);
                    //Holder[i].Dock = DockStyle.Top;
                    Holder[i].Location = new Point(horizontal, vertical);
                    horizontal += Holder[i].Width;
                    this.Controls.Add(player.Panel);
                    player.Panel.Location = new Point(Holder[0].Left - 10, Holder[0].Top - 10);
                    player.Panel.BackColor = Color.DarkBlue;
                    player.Panel.Height = 150;
                    player.Panel.Width = 180;
                    player.Panel.Visible = false;
                }
                if (this.bot1.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 2 && i < 4)
                    {
                        if (Holder[2].Tag != null)
                        {
                            Holder[3].Tag = Reserve[3];
                        }
                        Holder[2].Tag = Reserve[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(this.bot1.Panel);
                        this.bot1.Panel.Location = new Point(Holder[2].Left - 10, Holder[2].Top - 10);
                        this.bot1.Panel.BackColor = Color.DarkBlue;
                        this.bot1.Panel.Height = 150;
                        this.bot1.Panel.Width = 180;
                        this.bot1.Panel.Visible = false;
                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }
                if (this.bot2.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 4 && i < 6)
                    {
                        if (Holder[4].Tag != null)
                        {
                            Holder[5].Tag = Reserve[5];
                        }
                        Holder[4].Tag = Reserve[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(this.bot2.Panel);
                        this.bot2.Panel.Location = new Point(Holder[4].Left - 10, Holder[4].Top - 10);
                        this.bot2.Panel.BackColor = Color.DarkBlue;
                        this.bot2.Panel.Height = 150;
                        this.bot2.Panel.Width = 180;
                        this.bot2.Panel.Visible = false;
                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }
                if (this.bot3.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 6 && i < 8)
                    {
                        if (Holder[6].Tag != null)
                        {
                            Holder[7].Tag = Reserve[7];
                        }
                        Holder[6].Tag = Reserve[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(this.bot3.Panel);
                        this.bot3.Panel.Location = new Point(Holder[6].Left - 10, Holder[6].Top - 10);
                        this.bot3.Panel.BackColor = Color.DarkBlue;
                        this.bot3.Panel.Height = 150;
                        this.bot3.Panel.Width = 180;
                        this.bot3.Panel.Visible = false;
                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }
                if (this.bot4.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 8 && i < 10)
                    {
                        if (Holder[8].Tag != null)
                        {
                            Holder[9].Tag = Reserve[9];
                        }
                        Holder[8].Tag = Reserve[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(this.bot4.Panel);
                        this.bot4.Panel.Location = new Point(Holder[8].Left - 10, Holder[8].Top - 10);
                        this.bot4.Panel.BackColor = Color.DarkBlue;
                        this.bot4.Panel.Height = 150;
                        this.bot4.Panel.Width = 180;
                        this.bot4.Panel.Visible = false;
                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }
                if (this.bot5.Chips > 0)
                {
                    foldedPlayers--;
                    if (i >= 10 && i < 12)
                    {
                        if (Holder[10].Tag != null)
                        {
                            Holder[11].Tag = Reserve[11];
                        }
                        Holder[10].Tag = Reserve[10];
                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }
                        check = true;
                        Holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += Holder[i].Width;
                        Holder[i].Visible = true;
                        this.Controls.Add(this.bot5.Panel);
                        this.bot5.Panel.Location = new Point(Holder[10].Left - 10, Holder[10].Top - 10);
                        this.bot5.Panel.BackColor = Color.DarkBlue;
                        this.bot5.Panel.Height = 150;
                        this.bot5.Panel.Width = 180;
                        this.bot5.Panel.Visible = false;
                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }
                if (i >= 12)
                {
                    Holder[12].Tag = Reserve[12];
                    if (i > 12) Holder[13].Tag = Reserve[13];
                    if (i > 13) Holder[14].Tag = Reserve[14];
                    if (i > 14) Holder[15].Tag = Reserve[15];
                    if (i > 15)
                    {
                        Holder[16].Tag = Reserve[16];

                    }
                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }
                    check = true;
                    if (Holder[i] != null)
                    {
                        Holder[i].Anchor = AnchorStyles.None;
                        Holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        Holder[i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }
                #endregion
                if (this.bot1.Chips <= 0)
                {
                    this.bot1.FoldTurn = true;
                    Holder[2].Visible = false;
                    Holder[3].Visible = false;
                }
                else
                {
                    this.bot1.FoldTurn = false;
                    if (i == 3)
                    {
                        if (Holder[3] != null)
                        {
                            Holder[2].Visible = true;
                            Holder[3].Visible = true;
                        }
                    }
                }
                if (this.bot2.Chips <= 0)
                {
                    this.bot2.FoldTurn = true;
                    Holder[4].Visible = false;
                    Holder[5].Visible = false;
                }
                else
                {
                    this.bot2.FoldTurn = false;
                    if (i == 5)
                    {
                        if (Holder[5] != null)
                        {
                            Holder[4].Visible = true;
                            Holder[5].Visible = true;
                        }
                    }
                }
                if (this.bot3.Chips <= 0)
                {
                    this.bot3.FoldTurn = true;
                    Holder[6].Visible = false;
                    Holder[7].Visible = false;
                }
                else
                {
                    this.bot3.FoldTurn = false;
                    if (i == 7)
                    {
                        if (Holder[7] != null)
                        {
                            Holder[6].Visible = true;
                            Holder[7].Visible = true;
                        }
                    }
                }
                if (this.bot4.Chips <= 0)
                {
                    this.bot4.FoldTurn = true;
                    Holder[8].Visible = false;
                    Holder[9].Visible = false;
                }
                else
                {
                    this.bot4.FoldTurn = false;
                    if (i == 9)
                    {
                        if (Holder[9] != null)
                        {
                            Holder[8].Visible = true;
                            Holder[9].Visible = true;
                        }
                    }
                }
                if (this.bot5.Chips <= 0)
                {
                    this.bot5.FoldTurn = true;
                    Holder[10].Visible = false;
                    Holder[11].Visible = false;
                }
                else
                {
                    this.bot5.FoldTurn = false;
                    if (i == 11)
                    {
                        if (Holder[11] != null)
                        {
                            Holder[10].Visible = true;
                            Holder[11].Visible = true;
                        }
                    }
                }
                if (i == 16)
                {
                    if (!restart)
                    {
                        MaximizeBox = true;
                        MinimizeBox = true;
                    }
                    timer.Start();
                }
            }
            if (foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                foldedPlayers = 5;
            }
            if (i == 17)
            {
                buttonRaise.Enabled = true;
                buttonCall.Enabled = true;
                buttonRaise.Enabled = true;
                buttonRaise.Enabled = true;
                buttonFold.Enabled = true;
            }
        }
        async Task Turns()
        {
            #region Rotating
            if (!player.FoldTurn)
            {
                if (player.Turn)
                {
                    FixCall(playerStatus, this.player.Call, this.player.Raise, 1);
                    //MessageBox.Show("Player's Turn");
                    progressbarTimer.Visible = true;
                    progressbarTimer.Value = 1000;
                    t = 60;
                    up = 10000000;
                    timer.Start();
                    buttonRaise.Enabled = true;
                    buttonCall.Enabled = true;
                    buttonRaise.Enabled = true;
                    buttonRaise.Enabled = true;
                    buttonFold.Enabled = true;
                    turnCount++;
                    FixCall(playerStatus, this.player.Call, this.player.Raise, 2);
                }
            }
            if (player.FoldTurn || !player.Turn)
            {
                await AllIn();
                if (player.FoldTurn && !this.player.Folded)
                {
                    if (buttonCall.Text.Contains("All in") == false || buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        this.player.Folded = true;
                    }
                }
                await CheckRaise(0, 0);
                progressbarTimer.Visible = false;
                buttonRaise.Enabled = false;
                buttonCall.Enabled = false;
                buttonRaise.Enabled = false;
                buttonRaise.Enabled = false;
                buttonFold.Enabled = false;
                timer.Stop();
                this.bot1.Turn = true;
                if (!this.bot1.FoldTurn)
                {
                    if (this.bot1.Turn)
                    {
                        FixCall(bot1Status, this.bot1.Call, this.bot1.Raise, 1);
                        FixCall(bot1Status, this.bot1.Call, this.bot1.Raise, 2);
                        Rules(2, 3, "Bot 1", this.bot1.Type, this.bot1.Power, this.bot1.FoldTurn);
                        MessageBox.Show("Bot 1's Turn");
                        AI(2, 3, this.bot1.Chips, this.bot1.Turn, this.bot1.FoldTurn, bot1Status, 0, this.bot1.Power, this.bot1.Type);
                        turnCount++;
                        last = 1;
                        this.bot1.Turn = false;
                        this.bot2.Turn = true;
                    }
                }
                if (this.bot1.FoldTurn && !this.bot1.Folded)
                {
                    bools.RemoveAt(1);
                    bools.Insert(1, null);
                    maxLeft--;
                    this.bot1.Folded = true;
                }
                if (this.bot1.FoldTurn || !this.bot1.Turn)
                {
                    await CheckRaise(1, 1);
                    this.bot2.Turn = true;
                }
                if (!this.bot2.FoldTurn)
                {
                    if (this.bot2.Turn)
                    {
                        FixCall(bot2Status, this.bot2.Call, this.bot2.Raise, 1);
                        FixCall(bot2Status, this.bot2.Call, this.bot2.Raise, 2);
                        Rules(4, 5, "Bot 2", this.bot2.Type, this.bot2.Power, this.bot2.FoldTurn);
                        MessageBox.Show("Bot 2's Turn");
                        AI(4, 5, this.bot2.Chips, this.bot2.Turn, this.bot2.FoldTurn, bot2Status, 1, this.bot2.Power, this.bot2.Type);
                        turnCount++;
                        last = 2;
                        this.bot2.Turn = false;
                        this.bot3.Turn = true;
                    }
                }
                if (this.bot2.FoldTurn && !this.bot2.Folded)
                {
                    bools.RemoveAt(2);
                    bools.Insert(2, null);
                    maxLeft--;
                    this.bot2.Folded = true;
                }
                if (this.bot2.FoldTurn || !this.bot2.Turn)
                {
                    await CheckRaise(2, 2);
                    this.bot3.Turn = true;
                }
                if (!this.bot3.FoldTurn)
                {
                    if (this.bot3.Turn)
                    {
                        FixCall(bot3Status, this.bot3.Call, this.bot3.Raise, 1);
                        FixCall(bot3Status, this.bot3.Call, this.bot3.Raise, 2);
                        Rules(6, 7, "Bot 3", this.bot3.Type, this.bot3.Power, this.bot3.FoldTurn);
                        MessageBox.Show("Bot 3's Turn");
                        AI(6, 7, this.bot3.Chips, this.bot3.Turn, this.bot3.FoldTurn, bot3Status, 2, this.bot3.Power, this.bot3.Type);
                        turnCount++;
                        last = 3;
                        this.bot3.Turn = false;
                        this.bot4.Turn = true;
                    }
                }
                if (this.bot3.FoldTurn && !this.bot3.Folded)
                {
                    bools.RemoveAt(3);
                    bools.Insert(3, null);
                    maxLeft--;
                    this.bot3.Folded = true;
                }
                if (this.bot3.FoldTurn || !this.bot3.Turn)
                {
                    await CheckRaise(3, 3);
                    this.bot4.Turn = true;
                }
                if (!this.bot4.FoldTurn)
                {
                    if (this.bot4.Turn)
                    {
                        FixCall(bot4Status, this.bot4.Call, this.bot4.Raise, 1);
                        FixCall(bot4Status, this.bot4.Call, this.bot4.Raise, 2);
                        Rules(8, 9, "Bot 4", this.bot4.Type, this.bot4.Power, this.bot4.FoldTurn);
                        MessageBox.Show("Bot 4's Turn");
                        AI(8, 9, this.bot4.Chips, this.bot4.Turn, this.bot4.FoldTurn, bot4Status, 3, this.bot4.Power, this.bot4.Type);
                        turnCount++;
                        last = 4;
                        this.bot4.Turn = false;
                        this.bot5.Turn = true;
                    }
                }
                if (this.bot4.FoldTurn && !this.bot4.Folded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);
                    maxLeft--;
                    this.bot4.Folded = true;
                }
                if (this.bot4.FoldTurn || !this.bot4.Turn)
                {
                    await CheckRaise(4, 4);
                    this.bot5.Turn = true;
                }
                if (!this.bot5.FoldTurn)
                {
                    if (this.bot5.Turn)
                    {
                        FixCall(bot5Status, this.bot5.Call, this.bot5.Raise, 1);
                        FixCall(bot5Status, this.bot5.Call, this.bot5.Raise, 2);
                        Rules(10, 11, "Bot 5", this.bot5.Type, this.bot5.Power, this.bot5.FoldTurn);
                        MessageBox.Show("Bot 5's Turn");
                        AI(10, 11, this.bot5.Chips, this.bot5.Turn, this.bot5.FoldTurn, bot5Status, 4, this.bot5.Power, this.bot5.Type);
                        turnCount++;
                        last = 5;
                        this.bot5.Turn = false;
                    }
                }
                if (this.bot5.FoldTurn && !this.bot5.Folded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);
                    maxLeft--;
                    this.bot5.Folded = true;
                }
                if (this.bot5.FoldTurn || !this.bot5.Turn)
                {
                    await CheckRaise(5, 5);
                    player.Turn = true;
                }
                if (player.FoldTurn && !this.player.Folded)
                {
                    if (buttonCall.Text.Contains("All in") == false || buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        this.player.Folded = true;
                    }
                }
                #endregion
                await AllIn();
                if (!restart)
                {
                    await Turns();
                }
                restart = false;
            }
        }

        void Rules(int c1, int c2, string currentText, double current, double Power, bool foldedTurn) // Removed references!!!
        {
            //if (c1 == 0 && c2 == 1)
            //{
            //}
            if (!foldedTurn || c1 == 0 && c2 == 1 && playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = Reserve[c1];
                Straight[1] = Reserve[c2];
                Straight1[0] = Straight[2] = Reserve[12];
                Straight1[1] = Straight[3] = Reserve[13];
                Straight1[2] = Straight[4] = Reserve[14];
                Straight1[3] = Straight[5] = Reserve[15];
                Straight1[4] = Straight[6] = Reserve[16];
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight); Array.Sort(st1); Array.Sort(st2); Array.Sort(st3); Array.Sort(st4);
                #endregion
                for (i = 0; i < 16; i++)
                {
                    if (Reserve[i] == int.Parse(Holder[c1].Tag.ToString()) && Reserve[i + 1] == int.Parse(Holder[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        rPairFromHand(current, Power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        rPairTwoPair(current, Power);
                        #endregion

                        #region Two Pair current = 2
                        rTwoPair(current, Power);
                        #endregion

                        #region Three of a kind current = 3
                        rThreeOfAKind(current, Power, Straight);
                        #endregion

                        #region Straight current = 4
                        rStraight(current, Power, Straight);
                        #endregion

                        #region Flush current = 5 || 5.5
                        rFlush(current, Power, vf, Straight1);
                        #endregion

                        #region Full House current = 6
                        rFullHouse(current, Power, done, Straight);
                        #endregion

                        #region Four of a Kind current = 7
                        rFourOfAKind(current, Power, Straight);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        rStraightFlush(current, Power, st1, st2, st3, st4);
                        #endregion

                        #region High Card current = -1
                        rHighCard(current, Power);
                        #endregion
                    }
                }
            }
        }
        private void rStraightFlush(double current, double Power, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        current = 8;
                        Power = (st1.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        current = 9;
                        Power = (st1.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;
                        Power = (st2.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        current = 9;
                        Power = (st2.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;
                        Power = (st3.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        current = 9;
                        Power = (st3.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;
                        Power = (st4.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        current = 9;
                        Power = (st4.Max()) / 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rFourOfAKind(double current, double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 7 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rFullHouse(double current, double Power, bool done, int[] Straight)
        {
            if (current >= -1)
            {
                type = Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                Power = 13 * 2 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 6 });
                                sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }
                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }
                if (current != 6)
                {
                    Power = type;
                }
            }
        }
        private void rFlush(double current, double Power, bool vf, int[] Straight1)
        {
            if (current >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f1[0] % 4)
                    {
                        if (Reserve[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f1.Max() / 4 && Reserve[i + 1] / 4 < f1.Max() / 4)
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 4)//different cards in hand
                {
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f1[0] % 4)
                    {
                        if (Reserve[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f1[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 5)
                {
                    if (Reserve[i] % 4 == f1[0] % 4 && Reserve[i] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f1[0] % 4 && Reserve[i + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f1.Min() / 4 && Reserve[i + 1] / 4 < f1.Min())
                    {
                        current = 5;
                        Power = f1.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f2[0] % 4)
                    {
                        if (Reserve[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f2.Max() / 4 && Reserve[i + 1] / 4 < f2.Max() / 4)
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 4)//different cards in hand
                {
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f2[0] % 4)
                    {
                        if (Reserve[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f2[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 5)
                {
                    if (Reserve[i] % 4 == f2[0] % 4 && Reserve[i] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f2[0] % 4 && Reserve[i + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f2.Min() / 4 && Reserve[i + 1] / 4 < f2.Min())
                    {
                        current = 5;
                        Power = f2.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f3[0] % 4)
                    {
                        if (Reserve[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f3.Max() / 4 && Reserve[i + 1] / 4 < f3.Max() / 4)
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 4)//different cards in hand
                {
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f3[0] % 4)
                    {
                        if (Reserve[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f3[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 5)
                {
                    if (Reserve[i] % 4 == f3[0] % 4 && Reserve[i] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f3[0] % 4 && Reserve[i + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f3.Min() / 4 && Reserve[i + 1] / 4 < f3.Min())
                    {
                        current = 5;
                        Power = f3.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f4[0] % 4)
                    {
                        if (Reserve[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f4.Max() / 4 && Reserve[i + 1] / 4 < f4.Max() / 4)
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 4)//different cards in hand
                {
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f4[0] % 4)
                    {
                        if (Reserve[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f4[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 5)
                {
                    if (Reserve[i] % 4 == f4[0] % 4 && Reserve[i] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f4[0] % 4 && Reserve[i + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i + 1] + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f4.Min() / 4 && Reserve[i + 1] / 4 < f4.Min())
                    {
                        current = 5;
                        Power = f4.Max() + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f4[0] % 4 && vf)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rStraight(double current, double Power, int[] Straight)
        {
            if (current >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 4 });
                            sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        Win.Add(new Type() { Power = Power, Current = 4 });
                        sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rThreeOfAKind(double current, double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            Power = 13 * 3 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 3 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }
        private void rTwoPair(double current, double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (Reserve[i] / 4 != Reserve[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (Reserve[i] / 4 == Reserve[tc] / 4 && Reserve[i + 1] / 4 == Reserve[tc - k] / 4 ||
                                    Reserve[i + 1] / 4 == Reserve[tc] / 4 && Reserve[i] / 4 == Reserve[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (Reserve[i + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (Reserve[i] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1] / 4 != 0 && Reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[i] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void rPairTwoPair(double current, double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (Reserve[tc] / 4 == Reserve[tc - k] / 4)
                            {
                                if (Reserve[tc] / 4 != Reserve[i] / 4 && Reserve[tc] / 4 != Reserve[i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[i] / 4) * 2 + 13 * 4 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[tc] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[tc] / 4) * 2 + (Reserve[i] / 4) * 2 + current * 100;
                                            Win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (Reserve[i] / 4 > Reserve[i + 1] / 4)
                                        {
                                            if (Reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + Reserve[i] / 4 + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = Reserve[tc] / 4 + Reserve[i] / 4 + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (Reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + Reserve[i + 1] + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = Reserve[tc] / 4 + Reserve[i + 1] / 4 + current * 100;
                                                Win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                    }
                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void rPairFromHand(double current, double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (Reserve[i] / 4 == Reserve[i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (Reserve[i] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (Reserve[i + 1] / 4) * 4 + current * 100;
                            Win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (Reserve[i + 1] / 4 == Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + Reserve[i] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (Reserve[i + 1] / 4) * 4 + Reserve[i] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (Reserve[i] / 4 == Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + Reserve[i + 1] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (Reserve[tc] / 4) * 4 + Reserve[i + 1] / 4 + current * 100;
                                Win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }
        private void rHighCard(double current, double Power)
        {
            if (current == -1)
            {
                if (Reserve[i] / 4 > Reserve[i + 1] / 4)
                {
                    current = -1;
                    Power = Reserve[i] / 4;
                    Win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = Reserve[i + 1] / 4;
                    Win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (Reserve[i] / 4 == 0 || Reserve[i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;
                    Win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (Holder[j].Visible)
                    Holder[j].Image = Deck[j];
            }
            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            if (currentText == lastly)//lastfixed
            {
                if (winners > 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        player.Chips += int.Parse(textBoxGamePot.Text) / winners;
                        playerChips.Text = player.Chips.ToString();
                        //pPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        this.bot1.Chips += int.Parse(textBoxGamePot.Text) / winners;
                        textBoxBot1Chips.Text = this.bot1.Chips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        this.bot2.Chips += int.Parse(textBoxGamePot.Text) / winners;
                        textBoxBot2Chips.Text = this.bot2.Chips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        this.bot3.Chips += int.Parse(textBoxGamePot.Text) / winners;
                        textBoxBot3Chips.Text = this.bot3.Chips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        this.bot4.Chips += int.Parse(textBoxGamePot.Text) / winners;
                        textBoxBot4Chips.Text = this.bot4.Chips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        this.bot5.Chips += int.Parse(textBoxGamePot.Text) / winners;
                        textBoxBot5Chips.Text = this.bot5.Chips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        player.Chips += int.Parse(textBoxGamePot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        this.bot1.Chips += int.Parse(textBoxGamePot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        this.bot2.Chips += int.Parse(textBoxGamePot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        this.bot3.Chips += int.Parse(textBoxGamePot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        this.bot4.Chips += int.Parse(textBoxGamePot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        this.bot5.Chips += int.Parse(textBoxGamePot.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
                    }
                }
            }
        }
        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (this.hasRaising)
            {
                turnCount = 0;
                this.hasRaising = false;
                raisedTurn = currentTurn;
                changed = true;
            }
            else
            {
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        this.currentRaise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!player.FoldTurn)
                            playerStatus.Text = "";
                        if (!this.bot1.FoldTurn)
                            bot1Status.Text = "";
                        if (!this.bot2.FoldTurn)
                            bot2Status.Text = "";
                        if (!this.bot3.FoldTurn)
                            bot3Status.Text = "";
                        if (!this.bot4.FoldTurn)
                            bot4Status.Text = "";
                        if (!this.bot5.FoldTurn)
                            bot5Status.Text = "";
                    }
                }
            }
            if (rounds == Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        player.Call = 0; player.Raise = 0;
                        this.bot1.Call = 0; this.bot1.Raise = 0;
                        this.bot2.Call = 0; this.bot2.Raise = 0;
                        this.bot3.Call = 0; this.bot3.Raise = 0;
                        this.bot4.Call = 0; this.bot4.Raise = 0;
                        this.bot5.Call = 0; this.bot5.Raise = 0;
                    }
                }
            }
            if (rounds == Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        player.Call = 0; player.Raise = 0;
                        this.bot1.Call = 0; this.bot1.Raise = 0;
                        this.bot2.Call = 0; this.bot2.Raise = 0;
                        this.bot3.Call = 0; this.bot3.Raise = 0;
                        this.bot4.Call = 0; this.bot4.Raise = 0;
                        this.bot5.Call = 0; this.bot5.Raise = 0;
                    }
                }
            }
            if (rounds == River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (Holder[j].Image != Deck[j])
                    {
                        Holder[j].Image = Deck[j];
                        player.Call = 0; player.Raise = 0;
                        this.bot1.Call = 0; this.bot1.Raise = 0;
                        this.bot2.Call = 0; this.bot2.Raise = 0;
                        this.bot3.Call = 0; this.bot3.Raise = 0;
                        this.bot4.Call = 0; this.bot4.Raise = 0;
                        this.bot5.Call = 0; this.bot5.Raise = 0;
                    }
                }
            }
            if (rounds == End && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", this.player.Type, this.player.Power, this.player.FoldTurn);
                }
                if (!bot1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", this.bot1.Type, this.bot1.Power, this.bot1.FoldTurn);
                }
                if (!bot2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", this.bot2.Type, this.bot2.Power, this.bot2.FoldTurn);
                }
                if (!bot3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", this.bot3.Type, this.bot3.Power, this.bot3.FoldTurn);
                }
                if (!bot4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", this.bot4.Type, this.bot4.Power, this.bot4.FoldTurn);
                }
                if (!bot5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", this.bot5.Type, this.bot5.Power, this.bot5.FoldTurn);
                }
                Winner(player.Type, player.Power, "Player", player.Chips, fixedLast);
                Winner(this.bot1.Type, this.bot1.Power, "Bot 1", this.bot1.Chips, fixedLast);
                Winner(this.bot2.Type, this.bot2.Power, "Bot 2", this.bot2.Chips, fixedLast);
                Winner(this.bot3.Type, this.bot3.Power, "Bot 3", this.bot3.Chips, fixedLast);
                Winner(this.bot4.Type, this.bot4.Power, "Bot 4", this.bot4.Chips, fixedLast);
                Winner(this.bot5.Type, this.bot5.Power, "Bot 5", this.bot5.Chips, fixedLast);
                restart = true;
                player.Turn = true;
                player.FoldTurn = false;
                this.bot1.FoldTurn = false;
                this.bot2.FoldTurn = false;
                this.bot3.FoldTurn = false;
                this.bot4.FoldTurn = false;
                this.bot5.FoldTurn = false;
                if (player.Chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        player.Chips = f2.a;
                        this.bot1.Chips += f2.a;
                        this.bot2.Chips += f2.a;
                        this.bot3.Chips += f2.a;
                        this.bot4.Chips += f2.a;
                        this.bot5.Chips += f2.a;
                        player.FoldTurn = false;
                        player.Turn = true;
                        buttonRaise.Enabled = true;
                        buttonFold.Enabled = true;
                        buttonCheck.Enabled = true;
                        buttonRaise.Text = "Raise";
                    }
                }

                this.player.Panel.Visible = false;
                this.bot1.Panel.Visible = false;
                this.bot2.Panel.Visible = false;
                this.bot3.Panel.Visible = false;
                this.bot4.Panel.Visible = false;
                this.bot5.Panel.Visible = false;
                this.player.Call = 0;
                this.player.Raise = 0;
                this.bot1.Call = 0;
                this.bot1.Raise = 0;
                this.bot2.Call = 0;
                this.bot2.Raise = 0;
                this.bot3.Call = 0;
                this.bot3.Raise = 0;
                this.bot4.Call = 0;
                this.bot4.Raise = 0;
                this.bot5.Call = 0;
                this.bot5.Raise = 0;
                last = 0;
                call = bb;
                this.currentRaise = 0;
                ImgLocation = Directory.GetFiles("..\\..\\Resources\\Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
                rounds = 0;
                this.player.Power = 0;
                this.player.Type = -1;
                type = 0;
                this.bot1.Power = 0;
                this.bot2.Power = 0;
                this.bot3.Power = 0;
                this.bot4.Power = 0;
                this.bot5.Power = 0;
                this.bot1.Type = -1;
                this.bot2.Type = -1;
                this.bot3.Type = -1;
                this.bot4.Type = -1;
                this.bot5.Type = -1;
                ints.Clear();
                CheckWinners.Clear();
                winners = 0;
                Win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    Holder[os].Image = null;
                    Holder[os].Invalidate();
                    Holder[os].Visible = false;
                }
                textBoxGamePot.Text = "0";
                playerStatus.Text = "";
                await Shuffle();
                await Turns();
            }
        }
        void FixCall(Label status, int cCall, int cRaise, int options)
        {
            if (rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }
                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }
                    if (status.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }
                if (options == 2)
                {
                    if (cRaise != this.currentRaise && cRaise <= this.currentRaise)
                    {
                        call = Convert.ToInt32(this.currentRaise) - cRaise;
                    }
                    if (cCall != call || cCall <= call)
                    {
                        call = call - cCall;
                    }
                    if (cRaise == this.currentRaise && this.currentRaise > 0)
                    {
                        call = 0;
                        buttonCall.Enabled = false;
                        buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }
        async Task AllIn()
        {
            #region All in
            if (player.Chips <= 0 && !intsadded)
            {
                if (playerStatus.Text.Contains("Raise"))
                {
                    ints.Add(player.Chips);
                    intsadded = true;
                }
                if (playerStatus.Text.Contains("Call"))
                {
                    ints.Add(player.Chips);
                    intsadded = true;
                }
            }
            intsadded = false;
            if (this.bot1.Chips <= 0 && !this.bot1.FoldTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.bot1.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (this.bot2.Chips <= 0 && !this.bot2.FoldTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.bot2.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (this.bot3.Chips <= 0 && !this.bot3.FoldTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.bot3.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (this.bot4.Chips <= 0 && !this.bot4.FoldTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.bot4.Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (this.bot5.Chips <= 0 && !this.bot5.FoldTurn)
            {
                if (!intsadded)
                {
                    ints.Add(this.bot5.Chips);
                    intsadded = true;
                }
            }
            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            #endregion

            var abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                if (index == 0)
                {
                    player.Chips += int.Parse(textBoxGamePot.Text);
                    playerChips.Text = player.Chips.ToString();
                    player.Panel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    this.bot1.Chips += int.Parse(textBoxGamePot.Text);
                    playerChips.Text = this.bot1.Chips.ToString();
                    this.bot1.Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    this.bot2.Chips += int.Parse(textBoxGamePot.Text);
                    playerChips.Text = this.bot2.Chips.ToString();
                    this.bot2.Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    this.bot3.Chips += int.Parse(textBoxGamePot.Text);
                    playerChips.Text = this.bot3.Chips.ToString();
                    this.bot3.Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    this.bot4.Chips += int.Parse(textBoxGamePot.Text);
                    playerChips.Text = this.bot4.Chips.ToString();
                    this.bot4.Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    this.bot5.Chips += int.Parse(textBoxGamePot.Text);
                    playerChips.Text = this.bot5.Chips.ToString();
                    this.bot5.Panel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (int j = 0; j <= 16; j++)
                {
                    Holder[j].Visible = false;
                }
                await Finish(1);
            }
            intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && rounds >= End)
            {
                await Finish(2);
            }
            #endregion


        }
        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }
            player.Panel.Visible = false; this.bot1.Panel.Visible = false; this.bot2.Panel.Visible = false; this.bot3.Panel.Visible = false; this.bot4.Panel.Visible = false; this.bot5.Panel.Visible = false;
            call = bb; this.currentRaise = 0;
            foldedPlayers = 5;
            type = 0; rounds = 0; this.bot1.Power = 0; this.bot2.Power = 0; this.bot3.Power = 0; this.bot4.Power = 0; this.bot5.Power = 0; player.Power = 0; player.Type = -1; /*Raise = 0;*/
            this.bot1.Type = -1; this.bot2.Type = -1; this.bot3.Type = -1; this.bot4.Type = -1; this.bot5.Type = -1;
            this.bot1.Turn = false; this.bot2.Turn = false; this.bot3.Turn = false; this.bot4.Turn = false; this.bot5.Turn = false;
            this.bot1.FoldTurn = false; this.bot2.FoldTurn = false; this.bot3.FoldTurn = false; this.bot4.FoldTurn = false; this.bot5.FoldTurn = false;
            this.player.Folded = false; this.bot1.Folded = false; this.bot2.Folded = false; this.bot3.Folded = false; this.bot4.Folded = false; this.bot5.Folded = false;
            player.FoldTurn = false; player.Turn = true; restart = false; this.hasRaising = false;
            player.Call = 0; this.bot1.Call = 0; this.bot2.Call = 0; this.bot3.Call = 0; this.bot4.Call = 0; this.bot5.Call = 0; player.Raise = 0; this.bot1.Raise = 0; this.bot2.Raise = 0; this.bot3.Raise = 0; this.bot4.Raise = 0; this.bot5.Raise = 0;
            height = 0; width = 0; winners = 0; Flop = 1; Turn = 2; River = 3; End = 4; maxLeft = 6;
            last = 123; raisedTurn = 1;
            bools.Clear();
            CheckWinners.Clear();
            ints.Clear();
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            textBoxGamePot.Text = "0";
            t = 60; up = 10000000; turnCount = 0;
            playerStatus.Text = "";
            bot1Status.Text = "";
            bot2Status.Text = "";
            bot3Status.Text = "";
            bot4Status.Text = "";
            bot5Status.Text = "";
            if (player.Chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    player.Chips = f2.a;
                    this.bot1.Chips += f2.a;
                    this.bot2.Chips += f2.a;
                    this.bot3.Chips += f2.a;
                    this.bot4.Chips += f2.a;
                    this.bot5.Chips += f2.a;
                    player.FoldTurn = false;
                    player.Turn = true;
                    buttonRaise.Enabled = true;
                    buttonFold.Enabled = true;
                    buttonCheck.Enabled = true;
                    buttonRaise.Text = "Raise";
                }
            }
            ImgLocation = Directory.GetFiles("..\\..\\Resources\\Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                Holder[os].Image = null;
                Holder[os].Invalidate();
                Holder[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
        }
        void FixWinners()
        {
            Win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", this.player.Type, this.player.Power, this.player.FoldTurn);
            }
            if (!bot1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, "Bot 1", this.bot1.Type, this.bot1.Power, this.bot1.FoldTurn);
            }
            if (!bot2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", this.bot2.Type, this.bot2.Power, this.bot2.FoldTurn);
            }
            if (!bot3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", this.bot3.Type, this.bot3.Power, this.bot3.FoldTurn);
            }
            if (!bot4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", this.bot4.Type, this.bot4.Power, this.bot4.FoldTurn);
            }
            if (!bot5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", this.bot5.Type, this.bot5.Power, this.bot5.FoldTurn);
            }
            Winner(player.Type, player.Power, "Player", player.Chips, fixedLast);
            Winner(this.bot1.Type, this.bot1.Power, "Bot 1", this.bot1.Chips, fixedLast);
            Winner(this.bot2.Type, this.bot2.Power, "Bot 2", this.bot2.Chips, fixedLast);
            Winner(this.bot3.Type, this.bot3.Power, "Bot 3", this.bot3.Chips, fixedLast);
            Winner(this.bot4.Type, this.bot4.Power, "Bot 4", this.bot4.Chips, fixedLast);
            Winner(this.bot5.Type, this.bot5.Power, "Bot 5", this.bot5.Chips, fixedLast);
        }
        void AI(int c1, int c2, int sChips, bool sTurn, bool sFTurn, Label sStatus, int name, double botPower, double botCurrent)
        {
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    HighCard(sChips, sTurn, sFTurn, sStatus, botPower);
                }
                if (botCurrent == 0)
                {
                    PairTable(sChips, sTurn, sFTurn, sStatus, botPower);
                }
                if (botCurrent == 1)
                {
                    PairHand(sChips, sTurn, sFTurn, sStatus, botPower);
                }
                if (botCurrent == 2)
                {
                    TwoPair(sChips, sTurn, sFTurn, sStatus, botPower);
                }
                if (botCurrent == 3)
                {
                    ThreeOfAKind(sChips, sTurn, sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 4)
                {
                    Straight(sChips, sTurn, sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    Flush(sChips, sTurn, sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 6)
                {
                    FullHouse(sChips, sTurn, sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 7)
                {
                    FourOfAKind(sChips, sTurn, sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 8 || botCurrent == 9)
                {
                    StraightFlush(sChips, sTurn, sFTurn, sStatus, name, botPower);
                }
            }
            if (sFTurn)
            {
                Holder[c1].Visible = false;
                Holder[c2].Visible = false;
            }
        }
        private void HighCard(int sChips, bool sTurn, bool sFTurn, Label sStatus, double botPower)
        {
            HP(sChips, sTurn, sFTurn, sStatus, botPower, 20, 25);
        }
        private void PairTable(int sChips, bool sTurn, bool sFTurn, Label sStatus, double botPower)
        {
            HP(sChips, sTurn, sFTurn, sStatus, botPower, 16, 25);
        }
        private void PairHand(int sChips, bool sTurn, bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                PH(sChips, sTurn, sFTurn, sStatus, rCall, 6, rRaise);
            }
            if (botPower <= 139 && botPower >= 128)
            {
                PH(sChips, sTurn, sFTurn, sStatus, rCall, 7, rRaise);
            }
            if (botPower < 128 && botPower >= 101)
            {
                PH(sChips, sTurn, sFTurn, sStatus, rCall, 9, rRaise);
            }
        }
        private void TwoPair(int sChips, bool sTurn, bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                PH(sChips, sTurn, sFTurn, sStatus, rCall, 3, rRaise);
            }
            if (botPower <= 244 && botPower >= 234)
            {
                PH(sChips, sTurn, sFTurn, sStatus, rCall, 4, rRaise);
            }
            if (botPower < 234 && botPower >= 201)
            {
                PH(sChips, sTurn, sFTurn, sStatus, rCall, 4, rRaise);
            }
        }
        private void ThreeOfAKind(int sChips, bool sTurn, bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, tCall, tRaise);
                //sTurn = this.SwitchPlayerTurn();
            }
            if (botPower <= 327 && botPower >= 321)//10  8
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower < 321 && botPower >= 303)//7 2
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, tCall, tRaise);
            }
        }
        private void Straight(int sChips, bool sTurn, bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower <= 409 && botPower >= 407)//10  8
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower < 407 && botPower >= 404)
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, sCall, sRaise);
            }
        }
        private void Flush(int sChips, bool sTurn, bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(sChips, sTurn, sFTurn, sStatus, name, fCall, fRaise);
        }
        private void FullHouse(int sChips, bool sTurn, bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, fhCall, fhRaise);
            }
            if (botPower < 620 && botPower >= 602)
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, fhCall, fhRaise);
            }
        }
        private void FourOfAKind(int sChips, bool sTurn, bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, fkCall, fkRaise);
            }
        }
        private void StraightFlush(int sChips, bool sTurn, bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                Smooth(sChips, sTurn, sFTurn, sStatus, name, sfCall, sfRaise);
            }
        }

        private void Fold(bool sTurn, bool sFTurn, Label sStatus)
        {
            this.hasRaising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        private void Check(bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            hasRaising = false;
        }

        private void Call(int sChips, bool sTurn, Label sStatus)
        {
            hasRaising = false;
            sTurn = false;
            sChips -= call;
            sStatus.Text = "Call " + call;
            textBoxGamePot.Text = (int.Parse(textBoxGamePot.Text) + call).ToString();
        }

        //#region Separating methods with applied single-resposibility principle
        //private bool SwitchPlayerTurn()
        //{
        //    return false;
        //}

        //private bool SwitchPlayerFoldTurn()
        //{
        //    return true;
        //}

        //private string RefreshPlayerStatusOnCall(Label sStatus)
        //{
        //    return sStatus.Text = "Call " + call;
        //}

        //private string RefreshGamePotTextBox()
        //{
        //    return textBoxGamePot.Text = (int.Parse(textBoxGamePot.Text) + call).ToString();
        //}
        //#endregion

        private void Raised(int sChips, bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(this.currentRaise);
            sStatus.Text = "Raise " + this.currentRaise;
            textBoxGamePot.Text = (int.Parse(textBoxGamePot.Text) + Convert.ToInt32(this.currentRaise)).ToString();
            call = Convert.ToInt32(this.currentRaise);
            this.hasRaising = true;
            //sTurn = false;
        }
        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }
        private void HP(int sChips, bool sTurn, bool sFTurn, Label sStatus, double botPower, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (call <= 0)
            {
                Check(sTurn, sStatus);
                //sTurn = this.SwitchPlayerTurn();
            }
            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= RoundN(sChips, n))
                    {
                        Call(sChips, sTurn, sStatus);
                        //sTurn = this.SwitchPlayerTurn();
                        //sStatus.Text = this.RefreshPlayerStatusOnCall(sStatus);
                        //textBoxGamePot.Text = this.RefreshGamePotTextBox();
                    }
                    else
                    {
                        Fold(sTurn, sFTurn, sStatus);
                        //sTurn = this.SwitchPlayerTurn();
                        //sFTurn = this.SwitchPlayerFoldTurn();
                    }
                }
                if (rnd == 2)
                {
                    if (call <= RoundN(sChips, n1))
                    {
                        Call(sChips, sTurn, sStatus);
                        //sTurn = this.SwitchPlayerTurn();
                        //sStatus.Text = this.RefreshPlayerStatusOnCall(sStatus);
                        //textBoxGamePot.Text = this.RefreshGamePotTextBox();
                    }
                    else
                    {
                        Fold(sTurn, sFTurn, sStatus);
                        //sTurn = this.SwitchPlayerTurn();
                        //sFTurn = this.SwitchPlayerFoldTurn();
                    }
                }
            }
            if (rnd == 3)
            {
                if (this.currentRaise == 0)
                {
                    this.currentRaise = call * 2;
                    Raised(sChips, sTurn, sStatus);
                }
                else
                {
                    if (this.currentRaise <= RoundN(sChips, n))
                    {
                        this.currentRaise = call * 2;
                        Raised(sChips, sTurn, sStatus);
                    }
                    else
                    {
                        Fold(sTurn, sFTurn, sStatus);
                        //sTurn = this.SwitchPlayerTurn();
                        //sFTurn = this.SwitchPlayerFoldTurn();
                    }
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }
        private void PH(int sChips, bool sTurn, bool sFTurn, Label sStatus, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (rounds < 2)
            {
                if (call <= 0)
                {
                    this.Check(sTurn, sStatus);
                    //sTurn = this.SwitchPlayerTurn();
                }
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1))
                    {
                        Fold(sTurn, sFTurn, sStatus);
                        //sTurn = this.SwitchPlayerTurn();
                        //sFTurn = this.SwitchPlayerFoldTurn();
                    }
                    if (this.currentRaise > RoundN(sChips, n))
                    {
                        Fold(sTurn, sFTurn, sStatus);
                        //sTurn = this.SwitchPlayerTurn();
                        //sFTurn = this.SwitchPlayerFoldTurn();
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n) && call <= RoundN(sChips, n1))
                        {
                            Call(sChips, sTurn, sStatus);
                            //sTurn = this.SwitchPlayerTurn();
                            //sStatus.Text = this.RefreshPlayerStatusOnCall(sStatus);
                            //textBoxGamePot.Text = this.RefreshGamePotTextBox();
                        }
                        if (this.currentRaise <= RoundN(sChips, n) && this.currentRaise >= (RoundN(sChips, n)) / 2)
                        {
                            Call(sChips, sTurn, sStatus);
                            //sTurn = this.SwitchPlayerTurn();
                            //sStatus.Text = this.RefreshPlayerStatusOnCall(sStatus);
                            //textBoxGamePot.Text = this.RefreshGamePotTextBox();
                        }
                        if (this.currentRaise <= (RoundN(sChips, n)) / 2)
                        {
                            if (this.currentRaise > 0)
                            {
                                this.currentRaise = RoundN(sChips, n);
                                Raised(sChips, sTurn, sStatus);
                            }
                            else
                            {
                                this.currentRaise = call * 2;
                                Raised(sChips, sTurn, sStatus);
                            }
                        }

                    }
                }
            }
            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(sTurn, sFTurn, sStatus);
                        //sTurn = this.SwitchPlayerTurn();
                        //sFTurn = this.SwitchPlayerFoldTurn();
                    }
                    if (this.currentRaise > RoundN(sChips, n - rnd))
                    {
                        Fold(sTurn, sFTurn, sStatus);
                        //sTurn = this.SwitchPlayerTurn();
                        //sFTurn = this.SwitchPlayerFoldTurn();
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n - rnd) && call <= RoundN(sChips, n1 - rnd))
                        {
                            Call(sChips, sTurn, sStatus);
                            //sTurn = this.SwitchPlayerTurn();
                            //sStatus.Text = this.RefreshPlayerStatusOnCall(sStatus);
                            //textBoxGamePot.Text = this.RefreshGamePotTextBox();
                        }
                        if (this.currentRaise <= RoundN(sChips, n - rnd) && this.currentRaise >= (RoundN(sChips, n - rnd)) / 2)
                        {
                            Call(sChips, sTurn, sStatus);
                            //sTurn = this.SwitchPlayerTurn();
                            //sStatus.Text = this.RefreshPlayerStatusOnCall(sStatus);
                            //textBoxGamePot.Text = this.RefreshGamePotTextBox();
                        }
                        if (this.currentRaise <= (RoundN(sChips, n - rnd)) / 2)
                        {
                            if (this.currentRaise > 0)
                            {
                                this.currentRaise = RoundN(sChips, n - rnd);
                                Raised(sChips, sTurn, sStatus);
                            }
                            else
                            {
                                this.currentRaise = call * 2;
                                Raised(sChips, sTurn, sStatus);
                            }
                        }
                    }
                }
                if (call <= 0)
                {
                    this.currentRaise = RoundN(sChips, r - rnd);
                    Raised(sChips, sTurn, sStatus);
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }
        void Smooth(int botChips, bool botTurn, bool botFTurn, Label botStatus, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (call <= 0)
            {
                this.Check(botTurn, botStatus);
                //botTurn = this.SwitchPlayerTurn();
            }
            else
            {
                if (call >= RoundN(botChips, n))
                {
                    if (botChips > call)
                    {
                        Call(botChips, botTurn, botStatus);
                        //botTurn = this.SwitchPlayerTurn();
                        //botStatus.Text = this.RefreshPlayerStatusOnCall(botStatus);
                        //textBoxGamePot.Text = this.RefreshGamePotTextBox();
                    }
                    else if (botChips <= call)
                    {
                        this.hasRaising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        textBoxGamePot.Text = (int.Parse(textBoxGamePot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (this.currentRaise > 0)
                    {
                        if (botChips >= this.currentRaise * 2)
                        {
                            this.currentRaise *= 2;
                            Raised(botChips, botTurn, botStatus);
                        }
                        else
                        {
                            Call(botChips, botTurn, botStatus);
                            //botTurn = this.SwitchPlayerTurn();
                            //botStatus.Text = this.RefreshPlayerStatusOnCall(botStatus);
                            //textBoxGamePot.Text = this.RefreshGamePotTextBox();
                        }
                    }
                    else
                    {
                        this.currentRaise = call * 2;
                        Raised(botChips, botTurn, botStatus);
                    }
                }
            }
            if (botChips <= 0)
            {
                botFTurn = false;
            }
        }

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (progressbarTimer.Value <= 0)
            {
                player.FoldTurn = true;
                await Turns();
            }
            if (t > 0)
            {
                t--;
                progressbarTimer.Value = (t / 6) * 100;
            }
        }
        private void Update_Tick(object sender, object e)
        {
            if (player.Chips <= 0)
            {
                playerChips.Text = "Chips : 0";
            }
            if (this.bot1.Chips <= 0)
            {
                textBoxBot1Chips.Text = "Chips : 0";
            }
            if (this.bot2.Chips <= 0)
            {
                textBoxBot2Chips.Text = "Chips : 0";
            }
            if (this.bot3.Chips <= 0)
            {
                textBoxBot3Chips.Text = "Chips : 0";
            }
            if (this.bot4.Chips <= 0)
            {
                textBoxBot4Chips.Text = "Chips : 0";
            }
            if (this.bot5.Chips <= 0)
            {
                textBoxBot5Chips.Text = "Chips : 0";
            }
            playerChips.Text = "Chips : " + player.Chips;
            textBoxBot1Chips.Text = "Chips : " + this.bot1.Chips;
            textBoxBot2Chips.Text = "Chips : " + this.bot2.Chips;
            textBoxBot3Chips.Text = "Chips : " + this.bot3.Chips;
            textBoxBot4Chips.Text = "Chips : " + this.bot4.Chips;
            textBoxBot5Chips.Text = "Chips : " + this.bot5.Chips;
            if (player.Chips <= 0)
            {
                player.Turn = false;
                player.FoldTurn = true;
                buttonCall.Enabled = false;
                buttonRaise.Enabled = false;
                buttonFold.Enabled = false;
                buttonCheck.Enabled = false;
            }
            if (up > 0)
            {
                up--;
            }
            if (player.Chips >= call)
            {
                buttonCall.Text = "Call " + call;
            }
            else
            {
                buttonCall.Text = "All in";
                buttonRaise.Enabled = false;
            }
            if (call > 0)
            {
                buttonCheck.Enabled = false;
            }
            if (call <= 0)
            {
                buttonCheck.Enabled = true;
                buttonCall.Text = "Call";
                buttonCall.Enabled = false;
            }
            if (player.Chips <= 0)
            {
                buttonRaise.Enabled = false;
            }
            int parsedValue;

            if (textBoxRaise.Text != "" && int.TryParse(textBoxRaise.Text, out parsedValue))
            {
                if (player.Chips <= int.Parse(textBoxRaise.Text))
                {
                    buttonRaise.Text = "All in";
                }
                else
                {
                    buttonRaise.Text = "Raise";
                }
            }
            if (player.Chips < call)
            {
                buttonRaise.Enabled = false;
            }
        }
        private async void buttonFold_Click(object sender, EventArgs e)
        {
            playerStatus.Text = "Fold";
            player.Turn = false;
            player.FoldTurn = true;
            await Turns();
        }
        private async void buttonCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                player.Turn = false;
                playerStatus.Text = "Check";
            }
            else
            {
                //playerStatus.Text = "All in " + Chips;

                buttonCheck.Enabled = false;
            }
            await Turns();
        }
        private async void buttonCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", this.player.Type, this.player.Power, this.player.FoldTurn);
            if (player.Chips >= call)
            {
                player.Chips -= call;
                playerChips.Text = "Chips : " + player.Chips.ToString();
                if (textBoxGamePot.Text != "")
                {
                    textBoxGamePot.Text = (int.Parse(textBoxGamePot.Text) + call).ToString();
                }
                else
                {
                    textBoxGamePot.Text = call.ToString();
                }
                player.Turn = false;
                playerStatus.Text = "Call " + call;
                player.Call = call;
            }
            else if (player.Chips <= call && call > 0)
            {
                textBoxGamePot.Text = (int.Parse(textBoxGamePot.Text) + player.Chips).ToString();
                playerStatus.Text = "All in " + player.Chips;
                player.Chips = 0;
                playerChips.Text = "Chips : " + player.Chips.ToString();
                player.Turn = false;
                buttonFold.Enabled = false;
                player.Call = player.Chips;
            }
            await Turns();
        }
        private async void buttonRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", this.player.Type, this.player.Power, this.player.FoldTurn);
            int parsedValue;
            if (textBoxRaise.Text != "" && int.TryParse(textBoxRaise.Text, out parsedValue))
            {
                if (player.Chips > call)
                {
                    if (this.currentRaise * 2 > int.Parse(textBoxRaise.Text))
                    {
                        textBoxRaise.Text = (this.currentRaise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (player.Chips >= int.Parse(textBoxRaise.Text))
                        {
                            call = int.Parse(textBoxRaise.Text);
                            this.currentRaise = int.Parse(textBoxRaise.Text);
                            playerStatus.Text = "Raise " + call.ToString();
                            textBoxGamePot.Text = (int.Parse(textBoxGamePot.Text) + call).ToString();
                            buttonCall.Text = "Call";
                            player.Chips -= int.Parse(textBoxRaise.Text);
                            this.hasRaising = true;
                            last = 0;
                            player.Raise = Convert.ToInt32(this.currentRaise);
                        }
                        else
                        {
                            call = player.Chips;
                            this.currentRaise = player.Chips;
                            textBoxGamePot.Text = (int.Parse(textBoxGamePot.Text) + player.Chips).ToString();
                            playerStatus.Text = "Raise " + call.ToString();
                            player.Chips = 0;
                            this.hasRaising = true;
                            last = 0;
                            player.Raise = Convert.ToInt32(this.currentRaise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            player.Turn = false;
            await Turns();
        }
        private void bAdd_Click(object sender, EventArgs e)
        {
            if (textBoxAddChips.Text == "") { }
            else
            {
                player.Chips += int.Parse(textBoxAddChips.Text);
                this.bot1.Chips += int.Parse(textBoxAddChips.Text);
                this.bot2.Chips += int.Parse(textBoxAddChips.Text);
                this.bot3.Chips += int.Parse(textBoxAddChips.Text);
                this.bot4.Chips += int.Parse(textBoxAddChips.Text);
                this.bot5.Chips += int.Parse(textBoxAddChips.Text);
            }
            playerChips.Text = "Chips : " + player.Chips;
        }
        private void bOptions_Click(object sender, EventArgs e)
        {
            textBoxBigBlind.Text = bb.ToString();
            textBoxSmallBlind.Text = sb.ToString();
            if (textBoxBigBlind.Visible == false)
            {
                textBoxBigBlind.Visible = true;
                textBoxSmallBlind.Visible = true;
                buttonBigBlind.Visible = true;
                buttonSmallBlind.Visible = true;
            }
            else
            {
                textBoxBigBlind.Visible = false;
                textBoxSmallBlind.Visible = false;
                buttonBigBlind.Visible = false;
                buttonSmallBlind.Visible = false;
            }
        }
        private void buttonSmallBlind_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (textBoxSmallBlind.Text.Contains(",") || textBoxSmallBlind.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                textBoxSmallBlind.Text = sb.ToString();
                return;
            }
            if (!int.TryParse(textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                textBoxSmallBlind.Text = sb.ToString();
                return;
            }
            if (int.Parse(textBoxSmallBlind.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                textBoxSmallBlind.Text = sb.ToString();
            }
            if (int.Parse(textBoxSmallBlind.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(textBoxSmallBlind.Text) >= 250 && int.Parse(textBoxSmallBlind.Text) <= 100000)
            {
                sb = int.Parse(textBoxSmallBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void buttonBigBlind_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (textBoxBigBlind.Text.Contains(",") || textBoxBigBlind.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                textBoxBigBlind.Text = bb.ToString();
                return;
            }
            if (!int.TryParse(textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                textBoxSmallBlind.Text = bb.ToString();
                return;
            }
            if (int.Parse(textBoxBigBlind.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                textBoxBigBlind.Text = bb.ToString();
            }
            if (int.Parse(textBoxBigBlind.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(textBoxBigBlind.Text) >= 500 && int.Parse(textBoxBigBlind.Text) <= 200000)
            {
                bb = int.Parse(textBoxBigBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }
        #endregion
    }
}