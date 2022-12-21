using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CueSheetDesigner
{
    public class CueTrack
    {
        public int Index { get; set; }
        public string Type { get; set; }
        public string Performer { get; set; }
        public string Title { get; set; }
        public string StartTime { get; set; }
        public int FileIndex { get; set; }
    }
}
