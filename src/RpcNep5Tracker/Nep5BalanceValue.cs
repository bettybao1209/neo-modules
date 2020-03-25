using System.IO;
using System.Numerics;
using Neo.IO;

namespace Neo.Plugins
{
    public class Nep5BalanceValue : ICloneable<Nep5BalanceValue>, ISerializable
    {
        public BigInteger Balance;
        public uint LastUpdatedBlock;
        public UInt160 AssetScriptHash;

        int ISerializable.Size => Balance.ToByteArray().GetVarSize() + sizeof(uint) + UInt160.Length;

        public Nep5BalanceValue() : this(new BigInteger(), 0, new UInt160())
        {
        }

        public Nep5BalanceValue(BigInteger balance, uint lastUpdatedBlock, UInt160 assetScriptHash)
        {
            Balance = balance;
            LastUpdatedBlock = lastUpdatedBlock;
            AssetScriptHash = assetScriptHash;
        }

        void ISerializable.Serialize(BinaryWriter writer)
        {
            writer.WriteVarBytes(Balance.ToByteArray());
            writer.Write(LastUpdatedBlock);
            writer.Write(AssetScriptHash);
        }

        void ISerializable.Deserialize(BinaryReader reader)
        {
            Balance = new BigInteger(reader.ReadVarBytes(512));
            LastUpdatedBlock = reader.ReadUInt32();
            ((ISerializable)AssetScriptHash).Deserialize(reader);
        }

        Nep5BalanceValue ICloneable<Nep5BalanceValue>.Clone()
        {
            return new Nep5BalanceValue
            {
                Balance = Balance,
                LastUpdatedBlock = LastUpdatedBlock,
                AssetScriptHash = AssetScriptHash
            };
        }

        public void FromReplica(Nep5BalanceValue replica)
        {
            Balance = replica.Balance;
            LastUpdatedBlock = replica.LastUpdatedBlock;
            AssetScriptHash = replica.AssetScriptHash;
        }
    }
}
