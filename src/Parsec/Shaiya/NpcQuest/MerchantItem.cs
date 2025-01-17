﻿using Parsec.Readers;
using Parsec.Shaiya.Core;

namespace Parsec.Shaiya.NpcQuest
{
    public class MerchantItem : IBinary
    {
        /// <summary>
        /// Item Type
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// Item TypeId
        /// </summary>
        public byte TypeId { get; set; }

        public MerchantItem(SBinaryReader binaryReader)
        {
            Type = binaryReader.Read<byte>();
            TypeId = binaryReader.Read<byte>();
        }

        public byte[] GetBytes(params object[] options) => new[]
        {
            Type, TypeId
        };
    }
}
