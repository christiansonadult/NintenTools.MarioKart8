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

        /// <summary>
        /// Converts the given <paramref name="value"/> value to a <see cref="Dword"/> instance.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> value to represent in the new <see cref="Dword"/> instance.
        /// </param>
        public static implicit operator Dword(int value)
        {
            return new Dword() { Int32 = value };
        }

        /// <summary>
        /// Converts the given <paramref name="value"/> value to a <see cref="Dword"/> instance.
        /// </summary>
        /// <param name="value">The <see cref="Single"/> value to represent in the new <see cref="Dword"/> instance.
        /// </param>
        public static implicit operator Dword(float value)
        {
            return new Dword() { Single = value };
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Returns the <see cref="TypeCode"/> for this instance.
        /// </summary>
        /// <returns>The enumerated constant that is the <see cref="TypeCode"/> of the class or value type that
        /// implements this interface.</returns>
        public TypeCode GetTypeCode()
        {
            // These are sadly not flags, so just return the value for Int32 at least.
            return TypeCode.Int32;
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public bool ToBoolean(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {typeof(bool)}.");
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public byte ToByte(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {typeof(byte)}.");
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {typeof(char)}.");
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {typeof(DateTime)}.");
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {typeof(decimal)}.");
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent double-precision floating-point number using the
        /// specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies
        /// culture-specific formatting information.</param>
        /// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
        public double ToDouble(IFormatProvider provider)
        {
            return Single;
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public short ToInt16(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {typeof(short)}.");
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit signed integer using the specified
        /// culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies
        /// culture-specific formatting information.</param>
        /// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
        public int ToInt32(IFormatProvider provider)
        {
            return Int32;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit signed integer using the specified
        /// culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies
        /// culture-specific formatting information.</param>
        /// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
        public long ToInt64(IFormatProvider provider)
        {
            return Int32;
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {nameof(String)}.");
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent single-precision floating-point number using the
        /// specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies
        /// culture-specific formatting information.</param>
        /// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
        public float ToSingle(IFormatProvider provider)
        {
            return Single;
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public string ToString(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {nameof(String)}.");
        }

        /// <summary>
        /// Converts the value of this instance to an <see cref="Object"/> of the specified <see cref="Type"/> that has
        /// an equivalent value, using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="conversionType">The <see cref="Type"/> to which the value of this instance is converted.
        /// </param>
        /// <param name="provider">An <see cref="IFormatProvider"/> interface implementation that supplies
        /// culture-specific formatting information.</param>
        /// <returns>An <see cref="Object"/> instance of type conversionType whose value is equivalent to the value of
        /// this instance.</returns>
        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(Int32) || conversionType == typeof(Int64))
            {
                return Int32;
            }
            else if (conversionType == typeof(Single) || conversionType == typeof(Double))
            {
                return Single;
            }
            else
            {
                throw new ArgumentException($"Cannot convert {nameof(Dword)} to type {conversionType}.");
            }
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {nameof(UInt16)}.");
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public uint ToUInt32(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {nameof(UInt32)}.");
        }

        /// <summary>
        /// This operation is not supported.
        /// </summary>
        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new InvalidOperationException($"Cannot convert {nameof(Dword)} to type {nameof(UInt64)}.");
        }
    }
}
