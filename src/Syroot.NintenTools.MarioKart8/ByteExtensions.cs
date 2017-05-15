using System;

namespace Syroot.NintenTools.MarioKart8
{
    /// <summary>
    /// Represents extension methods for <see cref="Byte"/> instances.
    /// </summary>
    internal static class ByteExtensions
    {
        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        /// <summary>
        /// Returns the current byte with the bit at the <paramref name="index"/> set (being 1).
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="index">The 0-based index of the bit to enable.</param>
        /// <returns>The current byte with the bit enabled.</returns>
        internal static byte EnableBit(this byte self, int index)
        {
            return (byte)(self | (1 << index));
        }

        /// <summary>
        /// Returns the current byte with the bit at the <paramref name="index"/> cleared (being 0).
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="index">The 0-based index of the bit to disable.</param>
        /// <returns>The current byte with the bit disabled.</returns>
        internal static byte DisableBit(this byte self, int index)
        {
            return (byte)(self & ~(1 << index));
        }

        /// <summary>
        /// Returns a value indicating whether the bit at the <paramref name="index"/> in the current byte is enabled
        /// or disabled.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="index">The 0-based index of the bit to check.</param>
        /// <returns><c>true</c> when the bit is set; otherwise <c>false</c>.</returns>
        internal static bool GetBit(this byte self, int index)
        {
            return (self & (1 << index)) != 0;
        }

        /// <summary>
        /// Returns the current byte with all bits rotated in the given <paramref name="direction"/>, where positive
        /// directions rotate left and negative directions rotate right.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="direction">The direction in which to rotate, where positive directions rotate left.</param>
        /// <returns>The current byte with the bits rotated.</returns>
        internal static byte RotateBits(this byte self, int direction)
        {
            int bits = sizeof(byte) * 8;
            if (direction > 0)
            {
                return (byte)((self << direction) | (self >> (bits - direction)));
            }
            else if (direction < 0)
            {
                direction = -direction;
                return (byte)((self >> direction) | (self << (bits - direction)));
            }
            return self;
        }

        /// <summary>
        /// Returns the current byte with the bit at the <paramref name="index"/> enabled or disabled, according to
        /// <paramref name="enable"/>.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="index">The 0-based index of the bit to enable or disable.</param>
        /// <param name="enable"><c>true</c> to enable the bit; otherwise <c>false</c>.</param>
        /// <returns>The current byte with the bit enabled or disabled.</returns>
        internal static byte SetBit(this byte self, int index, bool enable)
        {
            if (enable)
            {
                return EnableBit(self, index);
            }
            else
            {
                return DisableBit(self, index);
            }
        }

        /// <summary>
        /// Returns the current byte with the bit at the <paramref name="index"/> enabled when it is disabled or
        /// disabled when it is enabled.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="index">The 0-based index of the bit to toggle.</param>
        /// <returns>The current byte with the bit toggled.</returns>
        internal static byte ToggleBit(this byte self, int index)
        {
            if (GetBit(self, index))
            {
                return DisableBit(self, index);
            }
            else
            {
                return EnableBit(self, index);
            }
        }

        /// <summary>
        /// Returns an <see cref="Byte"/> instance represented by the given number of <paramref name="bits"/>.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="bits">The number of least significant bits which are used to store the <see cref="Byte"/>
        /// value.</param>
        /// <returns>The decoded <see cref="Byte"/>.</returns>
        internal static byte DecodeByte(this byte self, int bits)
        {
            return DecodeByte(self, bits, 0);
        }

        /// <summary>
        /// Returns an <see cref="Byte"/> instance represented by the given number of <paramref name="bits"/>, starting
        /// at the <paramref name="firstBit"/>.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="bits">The number of least significant bits which are used to store the <see cref="Byte"/>
        /// value.</param>
        /// <param name="firstBit">The first bit of the encoded value.</param>
        /// <returns>The decoded <see cref="Byte"/>.</returns>
        internal static byte DecodeByte(this byte self, int bits, int firstBit)
        {
            // Shift to the first bit and keep only the required bits.
            return (byte)((self >> firstBit) & ((1 << bits) - 1));
        }

