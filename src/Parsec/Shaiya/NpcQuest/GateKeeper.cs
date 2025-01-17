﻿using System.Collections.Generic;
using Parsec.Readers;

namespace Parsec.Shaiya.NpcQuest
{
    public class GateKeeper : BaseNpc
    {
        public List<GateTarget> GateTargets { get; } = new();

        public GateKeeper(SBinaryReader binaryReader)
        {
            ReadBaseNpcFirstSegment(binaryReader);
            ReadBaseNpcSecondSegment(binaryReader);

            for (int i = 0; i < 3; i++)
            {
                var gateTarget = new GateTarget(binaryReader);
                GateTargets.Add(gateTarget);
            }

            ReadBaseNpcThirdSegment(binaryReader);
        }

        public override byte[] GetBytes(params object[] options)
        {
            var buffer = new List<byte>();

            WriteBaseNpcFirstSegmentBytes(buffer);
            WriteBaseNpcSecondSegmentBytes(buffer);

            foreach (var gateTarget in GateTargets)
                buffer.AddRange(gateTarget.GetBytes());

            WriteBaseNpcThirdSegmentBytes(buffer);

            return buffer.ToArray();
        }
    }
}
