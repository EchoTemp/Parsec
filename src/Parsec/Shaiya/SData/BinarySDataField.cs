﻿using System.Collections.Generic;
using System.Text;
using Parsec.Extensions;
using Parsec.Readers;
using Parsec.Shaiya.Core;

namespace Parsec.Shaiya.SData
{
    public class BinarySDataField : IBinary
    {
        public string Name { get; set; }

        public BinarySDataField(SBinaryReader binaryReader)
        {
            var length = binaryReader.Read<byte>();
            Name = binaryReader.ReadString(Encoding.Unicode, length);
        }

        public byte[] GetBytes(params object[] options)
        {
            var buffer = new List<byte>();
            buffer.Add((byte)Name.Length);
            buffer.AddRange(Name.GetBytes(Encoding.Unicode));
            return buffer.ToArray();
        }
    }
}
