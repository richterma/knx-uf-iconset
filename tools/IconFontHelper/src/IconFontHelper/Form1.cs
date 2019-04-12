using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IconFontHelper.Classes;
using IconFontHelper.Properties;

namespace IconFontHelper
{
    public partial class Form1 : Form
    {
        private readonly List<GlyphInfo> _list;
        private int _nextCharId = Settings.Default.FirstGlyphPosition;

        private string _svgDir;
        private string _glyphFile;

        private readonly BackgroundWorker _bgw = new BackgroundWorker();
        private delegate void UpdateProgressDelegate(int progress);

        public Form1()
        {
            InitializeComponent();

            if (!Prepare())
            {
                Close();
            }

            try
            {
                if (File.Exists(_glyphFile))
                {
                    _list = SerializationHelper.DeserializeCollectionFromFile(_glyphFile);

                    foreach (GlyphInfo glyph in _list)
                    {
                        if (_nextCharId <= glyph.CharId)
                        {
                            _nextCharId = glyph.CharId + 1;
                        }
                    }
                }
                else
                {
                    _list = new List<GlyphInfo>();
                }
            }
            catch
            {
                MessageBox.Show(
                    $"Error while reading GlyphList.{Environment.NewLine}Please check your settings file and make sure the file is accessible and properly formatted.");
                Close();
            }

            _bgw.WorkerReportsProgress = true;
            _bgw.DoWork += GenerateBatch;
            _bgw.ProgressChanged += BgwOnProgressChanged;
        }

        private void BgwOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateProgress(e.ProgressPercentage);
        }

        private void UpdateProgress(int progress)
        {
            if (pbActivity.InvokeRequired)
            {
                UpdateProgressDelegate d = UpdateProgress;
                Invoke(d, progress);
            }
            else
            {
                pbActivity.Value = progress;
            }
        }

        private bool Prepare()
        {
            bool result = true;

            if (!File.Exists(Settings.Default.FontForgeExecutable))
            {
                MessageBox.Show(
                    $"FontForge executable not found.{Environment.NewLine}Please make sure that you installed FontForge and configured the path in the settings file.");
                result = false;
            }

            if (_svgDir == null)
            {
                if (string.IsNullOrEmpty(Settings.Default.SvgDirectory))
                {
                    MessageBox.Show(
                        $"SvgDirectory not set.{Environment.NewLine}Please check the settings file.");
                    result = false;
                }

                string path = Path.GetFullPath(Settings.Default.SvgDirectory);
                if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                {
                    MessageBox.Show(
                        $"SvgDirectory not correct.{Environment.NewLine}Please check the settings file.");
                    result = false;
                }
                else
                {
                    _svgDir = path;
                }
            }

            _glyphFile = Path.GetFullPath(Settings.Default.GlyphList);

            return result;
        }

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            foreach (string svg in Directory.GetFiles(_svgDir))
            {
                string filename = Path.GetFileName(svg);

                bool inList = false;
                foreach (GlyphInfo element in _list)
                {
                    if (element.FileName == filename)
                    {
                        inList = true;
                    }
                }

                if (!inList)
                {
                    _list.Add(new GlyphInfo(filename, _nextCharId, ""));
                    _nextCharId++;
                }
            }

