using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RERev.Utils;
using RERev.Arc.Structs;
namespace RERev.Arc
{
        public class ArcFile
        {
            public Header Header { get; set; }
            public Table Table { get; set; }
            private Stream xMain;
            public ArcFile(Stream xIn)
            {
                xMain = xIn;
                this.Header = new Header(xIn);
                this.Table = new Table(xIn, Header);
            }
            public void Save(Stream xOut)
            {
                Header.FileCount = (short)Table.Entries.Count;

                Header.Write(xOut);
                Table.Write(xOut, xMain);
            }
        }
}
