using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CueSheetDesigner
{
    public class CueData
    {
        public CueData()
        {
            Tracks = new List<CueTrack>();
            Files = new List<CueFile>();
        }
        public CueTrack AddTrack(CueTrack track)
        {
            return AddTrack(track, Tracks.Count - 1);
        }
        public CueTrack AddTrack(CueTrack track, int index)
        {
            track.Index = index;
            Tracks.Add(track);
            return track;
        }
        public CueFile AddFile(CueFile file)
        {
            file.Index = Files.Count - 1;
            Files.Add(file);
            return file;
        }
        public List<CueTrack> Tracks { get; set; }
        public List<CueFile> Files { get; set; }
        public string Title { get; set; }
        public string Performer { get; set; }
    }
}
