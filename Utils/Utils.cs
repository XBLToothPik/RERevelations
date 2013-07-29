using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RERev.Methods;
namespace RERev.Utils
{
    public static class EvilUtils
    {
        public static Enums.Type FileType(Stream xIn)
        {
            int magic = StreamUtils.ReadInt32(xIn, false);

            switch (magic)
            {
                case 0x41524300:
                    return Enums.Type.ARC;
                case 0x474D4400:
                    return Enums.Type.GDM;
                default:
                    return Enums.Type.Neither;
            }
        }
    }
    namespace Enums
    {
        public enum Type
        {
            GDM,
            ARC,
            Neither
        }
    }
}
