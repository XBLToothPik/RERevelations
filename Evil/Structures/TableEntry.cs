using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RERev.Methods;
namespace RERev.Arc.Structs
{
    public class TableEntry
    {
        public string Name { get; set; }
        public Int32 Unk1 { get; set; }
        public Int32 FileSize { get; set; }
        public Int32 Unk2 { get; set; }
        public Int32 Offset { get; set; }
        public Stream customStream { get; set; }
        public long writeoffset_offset;
        public TableEntry(Stream xIn)
        {
            Name = StreamUtils.ReadNullString(xIn);
            while (true)
            {
                if (xIn.ReadByte() != 0)
                {
                    xIn.Position--;
                    break;
                }
            }

            Unk1 = StreamUtils.ReadInt32(xIn, true);
            FileSize = StreamUtils.ReadInt32(xIn, true);
            Unk2 = StreamUtils.ReadInt32(xIn, true);
            Offset = StreamUtils.ReadInt32(xIn, true);
        }

        public void Write(Stream xOut)
        {
            StreamUtils.WriteNullString(xOut, Name);

            //Creating the 80 byte entry, don't know if this is needed.
            int len = 80 - (Name.Length + 17);
            StreamUtils.WriteBytes(xOut, new byte[len]);

            StreamUtils.WriteInt32(xOut, Unk1, true);
            StreamUtils.WriteInt32(xOut, (customStream != null) ? (int)customStream.Length : FileSize, true);
            StreamUtils.WriteInt32(xOut, Unk2, true);
            writeoffset_offset = xOut.Position;
            StreamUtils.WriteBytes(xOut, new byte[4]); //Place holder

        }
        public void ExtractToStream(Stream xMain, Stream xOut)
        {
            if (customStream == null)
            {
                xMain.Position = Offset;
                StreamUtils.ReadBufferedStream(xMain, FileSize, xOut);
            }
            else
            {
                customStream.Position = 0;
                StreamUtils.ReadBufferedStream(customStream, (int)customStream.Length, xOut);
            }
        }
    }
}
