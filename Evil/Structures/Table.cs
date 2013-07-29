using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RERev.Methods;
namespace RERev.Arc.Structs
{
    public class Table
    {
        public List<TableEntry> Entries;
        public Table(Stream xIn, Header hdr)
        {
            Entries = new List<TableEntry>();
            for (int i = 0; i < hdr.FileCount; i++)
                Entries.Add(new TableEntry(xIn));
        }

        public void Write(Stream xOut, Stream xMain)
        {
            //Writing the TOC Data
            for (int i = 0; i < Entries.Count; i++)
                Entries[i].Write(xOut);

            if (xOut.Position < 32768)
                StreamUtils.WriteBytes(xOut, new byte[32768 - xOut.Position]);

            //Writing the file data
            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].customStream != null)
                {
                    Entries[i].customStream.Position = 0;
                    long xPos = xOut.Position;
                    xOut.Position = Entries[i].writeoffset_offset;
                    StreamUtils.WriteInt32(xOut, (int)xPos, true);
                    xOut.Position = xPos;
                    StreamUtils.ReadBufferedStream(Entries[i].customStream, (int)Entries[i].customStream.Length, xOut);
                }
                else
                {
                    xMain.Position = Entries[i].Offset;

                    long xPos = xOut.Position;
                    xOut.Position = Entries[i].writeoffset_offset;
                    StreamUtils.WriteInt32(xOut, (int)xPos, true);
                    xOut.Position = xPos;

                    StreamUtils.ReadBufferedStream(xMain, Entries[i].FileSize, xOut);

                }
            }
        }
        public TableEntry GetEntryByName(string name)
        {
            for (int i = 0; i < Entries.Count; i++)
                if (Entries[i].Name == name)
                    return Entries[i];
            return null;
        }
    }
}
