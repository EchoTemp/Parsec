﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Parsec.Readers;
using Parsec.Shaiya.Core;

namespace Parsec.Shaiya.DUALLAYERCLOTHES
{
    public class Costume : IBinary
    {
        public short Index { get; set; }

        public List<Layer> Layers { get; } = new();

        [JsonConstructor]
        public Costume()
        {
        }

        public Costume(ShaiyaBinaryReader binaryReader)
        {
            Index = binaryReader.Read<short>();

            var layer = new Layer(binaryReader);
            Layers.Add(layer);
        }

        public byte[] GetBytes()
        {
            var buffer = new List<byte>();

            buffer.AddRange(BitConverter.GetBytes(Index));

            foreach (var layer in Layers)
            {
                buffer.AddRange(layer.GetBytes());
            }

            return buffer.ToArray();
        }
    }
}