        /// <summary>
        /// Returns an <see cref="SByte"/> instance represented by the given number of <paramref name="bits"/>.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="bits">The number of least significant bits which are used to store the <see cref="SByte"/>
        /// value.</param>
        /// <returns>The decoded <see cref="SByte"/>.</returns>
        internal static sbyte DecodeSByte(this byte self, int bits)
        {
            return DecodeSByte(self, bits, 0);
        }

        /// <summary>
        /// Returns an <see cref="SByte"/> instance represented by the given number of <paramref name="bits"/>, starting
        /// at the <paramref name="firstBit"/>.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="bits">The number of least significant bits which are used to store the <see cref="SByte"/>
        /// value.</param>
        /// <param name="firstBit">The first bit of the encoded value.</param>
        /// <returns>The decoded <see cref="SByte"/>.</returns>
        internal static sbyte DecodeSByte(this byte self, int bits, int firstBit)
        {
            self >>= firstBit;
            int absMask = 1 << bits;
            byte abs = (byte)(self & (absMask - 1));
            if (abs.GetBit(bits - 1))
            {
                return (sbyte)(abs - absMask);
            }
            else
            {
                return (sbyte)abs;
            }
        }

        /// <summary>
        /// Returns the current byte with the given <paramref name="value"/> set into the given number of
        /// <paramref name="bits"/>.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="value">The value to encode.</param>
        /// <param name="bits">The number of bits which are used to store the <see cref="Byte"/> value.</param>
        /// <returns>The current byte with the value encoded into it.</returns>
        internal static byte Encode(this byte self, byte value, int bits)
        {
            return Encode(self, value, bits, 0);
        }

        /// <summary>
        /// Returns the current byte with the given <paramref name="value"/> set into the given number of
        /// <paramref name="bits"/> starting at <paramref name="firstBit"/>.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="value">The value to encode.</param>
        /// <param name="bits">The number of bits which are used to store the <see cref="Byte"/> value.</param>
        /// <param name="firstBit">The first bit used for the encoded value.</param>
        /// <returns>The current byte with the value encoded into it.</returns>
        internal static byte Encode(this byte self, byte value, int bits, int firstBit)
        {
            // Clear the bits required for the value and fit it into them by truncating.
            int mask = ((1 << bits) - 1) << firstBit;
            self &= (byte)~mask;
            value = (byte)((value << firstBit) & mask);
            
            // Set the value.
            return (byte)(self | value);
        }
        
        /// <summary>
        /// Returns the current byte with the given <paramref name="value"/> set into the given number of
        /// <paramref name="bits"/>.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="value">The value to encode.</param>
        /// <param name="bits">The number of bits which are used to store the <see cref="SByte"/> value.</param>
        /// <returns>The current byte with the value encoded into it.</returns>
        internal static byte Encode(this byte self, sbyte value, int bits)
        {
            return Encode(self, value, bits, 0);
        }

        /// <summary>
        /// Returns the current byte with the given <paramref name="value"/> set into the given number of
        /// <paramref name="bits"/> starting at <paramref name="firstBit"/>.
        /// </summary>
        /// <param name="self">The extended <see cref="Byte"/> instance.</param>
        /// <param name="value">The value to encode.</param>
        /// <param name="bits">The number of bits which are used to store the <see cref="SByte"/> value.</param>
        /// <param name="firstBit">The first bit used for the encoded value.</param>
        /// <returns>The current byte with the value encoded into it.</returns>
        internal static byte Encode(this byte self, sbyte value, int bits, int firstBit)
        {
            // Set the value as a normal byte, but then fix the sign.
            self = Encode(self, (byte)value, bits, firstBit);
            return self.SetBit(bits + firstBit - 1, value < 0);
        }
    }
}
