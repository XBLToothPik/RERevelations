using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RERev.Utils;
namespace RERev.Arc.Structs
{
    public class Header
    {
        public short Version { get; set; }
        public short FileCount { get; set; }
        public const int Magic = 0x00435241;
        public Header(Stream xIn)
        {
            StreamUtils.ReadInt32(xIn, true);
            Version = StreamUtils.ReadInt16(xIn, true);
            FileCount = StreamUtils.ReadInt16(xIn, true);
        }
        public void Write(Stream xOut)
        {
            StreamUtils.WriteInt32(xOut, Magic, true);
            StreamUtils.WriteInt16(xOut, Version, true);
            StreamUtils.WriteInt16(xOut, FileCount, true);
        }
    }
}
