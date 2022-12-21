using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CueSheetDesigner
{
    public class CueFile
    {
        public int Index { get; set; }
        public string FileType { get; set; }
        public string Filename { get; set; }

        public bool Equals(CueFile file)
        {
            return file.FileType == file.Filename;
        }
    }
}
