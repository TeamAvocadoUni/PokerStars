using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Models
{
    public abstract class Character : GameObject
    {
        protected Character()
        {
            this.Panel = new Panel();
            this.Chips = 10000;
            this.Folded = false;
            this.Call = 0;
            this.Raise = 0;
            this.Power = 0;
        }

        public int Chips { get; set; }

        public double Power { get; set; }

        public bool Turn { get; set; }

        public bool FoldTurn { get; set; }

        public double Type { get; set; }

        public bool Folded { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }
    }
}
