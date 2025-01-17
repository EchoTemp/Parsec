﻿using System.Collections.Generic;
using Parsec.Extensions;
using Parsec.Readers;
using Parsec.Shaiya.Common;
using Parsec.Shaiya.Core;

namespace Parsec.Shaiya.NpcQuest
{
    public class GateTarget : IBinary
    {
        public short MapId { get; set; }
        public Vector3 Position { get; set; }
        public string TargetName { get; set; }

        /// <summary>
        /// Teleporting gold cost
        /// </summary>
        public int Cost { get; set; }

        public GateTarget(SBinaryReader binaryReader)
        {
            MapId = binaryReader.Read<short>();
            Position = new Vector3(binaryReader);
            TargetName = binaryReader.ReadString(false);
            Cost = binaryReader.Read<int>();
        }

        public byte[] GetBytes(params object[] options)
        {
            var buffer = new List<byte>();

            buffer.AddRange(MapId.GetBytes());
            buffer.AddRange(Position.GetBytes());
            buffer.AddRange(TargetName.GetLengthPrefixedBytes(false));
            buffer.AddRange(Cost.GetBytes());

            return buffer.ToArray();
        }
    }
}
