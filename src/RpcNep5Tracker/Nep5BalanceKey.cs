using System;
using System.IO;
using Neo.IO;

namespace Neo.Plugins
{
    public class Nep5BalanceKey : IComparable<Nep5BalanceKey>, IEquatable<Nep5BalanceKey>, ISerializable
    {
        public readonly UInt160 UserScriptHash;
        public readonly int Id;

        public int Size => 20 + sizeof(int);

        public Nep5BalanceKey() : this(new UInt160(), new int())
        {
        }

        public Nep5BalanceKey(UInt160 userScriptHash, int id)
        {
            if (userScriptHash == null)
                throw new ArgumentNullException();
            UserScriptHash = userScriptHash;
            Id = id;
        }

        public int CompareTo(Nep5BalanceKey other)
        {
            if (other is null) return 1;
            if (ReferenceEquals(this, other)) return 0;
            int result = UserScriptHash.CompareTo(other.UserScriptHash);
            if (result != 0) return result;
            return Id.CompareTo(other.Id);
        }

        public bool Equals(Nep5BalanceKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return UserScriptHash.Equals(other.UserScriptHash) && Id.Equals(other.Id);
        }

        public override bool Equals(Object other)
        {
            return other is Nep5BalanceKey otherKey && Equals(otherKey);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = UserScriptHash.GetHashCode();
                hashCode = (hashCode * 397) ^ Id.GetHashCode();
                return hashCode;
            }
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(UserScriptHash);
            writer.Write(Id);
        }

        public void Deserialize(BinaryReader reader)
        {
            UserScriptHash.Deserialize(reader);
            reader.ReadInt32();
        }
    }
}
