using KeyboardHookManager;
using Memory;
using System;
using System.Drawing;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace KIKI_ReturnSaiyans
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public const uint WM_KEYDOWN = 0x100;
        public const uint WM_KEYUP = 0x101;
        public const uint WM_KEYDOWN1 = 0x104;
        public const uint WM_KEYUP1 = 0x105;
        public const uint WM_CHAR = 0x102;
        public const uint WM_SYSCOMMAND = 0x018;
        public const uint SC_CLOSE = 0x053;

        public char KeyChar { get; set; }

        Mem meme = new Mem();
        public static string Address_manaA = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,5F8";
        public static string Address_manaT = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,600";
        public static string Address_vidaA = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,5A8";
        public static string Address_vidaT = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,5B0";
        public static string Address_TempoPZ = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,640";
        public static string Address_TempoFood = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,638";
        public static string Address_XPTOTAL = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,5D8";
        public static string Address_level = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,5E0";
        public static string Address_TreinoOffRestante = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,648";
        public static string Address_mlT = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,608";
        public static string Address_mlA = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,620";
        public static string Address_mlPorcento = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,618";
        public static string Address_capAtual = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,5C8";
        public static string Address_capTotal = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,5D0";
        public static string Address_regenMana = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,670";
        public static string Address_regenVida = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,660";
        public static string Address_barraAmarela = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,650";
        public static string Address_Luz = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,B0";
        public static string Address_playerX = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,C";
        public static string Address_playerY = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,10";
        public static string Address_playerZ = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,14";
        public static string Address_playerViradoPara = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,48";
        public static string Address_playerGoX = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,424";
        public static string Address_playerGoY = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,434";
        public static string Address_playerGoZ = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,420";
        public static string Address_playerIsWalk = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,314";
        public static string Address_playerTargetIDbase = "ReturnOfTheSaiyans-opengl.exe+0x05CD1F8,14";
        public static string Address_playerTargetID = Address_playerTargetIDbase + ",4";
        public static string Address_playerName = "ReturnOfTheSaiyans-opengl.exe+0x05CE0F0,24";


        public string playerName;
        public double manaPorcento;
        public double manaAtual;
        public double manaTotal;
        public double vidaPorcento;
        public double vidaAtual;
        public double vidaTotal;
        public double TempoPZ;
        public double TempoFood;
        public double XPTOTAL;
        public double level;
        public double TreinoOffRestante;
        public double mlT;
        public double mlA;
        public double mlPorcento;
        public double capAtual;
        public double capTotal;
        public double regenMana;
        public double regenVida;
        public double barraAmarela;
        public int playerX;
        int xNoMove;
        int yNoMove;
        int zNoMove;
        public int playerY;
        public int playerZ;
        public int playerGoX;
        public int playerGoY;
        public int playerGoZ;
        public int playerIsWalk;
        public int playerViradoPara;
        int Light;
#pragma warning disable CS0169 // O campo "Form1.playerTargetID" nunca é usado
        int playerTargetID;
