﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Parsec.Readers;
using Parsec.Shaiya.Core;

namespace Parsec.Shaiya.SetItem
{
    public class Synergy : IBinary
    {
        public string Description { get; set; }

        [JsonConstructor]
        public Synergy()
        {
        }

        public Synergy(SBinaryReader binaryReader)
        {
            Description = binaryReader.ReadString();
        }

        public byte[] GetBytes(params object[] options)
        {
            var buffer = new List<byte>();
            buffer.AddRange(BitConverter.GetBytes(Description.Length + 1));
            buffer.AddRange(Encoding.ASCII.GetBytes(Description + '\0'));
            return buffer.ToArray();
        }
    }
}
