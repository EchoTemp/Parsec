﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Parsec.Extensions;
using Parsec.Readers;
using Parsec.Shaiya.Core;

namespace Parsec.Shaiya.EFT
{
    public class EffectSub2 : IBinary
    {
        public float Unknown1 { get; set; }
        public float Unknown2 { get; set; }

        [JsonConstructor]
        public EffectSub2()
        {
        }

        public EffectSub2(SBinaryReader binaryReader)
        {
            Unknown1 = binaryReader.Read<float>();
            Unknown2 = binaryReader.Read<float>();
        }

        public byte[] GetBytes(params object[] options)
        {
            var buffer = new List<byte>();
            buffer.AddRange(Unknown1.GetBytes());
            buffer.AddRange(Unknown2.GetBytes());
            return buffer.ToArray();
        }
    }
}
