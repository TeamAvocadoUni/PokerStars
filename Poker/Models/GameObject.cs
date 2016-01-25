namespace Poker.Models
{
    using System.Windows.Forms;
    
    public abstract class GameObject : Control
    {
        public GameObject()
        {
            this.Panel = new Panel();
        }
        public Panel Panel { get; set; }
        public Label Status { get; set; }
        public TextBox TextBoxChips { get; set; }
    }
}
