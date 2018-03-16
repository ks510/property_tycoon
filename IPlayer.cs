namespace PropertyTycoonProject.Assets
{
    public interface IPlayer
    {
        void RollDice();
        void MoveToken(int numberOfSpaces);
        void BuyProperty(IProperty player);
        void SellProperty(IProperty player);
        void BuyHouse(IProperty player);
        void Bid(int amount);
        void PayRent(IProperty);
        void PayFine(int amount);
        void PayJailFine(int amount);
        void ReceiveCash(int amount);
        void DrawCard(bool potLuck);
        bool InJail();
        bool PassedFirstGo();

        Token GetToken();
        bool FinishedTurn();
        int DoublesRolled();
        int GetTotalValue();
        int GetCurrentSpace();
        void SetBankrupt(bool bankrupt);
        bool IsBankrupt();

    }
}