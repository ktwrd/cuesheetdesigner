using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CueSheetDesigner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ReloadListView(CueData data)
        {
            listView1.Items.Clear();
            foreach (var item in data.Tracks)
            {
                var duration = CalculateDuration(data, item);
                listView1.Items.Add(new ListViewItem(new string[]
                {
                    item.Index.ToString(),
                    item.Type.ToString(),
                    item.Performer.ToString(),
                    item.Title.ToString(),
                    item.StartTime.ToString(),
                    duration,
                    item.FileIndex.ToString()
                }));
            }
        }
        private string CalculateDuration(CueData data, CueTrack track)
        {
            var next = data.Tracks.Count > track.Index + 1 ? data.Tracks[track.Index + 1] : null;
            if (next == null)
                return "End";
            var startTimestamp = TimestampToSpan(track.StartTime);
            TimeSpan endTimestamp = TimestampToSpan(next.StartTime);
            var diff = endTimestamp - startTimestamp;
            return $"{Math.Floor(diff.TotalMinutes)}:{diff.Seconds}:{Math.Round((diff.Milliseconds / 1000f) * 75)}";
        }
        private TimeSpan TimestampToSpan(string ts)
        {
            var splitted = ts.Split(':');
            var min = int.Parse(splitted[0]);
            var sec = int.Parse(splitted[1]);
            var ms = (int)Math.Round((int.Parse(splitted[2]) / 75f) * 100f);
            var timeSpan = new TimeSpan(0, 0, min, sec, ms);
            return timeSpan;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var parser = new CueParser();
                var parsed = parser.ParseFile(File.ReadAllLines(openFileDialog1.FileName));
                ReloadListView(parsed);
            }
        }
    }
}
