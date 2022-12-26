namespace Coin.Models.Concrete
{
    public class Transaction
    {
        public string FromAdress { get; set; }
        public string ToAdress { get; set; }
        public int Amount { get; set; }

        public Transaction(string fromAdress, string toAdress, int amount)
        {
            FromAdress = fromAdress;
            ToAdress = toAdress;
            Amount = amount;
        }
    }
}
