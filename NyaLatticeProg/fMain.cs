using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProgLink;
using System.IO;

namespace NyaLatticeProg
{
    public partial class fMain : Form
    {
        ProgramWorker Prog = new ProgramWorker();

        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            OnStateChanged(false);
            UpdateState();

            Prog.P.Link.onConnected += Link_onConnected;
            Prog.P.Link.onDisconnected += Link_onDisconnected;
            
            tTimer.Start();
        }
        
        void OnStateChanged(bool Avail)
        {
            if(Avail)
                Text = $"Lattice iCE5LP Programmer [{Prog.P.Link.Name}]";
            else
                Text = "Lattice iCE5LP Programmer";

            bProgram.Enabled = Avail;
            pbProgress.Value = 0;
        }

        private void Link_onDisconnected(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate {
                OnStateChanged(false);
            });
        }

        private void Link_onConnected(object sender, EventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate {
                OnStateChanged(true);
            });
        }

        private void tTimer_Tick(object sender, EventArgs e)
        {
            Prog.P.Link.Tick();
            Prog.Tick();

            pbStatus.BackColor = (Prog.P.Responsed) ? Color.Green : SystemColors.Control;

            lStatus.Text = Prog.Status;
            pbProgress.Value = Convert.ToInt32(pbProgress.Maximum * Prog.Progress / 100);
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            if(dlgOpen.ShowDialog() == DialogResult.OK)
                tFilename.Text = dlgOpen.FileName;
        }

        private void tFilename_TextChanged(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            tFilename.BackColor = (File.Exists(tFilename.Text)) ? Color.LightGreen : Color.LightPink;
        }

        private void bProgram_Click(object sender, EventArgs e)
        {
            if(File.Exists(tFilename.Text))
            {
                var Bitmap = File.ReadAllBytes(tFilename.Text);

                Prog.Program(Bitmap);
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Prog.Cancel();
        }
    }
}
