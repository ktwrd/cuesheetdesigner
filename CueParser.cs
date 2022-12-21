using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CueSheetDesigner
{
    internal class CueParser
    {
        internal CueData ParseFile(string[] lines)
        {
            var instance = new CueData();
            int currentFileIndex = 0;
            CueTrack currentTrack = null;
            Dictionary<string, string> currentKeyPairs = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                var splitted = line.Trim().Split(
                    new string[] { " " },
                    StringSplitOptions.None);
                switch (splitted[0].ToUpper())
                {
                    case "TRACK":
                        currentKeyPairs["_TRACK_INDEX"] = splitted[1];
                        currentKeyPairs["_TRACK_TYPE"] = splitted[2];
                        instance.AddTrack(new CueTrack());
                        break;
                    case "PERFORMER":
                        currentKeyPairs["PERFORMER"] = ParseValue(line);
                        break;
                    case "TITLE":
                        currentKeyPairs["TITLE"] = ParseValue(line);
                        break;
                    case "FILE":
                        var fileInstance = new CueFile()
                        {
                            Filename = splitted[1].Trim(new char[] { '"' }),
                            FileType = splitted[2]
                        };
                        currentFileIndex = instance.AddFile(fileInstance).Index;
                        break;
                    case "INDEX":
                        currentKeyPairs["_INDEXFILE"] = splitted[1];
                        currentKeyPairs["_INDEXTS"] = splitted[2];
                        break;
                }
                var last = instance.Tracks.LastOrDefault();
                if (last != null)
                {
                    last.Index = int.Parse(GetKey(currentKeyPairs, "_TRACK_INDEX") ?? "1") - 1;
                    last.Type = GetKey(currentKeyPairs, "_TRACK_TYPE");
                    last.Performer = GetKey(currentKeyPairs, "PERFORMER");
                    last.Title = GetKey(currentKeyPairs, "TITLE");
                    last.StartTime = GetKey(currentKeyPairs, "_INDEXTS") ?? "00:00:00";
                    last.FileIndex = int.Parse(GetKey(currentKeyPairs, "_INDEXFILE") ?? "1") - 1;
                    instance.Tracks[last.Index] = last;
                }
            }

            return instance;
        }
        private string GetKey(Dictionary<string, string> dict, string key)
        {
            if (dict.ContainsKey(key))
                return dict[key];
            return null;
        }

        private string ParseValue(string content)
        {
            var full = content.Trim().Split(new string[] { " " }, StringSplitOptions.None).ToList();
            full.RemoveAt(0);
            var str = "";
            var joined = string.Join(" ", full);
            foreach (var c in joined)
            {
                if (c != '"')
                    str += c;
            }
            return str;
        }
    }
}
