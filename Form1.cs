using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Bunifu.UI.WinForms;

namespace TerminalCommandApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bunifuProgressBar1.MaximumValue = 100;
            bunifuProgressBar1.MinimumValue = 0;
            bunifuProgressBar1.Value = 0;
            LoadSpotifyPath();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            bunifuProgressBar1.Value = 0;

            string command = "iwr -useb https://raw.githubusercontent.com/spicetify/marketplace/main/resources/install.ps1 | iex";

            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "-Command " + command,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = processInfo };

            Thread progressThread = new Thread(() =>
            {
                process.Start();

                while (!process.HasExited)
                {
                    bunifuProgressBar1.Invoke(new MethodInvoker(delegate
                    {
                        if (bunifuProgressBar1.Value < 100)
                        {
                            bunifuProgressBar1.Value += 1;
                        }
                    }));
                    Thread.Sleep(100);
                }

                bunifuProgressBar1.Invoke(new MethodInvoker(delegate
                {
                    bunifuProgressBar1.Value = 100;
                }));

                OpenSpotify();
            });

            progressThread.Start();
        }

        private void OpenSpotify()
        {
            string spotifyPath = Spotify_Tool.Properties.Settings.Default.SpotifyPath;

            if (string.IsNullOrEmpty(spotifyPath))
            {
                MessageBox.Show("Spotify path not set. Please set the path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Process.Start(spotifyPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open Spotify: " + ex.Message);
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.FileName = "";
            openFileDialog1.DefaultExt = "exe";
            openFileDialog1.Filter = "exe files|*.exe|All files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = openFileDialog1.FileName;
                Spotify_Tool.Properties.Settings.Default.SpotifyPath = selectedPath;
                Spotify_Tool.Properties.Settings.Default.Save();

                MessageBox.Show("Path added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadSpotifyPath()
        {
            string spotifyPath = Spotify_Tool.Properties.Settings.Default.SpotifyPath;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/JohnYoussef-hub/Spotify_Tool/blob/main/README.md");
        }
    }
}
