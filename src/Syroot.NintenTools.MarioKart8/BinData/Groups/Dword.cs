using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents a 4-byte value which can hold differently typed data.
    /// </summary>
    [DebuggerDisplay(nameof(Dword) + " Int32={Int32}, Single={Single}")]
    [StructLayout(LayoutKind.Explicit)]
    public struct Dword : IConvertible
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        /// <summary>
        /// The data as an <see cref="Int32"/>.
        /// </summary>
        [FieldOffset(0)]
        public Int32 Int32;

        /// <summary>
        /// The data as a <see cref="Single"/>.
        /// </summary>
        [FieldOffset(0)]
        public Single Single;

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        public static implicit operator Dword(int value)
        {
            return new Dword() { Int32 = value };
        }

        public static implicit operator Dword(float value)
        {
            return new Dword() { Single = value };
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Int32;
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Single;
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
