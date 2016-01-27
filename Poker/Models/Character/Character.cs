namespace Poker.Models.Character
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Interfaces;

    public abstract class Character : CardHolder, ICharacter
    {
        private int chips;

        protected Character(int id, string name, Label status, TextBox chipsTextBox, /*int[] cardIndexes,*/ int chips, IList<PictureBox> pictureBoxHolder, Panel panel)
        {
            this.Id = id;
            this.Name = name;
            this.CharacterStatus = status;
            this.TextBoxChips = chipsTextBox;
            this.Chips = chips;
            this.PictureBox = pictureBoxHolder ?? new List<PictureBox>();
            this.CharacterPanel = panel ?? new Panel();
            this.CharacterType = new Type();
            this.Cards = new List<ICard>();
        }

        public int CallValue { get; set; }

        public Panel CharacterPanel { get; set; }

        public Label CharacterStatus { get; set; }

        public Type CharacterType { get; set; }

        public bool HasRaised { get; set; }

        public int Chips
        {
            get
            {
                return this.chips;
            }

            set
            {
                this.chips = value < 0 ? 0 : value;
                this.UpdateChipsTetxBox(this.chips);
                if (this.chips == 0)
                {
                    this.IsInTurn = false;
                    this.FoldTurn = true;
                }
            }
        }

        public bool FoldTurn { get; set; }

        public bool HasFolded { get; set; }

        public int Id { get; set; }

        public bool IsInGame
        {
            get
            {
                 return this.Chips > 0;
            }
        }

        public bool IsInTurn { get; set; }

        public string Name { get; set; }

        public int RaiseValue { get; set; }

        public TextBox TextBoxChips { get; set; }

        public override async Task SetAllCards(IList<ICard> cards)
        {
              for (int i = 0; i < cards.Count; i++)
            {
                await Task.Delay(200);
                this.Cards.Add(cards[i]);
                this.PictureBox[i].Tag = cards[i].CardPower;
                this.SetCardImage(cards[i], this.PictureBox[i]);
                if (this.IsInGame)
                {
                    this.FoldTurn = false;
                    this.PictureBox[i].Visible = true;
                }
                else
                {
                    this.FoldTurn = true;
                    this.PictureBox[i].Visible = false;
                }
            }
        }

        public void Raise(int chipsValue)
        {
            this.CharacterStatus.Text = "Raise " + chipsValue;
            this.Chips -= chipsValue;
            this.RaiseValue = chipsValue;
            this.IsInTurn = false;
        }

        public void Call(int amount)
        {
            this.IsInTurn = false;
            this.Chips -= amount;
            this.CallValue = amount;
            this.CharacterStatus.Text = "Call " + amount;
        }

        public void Fold()
        {
            this.CharacterStatus.Text = "Fold";
            this.IsInTurn = false;
            this.FoldTurn = true;
            this.HasFolded = true;
        }

        public void Check()
        {
            this.CharacterStatus.Text = "Check";
            this.IsInTurn = false;
        }

        public void AllIn()
        {
            this.CharacterStatus.Text = "All in " + this.Chips;
            this.CallValue = this.Chips;
            this.Chips = 0;
            this.IsInTurn = false;
        }

        private void UpdateChipsTetxBox(int value)
        {
            this.TextBoxChips.Text = "Chips : " + value;
        }
    }
}