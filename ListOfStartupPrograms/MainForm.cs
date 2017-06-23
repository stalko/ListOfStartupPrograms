using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ListOfStartupPrograms.StartupPrograms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ListOfStartupPrograms
{
    public partial class MainForm : Form
    {
        List<ProgramDTO> programs;

        public MainForm()
        {
            InitializeComponent();
            progressBarWait.MarqueeAnimationSpeed = 2;
            progressBarWait.Style = ProgressBarStyle.Marquee;

            var bw = new BackgroundWorker();
            bw.DoWork += ExecuteOperations;
            bw.RunWorkerAsync();
        }

        private void ExecuteOperations(object sender, DoWorkEventArgs e)
        {
            var startup = new AllStartupPrograms();
            programs = startup.GetAllPrograms();
            BeginInvoke(new Action(() =>
            {
                listViewPrograms.SmallImageList = new ImageList();
                for (int i = 0; i < programs.Count; i++)
                {
                    string[] lv = new string[] {
                    "",
                    programs[i].Name,
                    programs[i].Command,
                    programs[i].FilePath,
                    programs[i].TypeAutorun.ToString(),
                    programs[i].IsDigitalSignatureExists ? "Yes" : "No",
                    programs[i].IsDigitalSignatureCorrect ? "Yes" : "No",
                    programs[i].CompanyName
                };
                    listViewPrograms.SmallImageList.Images.Add(programs[i].Image);
                    listViewPrograms.Items.Add(new ListViewItem(lv, i));
                }
                progressBarWait.Style = ProgressBarStyle.Continuous;
            }));
        }

        private void listViewPrograms_DoubleClick(object sender, EventArgs e)
        {
            if (listViewPrograms.SelectedItems.Count == 1)
            {
                var id = listViewPrograms.SelectedItems[0].Index;
                if (File.Exists(programs[id].FilePath))
                {
                    Process.Start("explorer.exe", "/select, " + programs[id].FilePath);
                }
                else
                {
                    MessageBox.Show("Файл не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
