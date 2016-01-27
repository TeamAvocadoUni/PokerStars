namespace Poker.Interfaces
{
    using System.Windows.Forms;

    public interface ICharacter : ICardHolder, IGameAction
    {
        int Id { get; }

        string Name { get; }

        Panel CharacterPanel { get; }

        Type CharacterType { get; }

        Label CharacterStatus { get; }

        TextBox TextBoxChips { get; }

        int Chips { get; set; }

        int CallValue { get; set; }

        int RaiseValue { get; set; }

        bool HasFolded { get; set; }

        bool HasRaised { get; set; }

        bool IsInTurn { get; set; }

        bool FoldTurn { get; set; }

        bool IsInGame { get; }

    }
}
