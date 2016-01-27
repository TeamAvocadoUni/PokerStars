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

        int Chips { get; }

        int CallValue { get; }

        int RaiseValue { get; }

        bool HasFolded { get; }

        bool HasRaised { get; set; }

        bool IsInTurn { get; }

        bool FoldTurn { get; }

        bool IsInGame { get; }

    }
}