#pragma warning restore CS0169 // O campo "Form1.playerTargetID" nunca é usado

        public string gameProcessoOpengl = "ReturnOfTheSaiyans-opengl";
        public string gameProcessoDirectx = "ReturnOfTheSaiyans-directx";
        config config = new config();

        IntPtr janelaROTS;
        int PID_1;
        int PID_2;
        int deslogou;

        Keys HTKcuraMagia;
        Keys HTKcuraVidaSenzu;
        Keys HTKcuraKISenzu;
        Keys HTKtreino1;
        Keys HTKtreino2;
        Keys HTKtreino3;
        Keys HTKtreino4;
        Keys HTKtreino5;
        Keys HTKcomerFood;
        Keys HTKatacar;
        Keys HTKpausarBOT;
        Keys HTKativarBOT;
        public Form1()
        {
            InitializeComponent();
            HookManager.KeyDown += HookManager_KeyDown;
            HookManager.KeyPress += HookManager_KeyPress;
            HookManager.KeyUp += HookManager_KeyUp;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            HTKcuraMagia = config.HTKcuraMagia;
            HTKcuraVidaSenzu = config.HTKcuraVidaSenzu;
            HTKcuraKISenzu = config.HTKcuraVidaSenzu;
            HTKtreino1 = config.HTKtreino1;
            HTKtreino2 = config.HTKtreino2;
            HTKtreino3 = config.HTKtreino3;
            HTKtreino4 = config.HTKtreino4;
            HTKtreino5 = config.HTKtreino5;
            HTKcomerFood = config.HTKcomerFood;
            HTKatacar = config.HTKatacar;
            HTKativarBOT = config.HTKativarBOT;
            HTKpausarBOT = config.HTKpausarBOT;

            textBox1.Text = config.HTKcuraMagia.ToString();
            textBox2.Text = config.HTKcuraVidaSenzu.ToString();
            textBox3.Text = config.HTKcuraKISenzu.ToString();
            textBox4.Text = config.HTKtreino1.ToString();
            textBox5.Text = config.HTKtreino2.ToString();
            textBox6.Text = config.HTKtreino3.ToString();
            textBox7.Text = config.HTKcomerFood.ToString();
            textBox8.Text = config.HTKatacar.ToString();
            textBox9.Text = config.HTKtreino4.ToString();
            textBox10.Text = config.HTKtreino5.ToString();
            textBox11.Text = config.HTKpausarBOT.ToString();
            textBox12.Text = config.HTKativarBOT.ToString();
            progressBar1.ForeColor = Color.Red;
        }

        private void HookManager_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        private void HookManager_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        private void HookManager_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == HTKpausarBOT)
            {
                checkCuraVidaMagia.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkAttack.Checked = false;
                checkTreinarKI.Checked = false;
            }
            if (e.KeyCode == HTKativarBOT)
            {
                checkCuraVidaMagia.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
            }
        }

        #region TIMERS 

        private void timerAttackSeAtacado_Tick(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {

                if (janelaROTS != IntPtr.Zero)
                {
                    if (TempoPZ >= Convert.ToDouble("28"))
                    {
                        SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)Keys.P), (IntPtr)0);
                        SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)Keys.P), (IntPtr)0);
                    }
                }

            }
        }
        private void timerAttack_Tick(object sender, EventArgs e)
        {
            if (checkAttack.Checked)
            {
                if (janelaROTS != IntPtr.Zero)
                {
                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKatacar), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKatacar), (IntPtr)0);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            PID_1 = meme.GetProcIdFromName(gameProcessoOpengl);
            PID_2 = meme.GetProcIdFromName(gameProcessoDirectx);
            if (PID_1 > 0 || PID_2 > 0)
            {
                if (PID_1 > 0)
                {
                    meme.OpenProcess(PID_1);
                    label20.Visible = false;
                    janelaROTS = FindWindow(null, "Return Of The Saiyans");
                }
                if (PID_2 > 0)
                {
                    meme.OpenProcess(PID_2);
                    label20.Visible = true;
                }

                bool success = true;
                try
                {
                    vidaAtual = meme.ReadDouble(Address_vidaA);
                    vidaTotal = meme.ReadDouble(Address_vidaT);
                    manaAtual = meme.ReadDouble(Address_manaA);
                    manaTotal = meme.ReadDouble(Address_manaT);
                    level = meme.ReadDouble(Address_level);
                    mlT = meme.ReadDouble(Address_mlT);
                    mlA = meme.ReadDouble(Address_mlA);
                    mlPorcento = meme.ReadDouble(Address_mlPorcento);
                    TreinoOffRestante = meme.ReadDouble(Address_TreinoOffRestante);
                    capAtual = meme.ReadDouble(Address_capAtual);
                    capTotal = meme.ReadDouble(Address_capTotal);
                    regenMana = meme.ReadDouble(Address_regenMana);
                    regenVida = meme.ReadDouble(Address_regenVida);
                    TempoFood = meme.ReadDouble(Address_TempoFood);
                    TempoPZ = meme.ReadDouble(Address_TempoPZ);
                    barraAmarela = meme.ReadDouble(Address_barraAmarela);
                    playerX = meme.ReadInt(Address_playerX);
                    playerY = meme.ReadInt(Address_playerY);
                    playerZ = meme.ReadInt(Address_playerZ);
                    Light = meme.ReadInt(Address_Luz);
                    playerName = meme.ReadString(Address_playerName);
                    //playerTargetID = meme.ReadInt(Address_playerTargetID);

                    if (vidaAtual >= 0)
                    {
                        var calcPorcentoVida = vidaAtual / vidaTotal;
                        vidaPorcento = calcPorcentoVida * 100;
                        var calcPorcentoMana = manaAtual / manaTotal;
                        manaPorcento = calcPorcentoMana * 100;

                        if (checkBox5.Checked == true)
                        {
                            timerCaminharTreino.Enabled = true;
                        }
                        else
                        {
                            timerCaminharTreino.Enabled = false;
                        }

                        lblVida.Text = vidaAtual.ToString() + "/" + vidaTotal.ToString();
                        lblVidaPorcentagem.Text = "VIDA: " + (vidaPorcento).ToString("F") + "%";
                        lblMana.Text = manaAtual.ToString() + "/" + manaTotal.ToString();
                        lblManaPorcentagem.Text = "KI: " + (manaPorcento).ToString("F") + "%";
                        label10.Text = "Level: " + (level).ToString() + ".";
                        label11.Text = "Focus: " + (mlT).ToString() + " (" + (mlA).ToString() + ") " + mlPorcento.ToString() + "% para o "+(mlA+1).ToString()+".";
                        label12.Text = "Stamina de Treino: " + (TreinoOffRestante).ToString() + "s.";
                        label13.Text = "Cap restante: " + (capAtual).ToString() + " de " + capTotal+".";
                        label14.Text = "Regen KI: " + (regenMana).ToString() + "/s";
                        label15.Text = "Regen Vida: " + (regenVida).ToString() + "/s";
                        label3.Text = "Tempo de food restante: " + (TempoFood).ToString() + "s";
                        label17.Text = "Tempo de PZ restante: " + (TempoPZ).ToString() + "s";
                        label23.Text = "X: " + (playerX).ToString() + ", Y: " + (playerY).ToString() + ", Z: " + (playerZ).ToString();
                        //  label28.Text = "Target: " + (playerTargetID).ToString() + "";
                        //  label16.Text = "Barra Amarela: " + (barraAmarela).ToString() + "";
                        progressBar1.Maximum = 100;
                        progressBar1.Value = Convert.ToInt32(vidaPorcento);
                        progressBar2.Maximum = 100;
                        progressBar2.Value = Convert.ToInt32(manaPorcento);

                       



                      

                    } else
                    {
                       
                    }
                    if (level <= 0)
                    {
                        deslogou = 1;
                        this.Text = "KIKI -ROTS";
                    } else
                    {
                        this.Text = "KIKI -ROTS- " + playerName;
                    }
                }
                catch
                {
                    if (deslogou == 1 && (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked))
                    {

                        checkCuraVidaMagia.Checked = false;
                        checkBox2.Checked = false;
                        checkBox3.Checked = false;
                        checkBox4.Checked = false;
                        checkBox5.Checked = false;
                        checkBox7.Checked = false;
                        checkBox8.Checked = false;
                        checkBox9.Checked = false;
                        checkAttack.Checked = false;
                        checkTreinarKI.Checked = false;

                        checkCuraVidaMagia.Checked = true;
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        deslogou = 0;
                    }
                    if (deslogou == 1)
                    {

                        checkCuraVidaMagia.Checked = false;
                        checkBox2.Checked = false;
                        checkBox3.Checked = false;
                        checkBox4.Checked = false;
                        deslogou = 0;
                    }
                }
                finally
                {
                    
                }


            }
        }

        private void timerCaminharTreino_Tick_1(object sender, EventArgs e)
        {
            try
            {
                label16.Text = label16.Text = "Treino: Aguardando barra";

                if (barraAmarela > 2)
                {

                    Random randNum = new Random();
                    if (janelaROTS != IntPtr.Zero)
                    {
                        int dir = randNum.Next(1, 5);

                        if (dir == 1)
                        {
                            label16.Text = "Caminhar para o NORTE";
                            SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)Keys.W), (IntPtr)0);
                            SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)Keys.W), (IntPtr)0);
                        }
                        if (dir == 2)
                        {
                            label16.Text = "Caminhar para o SUL";
                            SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)Keys.S), (IntPtr)0);
                            SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)Keys.S), (IntPtr)0);
                        }
                        if (dir == 3)
                        {
                            label16.Text = "Caminhar para o OESTE";
                            SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)Keys.A), (IntPtr)0);
                            SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)Keys.A), (IntPtr)0);
                        }
                        if (dir == 4)
                        {
                            label16.Text = "Caminhar para o LESTE";
                            SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)Keys.D), (IntPtr)0);
                            SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)Keys.D), (IntPtr)0);
                        }
                        if (dir == 5)
                        {}
                    }
                }
                else
                {
                    label16.Text = label16.Text = "Treino: Aguardando barra";
                }
            }
            catch{}
        }
        private void timerTreinaMana_Tick(object sender, EventArgs e)
        {
            try
            {
                decimal manaParaTreinar = numericUpDown1.Value;
                if (Convert.ToDecimal(manaPorcento) >= manaParaTreinar)
                {
                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino1), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino1), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino2), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino2), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino3), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino3), (IntPtr)0);
                }
            }
            catch{}
        }

        private void timerCurarVidaSenzu_Tick(object sender, EventArgs e)
        {
            try
            {
                decimal vidaComSenzu = numericUpDown3.Value;
                if (Convert.ToDecimal(vidaPorcento) <= vidaComSenzu)
                {
                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKcuraVidaSenzu), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKcuraVidaSenzu), (IntPtr)0);
                }
            }
            catch{}
        }

        private void timerCurarKISenzu_Tick(object sender, EventArgs e)
        {
            try
            {
                decimal manaComSenzu = numericUpDown4.Value;
                if (Convert.ToDecimal(manaPorcento) <= manaComSenzu)
                {
                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKcuraKISenzu), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKcuraKISenzu), (IntPtr)0);
                }
            }
            catch{}
        }

        private void timerCurarVidaMagia_Tick(object sender, EventArgs e)
        {
            try
            {
                decimal vidaComMagia = numericUpDown2.Value;
                if (Convert.ToDecimal(vidaPorcento) <= vidaComMagia)
                {
                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKcuraMagia), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKcuraMagia), (IntPtr)0);
                }
            }
            catch{}
        }

        private void timerComerFood_Tick(object sender, EventArgs e)
        {
            try
            {
                if (TempoFood <= 60)
                {
                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKcomerFood), (IntPtr)0);
                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKcomerFood), (IntPtr)0);
                }
            }
            catch{}
        }

        private void timerLuz_Tick(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                if (Light < 55000)
                {
                    meme.WriteMemory(Address_Luz, "int", "56160");
                }
            }
        }
        #endregion

        #region CHECKBOX

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                xNoMove = playerX;
                yNoMove = playerY;
                zNoMove = playerZ;
                Thread WA = new Thread(alertaMove) { IsBackground = true };
                WA.Start();
            }
        }
        private void checkCuraVidaMagia_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCuraVidaMagia.Checked)
            {
                Thread WA = new Thread(curarMagia) { IsBackground = true };
                WA.Start();
            }
        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                Thread WA = new Thread(alertaVida) { IsBackground = true };
                WA.Start();
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                timerAttackSeAtacado.Enabled = true;
            }
            else
            {
                timerAttackSeAtacado.Enabled = false;
            }
        }
        private void checkAttack_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAttack.Checked)
            {
                timerAttack.Enabled = true;
            }
            else
            {
                timerAttack.Enabled = false;
            }
        }
        private void checkTreinarKI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTreinarKI.Checked)
            {
                checkBox9.Visible = true;
                Thread WA = new Thread(treinarKI) { IsBackground = true };
                WA.Start();
            }
            else
            {
                checkBox9.Visible = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Thread WA = new Thread(curarHPSenzu) { IsBackground = true };
                WA.Start();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                Thread WA = new Thread(curarKISenzu) { IsBackground = true };
                WA.Start();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                timerComerFood.Enabled = true;
            }
            else
            {
                timerComerFood.Enabled = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                MessageBox.Show("Lembre-se que o chat deve estar OFF");
                timerCaminharTreino.Enabled = true;
            }
            else
            {
                label16.Text = label16.Text = "Treino: OFF";
                timerCaminharTreino.Enabled = false;
            }
        }


        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                timerLuz.Enabled = true;
            }
            else
            {
                timerLuz.Enabled = false;
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            Thread WA = new Thread(alertaCap) { IsBackground = true };
            WA.Start();
        }
        #endregion

        #region THREADS

        private void alertaVida()
        {
            try
            {
                while (true)
                {
                    if (checkBox7.Checked)
                    {
                        decimal vidaMenorQue = numericUpDown5.Value;
                        if (Convert.ToDecimal(vidaPorcento) <= vidaMenorQue)
                        {
                            SoundPlayer Player = new SoundPlayer();
                            Player.SoundLocation = "beep.wav";
                            Player.Play();
                            Thread.Sleep(600);
                        }

                        Thread.Sleep(600);
                    }
                    Thread.Sleep(600);
                }
            }
            catch

            {

            }
        }
        private void alertaMove()
        {
            try
            {
                while (true)
                {
                    if (checkBox1.Checked)
                    {
                        if (xNoMove != playerX || yNoMove != playerY || zNoMove != playerZ)
                        {
                            SoundPlayer Player = new SoundPlayer();
                            Player.SoundLocation = "beep2.wav";
                            Player.Play();
                            Thread.Sleep(600);
                        }
                        Thread.Sleep(600);
                    }
                    Thread.Sleep(600);
                }
            }
            catch{}
        }

        private void alertaCap()
        {
            try
            {
                while (true)
                {
                    if (checkBox10.Checked)
                    {
                        if (capAtual < 50 && level >0)
                        {
                            SoundPlayer Player = new SoundPlayer();
                            Player.SoundLocation = "beep2.wav";
                            Player.Play();
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(1000);
                }
            }
            catch { }
        }

        private void curarMagia()
        {
            try
            {
                while (true)
                {
                    if (checkCuraVidaMagia.Checked == true)
                    {
                        decimal vidaComMagia = numericUpDown2.Value;
                        if (Convert.ToDecimal(vidaPorcento) <= vidaComMagia)
                        {
                            SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKcuraMagia), (IntPtr)0);
                            SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKcuraMagia), (IntPtr)0);
                            Thread.Sleep(100);
                        }
                        Thread.Sleep(100);
                    }
                }
            }
            catch{}
        }

        private void curarHPSenzu()
        {
            try
            {
                while (true)
                {
                    if (checkBox2.Checked)
                    {
                        decimal vidaComSenzu = numericUpDown3.Value;
                        if (Convert.ToDecimal(vidaPorcento) <= vidaComSenzu)
                        {
                            SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKcuraVidaSenzu), (IntPtr)0);
                            SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKcuraVidaSenzu), (IntPtr)0);
                        }
                        Thread.Sleep(1);
                    }
                    Thread.Sleep(1);
                }
            }
            catch{}
        }

        private void curarKISenzu()
        {
            try
            {
                while (true)
                {
                    if (checkBox3.Checked)
                    {
                        decimal manaComSenzu = numericUpDown4.Value;
                        if (Convert.ToDecimal(manaPorcento) <= manaComSenzu)
                        {
                            SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKcuraKISenzu), (IntPtr)0);
                            SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKcuraKISenzu), (IntPtr)0);
                        }
                        Thread.Sleep(1);
                    }
                    Thread.Sleep(1);
                }
            }
            catch{}
        }

        private void comerFood()
        {
            try
            {
                while (true)
                {
                    if (checkBox4.Checked)
                    {
                        if (TempoFood <= 60)
                        {
                            SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKcomerFood), (IntPtr)0);
                            SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKcomerFood), (IntPtr)0);
                        }
                        Thread.Sleep(1);
                    }
                    Thread.Sleep(1);
                }
            }
            catch{}
        }

        private void treinarKI()
        {
            try
            {
                while (true)
                {
                    if (checkTreinarKI.Checked)
                    {
                        decimal manaParaTreinar = numericUpDown1.Value;
                        if (Convert.ToDecimal(manaPorcento) >= manaParaTreinar)
                        {
                            if (checkBox9.Checked == false)
                            {
                                if (TempoPZ >= Convert.ToDouble("29"))
                                {
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino1), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino1), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino2), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino2), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino3), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino3), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino4), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino4), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino5), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino5), (IntPtr)0);
                                }
                            }
                                else
                                {
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino1), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino1), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino2), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino2), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino3), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino3), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino4), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino4), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYDOWN, ((IntPtr)HTKtreino5), (IntPtr)0);
                                    SendMessage(janelaROTS, WM_KEYUP, ((IntPtr)HTKtreino5), (IntPtr)0);
                                }
                        }
                        Thread.Sleep(100);
                    }
                    Thread.Sleep(100);
                }
            }
            catch{}
        }
        #endregion
        
        #region BOTÕES
        /*  public bool Attack()
          {
              uint creatureId = Id;
              client.Player.TargetId = creatureId;
              return Packets.Outgoing.AttackPacket.Send(client, (uint)creatureId);
          }*/
        private void button1_Click(object sender, EventArgs e)
        {
 
        }

        #endregion

        #region TEXTBOX
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox1.Text = e.KeyData.ToString();
            HTKcuraMagia = e.KeyData;
            config.HTKcuraMagia = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            textBox2.ReadOnly = true;
            textBox2.Text = e.KeyData.ToString();
            HTKcuraVidaSenzu = e.KeyData;
            config.HTKcuraVidaSenzu = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            textBox3.ReadOnly = true;
            textBox3.Text = e.KeyData.ToString();
            HTKcuraKISenzu = e.KeyData;
            config.HTKcuraKISenzu = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            textBox4.ReadOnly = true;
            textBox4.Text = e.KeyData.ToString();
            HTKtreino1 = e.KeyData;
            config.HTKtreino1 = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            textBox5.ReadOnly = true;
            textBox5.Text = e.KeyData.ToString();
            HTKtreino2 = e.KeyData;
            config.HTKtreino2 = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            textBox6.ReadOnly = true;
            textBox6.Text = e.KeyData.ToString();
            HTKtreino3 = e.KeyData;
            config.HTKtreino3 = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            textBox7.ReadOnly = true;
            textBox7.Text = e.KeyData.ToString();
            HTKcomerFood = e.KeyData;
            config.HTKcomerFood = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            textBox8.ReadOnly = true;
            textBox8.Text = e.KeyData.ToString();
            HTKatacar = e.KeyData;
            config.HTKatacar = e.KeyData;
            config.Save();
            this.ActiveControl = null;
            MessageBox.Show("Lembre-se que o chat deve estar OFF");

        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            textBox9.ReadOnly = true;
            textBox9.Text = e.KeyData.ToString();
            HTKtreino4 = e.KeyData;
            config.HTKtreino4 = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            textBox10.ReadOnly = true;
            textBox10.Text = e.KeyData.ToString();
            HTKtreino5 = e.KeyData;
            config.HTKtreino5 = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            textBox11.ReadOnly = true;
            textBox11.Text = e.KeyData.ToString();
            HTKpausarBOT = e.KeyData;
            config.HTKpausarBOT = e.KeyData;
            config.Save();
            this.ActiveControl = null;

        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            textBox12.ReadOnly = true;
            textBox12.Text = e.KeyData.ToString();
            HTKativarBOT = e.KeyData;
            config.HTKativarBOT = e.KeyData;
            config.Save();
            this.ActiveControl = null;
        }

        #endregion


    }
}
