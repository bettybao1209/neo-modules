
namespace Neo.Plugins
{
    public class TransactionInfo
    {
        public int TransactionID { get; set; }
        public string TxHash { get; set; }
        public string ToAddress { get; set; }
        public uint Amount { get; set; }
    }
}
