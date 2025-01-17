﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parsec.Shaiya.Core;

namespace Parsec.Extensions
{
    public static class BinaryExtensions
    {
        /// <summary>
        /// Serializes a short value (int16) into a byte array
        /// </summary>
        public static byte[] GetBytes(this short value) => BitConverter.GetBytes(value);

        /// <summary>
        /// Serializes an unsigned short value (uint16) into a byte array
        /// </summary>
        public static byte[] GetBytes(this ushort value) => BitConverter.GetBytes(value);

        /// <summary>
        /// Serializes an int value (int32) into a byte array
        /// </summary>
        public static byte[] GetBytes(this int value) => BitConverter.GetBytes(value);

        /// <summary>
        /// Serializes an unsigned int value (uint32) into a byte array
        /// </summary>
        public static byte[] GetBytes(this uint value) => BitConverter.GetBytes(value);

        /// <summary>
        /// Serializes an int value (int64) into a byte array
        /// </summary>
        public static byte[] GetBytes(this long value) => BitConverter.GetBytes(value);

        /// <summary>
        /// Serializes an unsigned int value (uint64) into a byte array
        /// </summary>
        public static byte[] GetBytes(this ulong value) => BitConverter.GetBytes(value);

        /// <summary>
        /// Serializes a float value (single) into a byte array
        /// </summary>
        public static byte[] GetBytes(this float value) => BitConverter.GetBytes(value);

        /// <summary>
        /// Serializes a string prefixed by its length (int32) using the provided encoding
        /// </summary>
        public static byte[] GetLengthPrefixedBytes(
            this string str,
            Encoding encoding,
            bool includeStringTerminator = true
        )
        {
            var buffer = new List<byte>();

            var finalStr = includeStringTerminator ? str + '\0' : str;
            var length = finalStr.Length;

            if (encoding.Equals(Encoding.Unicode))
                length *= 2;

            buffer.AddRange(length.GetBytes());
            buffer.AddRange(finalStr.GetBytes(encoding));

            return buffer.ToArray();
        }

        /// <summary>
        /// Serializes a string prefixed by its length (int32) using ASCII encoding
        /// </summary>
        public static byte[] GetLengthPrefixedBytes(this string str, bool includeStringTerminator = true) =>
            GetLengthPrefixedBytes(str, Encoding.UTF8, includeStringTerminator);

        /// <summary>
        /// Serializes the string into a byte array with UTF8 encoding. It doesn't include a string terminator.
        /// </summary>
        public static byte[] GetBytes(this string str) => GetBytes(str, Encoding.UTF8);

        /// <summary>
        /// Serializes the string into a byte array with the provided encoding. It doesn't include a string terminator.
        /// </summary>
        public static byte[] GetBytes(this string str, Encoding encoding) => encoding.GetBytes(str);

        /// <summary>
        /// Serializes a list of items into a byte array
        /// </summary>
        /// <param name="list"><see cref="IEnumerable{T}"/> of type <see cref="IBinary"/></param>
        /// <param name="lengthPrefixed">Indicates if the data should be prefixed with the array's length (int32) </param>
        public static byte[] GetBytes(this IEnumerable<IBinary> list, bool lengthPrefixed = true)
        {
            var buffer = new List<byte>();

            var enumerable = list as IBinary[] ?? list.ToArray();

            // Add length bytes
            if (lengthPrefixed)
                buffer.AddRange(enumerable.Length.GetBytes());

            // Add item bytes
            foreach (var item in enumerable)
                buffer.AddRange(item.GetBytes());

            return buffer.ToArray();
        }
    }
}
