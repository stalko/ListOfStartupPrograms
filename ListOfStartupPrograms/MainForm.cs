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

namespace ListOfStartupPrograms
{
    public partial class MainForm : Form
    {
        List<ProgramDTO> programs;

        public MainForm()
        {
            InitializeComponent();
            var startup = new AllStartupPrograms();

            programs = startup.GetAllPrograms();
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
            }
        }
    }
}
