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

namespace LatticeProg
{
    public partial class fMain : Form
    {
        ProgramWorker Prog = new ProgramWorker();
        int Value = 0;

        readonly int[] PWMTable = new int[256] {0   ,   196 ,   197 ,   198 ,   199 ,   200 ,   201 ,   202 ,   203 ,   204 ,   205 ,   210 ,   216 ,   223 ,   232 ,   244 ,
            258    , 275    , 295    , 319    , 347    , 380    , 418    , 461    , 510    ,   566 ,   628 ,   699 ,   777 ,   865 ,   962 ,   1070    ,
            1188   , 1318   , 1460   , 1616   , 1785   , 1969   , 2168   , 2384   , 2618   ,   2869    ,   3140    ,   3430    ,   3742    ,   4075    ,   4432    ,   4812    ,
            5218   , 5649   , 6108   , 6596   , 7113   , 7660   , 8240   , 8852   , 9499   ,   10182   ,   10901   ,   11659   ,   12456   ,   13294   ,   14174   ,   15098   ,
            16067  , 17082  , 18146  , 19258  , 20422  , 21638  , 22909  , 24235  , 25618  ,   27060   ,   28562   ,   30127   ,   31756   ,   33450   ,   35211   ,   37042   ,
            38943  , 40917  , 42965  , 45090  , 47293  , 49576  , 51941  , 54390  , 56925  ,   59548   ,   62261   ,   65066   ,   67965   ,   70960   ,   74053   ,   77246   ,
            80542  , 83942  , 87449  , 91066  , 94793  , 98634  , 102591 , 106666 , 110861 ,   115179  ,   119623  ,   124194  ,   128894  ,   133728  ,   138696  ,   143801  ,
            149046 , 154434 , 159967 , 165647 , 171477 , 177460 , 183599 , 189895 , 196353 ,   202973  ,   209760  ,   216716  ,   223844  ,   231146  ,   238626  ,   246285  ,
            254128 , 262157 , 270375 , 278784 , 287389 , 296191 , 305194 , 314401 , 323815 ,   333438  ,   343275  ,   353328  ,   363600  ,   374095  ,   384816  ,   395765  ,
            406946 , 418363 , 430019 , 441916 , 454059 , 466451 , 479094 , 491993 , 505151 ,   518571  ,   532256  ,   546211  ,   560439  ,   574943  ,   589727  ,   604794  ,
            620148 , 635792 , 651731 , 667968 , 684507 , 701351 , 718503 , 735969 , 753751 ,   771854  ,   790281  ,   809036  ,   828122  ,   847545  ,   867308  ,   887414  ,
            907867 , 928673 , 949834 , 971354 , 993239 , 1015491, 1038115, 1061115, 1084496,   1108260 ,   1132413 ,   1156959 ,   1181902 ,   1207246 ,   1232995 ,   1259155 ,
            1285728, 1312720, 1340135, 1367977, 1396250, 1424960, 1454110, 1483705, 1513750,   1544249 ,   1575207 ,   1606627 ,   1638516 ,   1670877 ,   1703715 ,   1737034 ,
            1770840, 1805138, 1839931, 1875224, 1911023, 1947333, 1984157, 2021501, 2059370,   2097768 ,   2136701 ,   2176174 ,   2216191 ,   2256757 ,   2297878 ,   2339558 ,
            2381803, 2424617, 2468006, 2511975, 2556529, 2601673, 2647412, 2693751, 2740697,   2788253 ,   2836426 ,   2885220 ,   2934641 ,   2984695 ,   3035386 ,   3086720 ,
            3138702, 3191338, 3244634, 3298594, 3353225, 3408531, 3464519, 3521194, 3578562,   3636627 ,   3695397 ,   3754876 ,   3815070 ,   3875985 ,   3937626 ,   4000000 };
    
        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            //tFilename.Text = "C:\\Users\\Alex\\Documents\\Lattice\\fpgadimmer6\\pwm_test\\pwm_test_Implmnt\\sbt\\outputs\\bitmap\\test_bitmap.bin";
            //tFilename.Text = "C:\\Users\\Alex\\Documents\\Lattice\\test\\blinker\\blinker_Implmnt\\sbt\\outputs\\bitmap\\test_bitmap.bin";
            //tFilename.Text = "C:\\Users\\Alex\\Documents\\Lattice\\test\\ledex\\ledex_Implmnt\\sbt\\outputs\\bitmap\\test_bitmap.bin";
            //tFilename.Text = "C:\\Users\\Alex\\Documents\\Lattice\\test\\hx1k_test\\hx1k_test_Implmnt\\sbt\\outputs\\bitmap\\test_bitmap.bin";
            tFilename.Text = "C:\\Users\\Alex\\Documents\\Lattice\\test\\hx1k_dim6\\hx1k_test_Implmnt\\sbt\\outputs\\bitmap\\test_bitmap.bin";
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

            if (!Prog.Busy)
            {
               // byte V = Convert.ToByte(tbValue.Value & 0xFF);
                //byte V = Convert.ToByte(Value & 0xFF);
                // Value += 0x01;
                int Val = PWMTable[tbValue.Value & 0xFF];

                byte HB = Convert.ToByte((Val >> 16) & 0xFF);
                byte MB = Convert.ToByte((Val >> 8) & 0xFF);
                byte LB = Convert.ToByte((Val) & 0xFF);

                Prog.P.Control(new byte[] { HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB,
                                            HB, MB, LB });
            }
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

        private void bTest_Click(object sender, EventArgs e)
        {
            Prog.P.Control(new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 });
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Prog.Cancel();
        }
    }
}
