namespace Poker.Models.Character
{
    using System;
    using System.Windows.Forms;

    using Poker.Interfaces;

    public class SingleBet : ISingleBet
    {
        public SingleBet(TextBox betTextBox)
        {
            this.BetTextBox = betTextBox;
            this.BetTextBox.Enabled = false;
            this.BetValue = 0;
            this.UpdateTextBox();
        }

        public TextBox BetTextBox { get; }

        public int BetValue { get; set; }

        public int LastBet { get; set; }

        public void SetBet(int chips)
        {
            this.BetValue = chips;
            this.UpdateTextBox();
        }

        public void AddBet(int chips)
        {
            this.BetValue += chips;
            this.UpdateTextBox();
        }

        public void Clear()
        {
            this.BetValue = 0;
            this.UpdateTextBox();
        }

        private void UpdateTextBox()
        {
            this.BetTextBox.Text = this.BetValue.ToString();
        }
    }
}
