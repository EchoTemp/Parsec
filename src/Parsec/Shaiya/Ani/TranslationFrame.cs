﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Parsec.Extensions;
using Parsec.Readers;
using Parsec.Shaiya.Common;
using Parsec.Shaiya.Core;

namespace Parsec.Shaiya.Ani
{
    public class TranslationFrame : IBinary
    {
        public int Keyframe { get; set; }
        public Vector3 Vector { get; set; }

        [JsonConstructor]
        public TranslationFrame()
        {
        }

        public TranslationFrame(SBinaryReader binaryReader)
        {
            Keyframe = binaryReader.Read<int>();
            Vector = new Vector3(binaryReader);
        }

        /// <inheritdoc />
        public byte[] GetBytes(params object[] options)
        {
            var buffer = new List<byte>();
            buffer.AddRange(Keyframe.GetBytes());
            buffer.AddRange(Vector.GetBytes());
            return buffer.ToArray();
        }
    }
}