            SerializationHelper.SerializeCollectionToFile(_list, _glyphFile);
        }

        private void btnGenerateBatch_Click(object sender, EventArgs e)
        {
            _bgw.RunWorkerAsync();
        }

        private void GenerateBatch(object sender, DoWorkEventArgs e)
        {
            string tmpFile = Path.GetTempFileName();
            Dictionary<string, List<ExtendedGlyphInfo>> dict = new Dictionary<string, List<ExtendedGlyphInfo>>();

            try
            {
                int count = _list.Count;
                int counter = 0;

                StringBuilder builder = new StringBuilder();
                builder.Append(Resources.LoggingHeader);

                foreach (GlyphInfo glyph in _list)
                {
                    string content = string.Format(
                        Resources.LoggingContent,
                        glyph.CharId,
                        $"{_svgDir}\\{glyph.FileName}".Replace("\\", "\\\\"));

                    builder.Append(Environment.NewLine);
                    builder.Append(Environment.NewLine);
                    builder.Append(content);
                }

                File.WriteAllText(tmpFile, builder.ToString());

                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = Settings.Default.FontForgeExecutable,
                        Arguments = $"-lang=ff -script {tmpFile}",
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    };

                    process.Start();
                    while (!process.StandardOutput.EndOfStream)
                    {
                        string line = process.StandardOutput.ReadLine();

                        if (line != null && line.StartsWith("GLYPHDETAILS"))
                        {
                            string[] split = line.Split(';');
                            int charId = int.Parse(split[1]);
                            int deltaX = (int)Math.Round(decimal.Parse(split[2]));
                            int deltaY = (int)Math.Round(decimal.Parse(split[3]));
                            int width = (int)Math.Round(decimal.Parse(split[4]));
                            int factor = (int)Math.Round(decimal.Parse(split[5]));

                            foreach (GlyphInfo glyph in _list)
                            {
                                if (glyph.CharId == charId)
                                {
                                    ExtendedGlyphInfo eGlyph = new ExtendedGlyphInfo(
                                        glyph, deltaX, deltaY, width, factor);

                                    if (!dict.ContainsKey(glyph.GroupId))
                                    {
                                        dict.Add(glyph.GroupId, new List<ExtendedGlyphInfo>());
                                    }

                                    dict[glyph.GroupId].Add(eGlyph);
                                }
                            }

                            counter++;
                            decimal progress = Math.Max(0, counter / (decimal)count * 100m - 5);

                            _bgw.ReportProgress((int)progress);
                        }
                    }

                    process.WaitForExit();

                    foreach (KeyValuePair<string, List<ExtendedGlyphInfo>> kvp in dict)
                    {
                        if (string.IsNullOrWhiteSpace(kvp.Key))
                        {
                            foreach (ExtendedGlyphInfo glyph in kvp.Value)
                            {
                                glyph.Width = 10;
                            }

                            continue;
                        }

                        int deltaX = int.MinValue;
                        int deltaY = int.MaxValue;
                        int width = int.MinValue;
                        int scale = int.MaxValue;

                        foreach (ExtendedGlyphInfo glyph in kvp.Value)
                        {
                            deltaX = Math.Max(deltaX, glyph.DeltaX);
                            deltaY = Math.Min(deltaY, glyph.DeltaY);
                            width = Math.Max(width, glyph.Width);
                            scale = Math.Min(scale, glyph.ScalingFactor);
                        }

                        foreach (ExtendedGlyphInfo glyph in kvp.Value)
                        {
                            glyph.DeltaX = deltaX;
                            glyph.DeltaY = deltaY;
                            glyph.ScalingFactor = scale;

                            if (glyph.Width == width)
                            {
                                glyph.Width = 10;
                            }
                            else
                            {
                                glyph.Width = 10 + width - glyph.Width;
                            }
                        }
                    }

                    StringBuilder batch = new StringBuilder();
                    batch.Append(Resources.BatchHeader);
                    batch.AppendLine("");

                    List<ExtendedGlyphInfo> finalList = new List<ExtendedGlyphInfo>();
                    foreach (KeyValuePair<string, List<ExtendedGlyphInfo>> kvp in dict)
                    {
                        foreach (ExtendedGlyphInfo glyph in kvp.Value)
                        {
                            finalList.Add(glyph);
                        }
                    }

                    foreach (ExtendedGlyphInfo glyph in finalList.OrderBy(g => g.Info.CharId))
                    {
                        batch.AppendLine($"::CALL :UPDATEICON {glyph.Info.CharId} {glyph.Info.FileName} {glyph.DeltaX} {glyph.DeltaY} {glyph.Width} {glyph.ScalingFactor}");
                    }

                    batch.AppendLine("");
                    batch.AppendLine("");
                    batch.AppendLine("");
                    batch.Append(Resources.BatchFooter);

                    string path = Path.GetFullPath(Settings.Default.BatchFile);
                    File.WriteAllText(path, batch.ToString());

                    _bgw.ReportProgress(100);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                File.Delete(tmpFile);
            }
        }
    }
}
