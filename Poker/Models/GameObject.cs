namespace Poker.Models
{
    using System.Windows.Forms;
    
    public abstract class GameObject : Control
    {
        public Panel Panel { get; set; }
    }
}
