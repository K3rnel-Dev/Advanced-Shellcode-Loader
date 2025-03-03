
namespace Builder
{
    partial class CMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMain));
            this.MinimazeBtn = new System.Windows.Forms.PictureBox();
            this.CloseBtn = new System.Windows.Forms.PictureBox();
            this.IconBox = new System.Windows.Forms.PictureBox();
            this.ShellcodeFile = new System.Windows.Forms.TextBox();
            this.XorKeys = new System.Windows.Forms.TextBox();
            this.BuildBtn = new System.Windows.Forms.Button();
            this.SelectFileBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GenerateInit = new System.Windows.Forms.Timer(this.components);
            this.arch86 = new System.Windows.Forms.RadioButton();
            this.arch64 = new System.Windows.Forms.RadioButton();
            this.SelectIconBtn = new System.Windows.Forms.Button();
            this.AssembyCloneBtn = new System.Windows.Forms.Button();
            this.ResetBtn = new System.Windows.Forms.Button();
            this.UseObfuscate = new System.Windows.Forms.CheckBox();
            this.ProcessInjectCmb = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.AboutShowBtn = new System.Windows.Forms.Button();
            this.AboutBtn = new System.Windows.Forms.Label();
            this.AutorunChk = new System.Windows.Forms.CheckBox();
            this.CompressChk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MinimazeBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MinimazeBtn
            // 
            this.MinimazeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimazeBtn.Image = global::Builder.Properties.Resources.expand;
            this.MinimazeBtn.Location = new System.Drawing.Point(32, 5);
            this.MinimazeBtn.Name = "MinimazeBtn";
            this.MinimazeBtn.Size = new System.Drawing.Size(21, 17);
            this.MinimazeBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.MinimazeBtn.TabIndex = 3;
            this.MinimazeBtn.TabStop = false;
            this.MinimazeBtn.Click += new System.EventHandler(this.MinimazeBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.Image = global::Builder.Properties.Resources.close;
            this.CloseBtn.Location = new System.Drawing.Point(8, 5);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(21, 17);
            this.CloseBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CloseBtn.TabIndex = 2;
            this.CloseBtn.TabStop = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // IconBox
            // 
            this.IconBox.BackColor = System.Drawing.Color.Transparent;
            this.IconBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IconBox.Location = new System.Drawing.Point(227, 282);
            this.IconBox.Name = "IconBox";
            this.IconBox.Size = new System.Drawing.Size(90, 82);
            this.IconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IconBox.TabIndex = 0;
            this.IconBox.TabStop = false;
            // 
            // ShellcodeFile
            // 
            this.ShellcodeFile.AllowDrop = true;
            this.ShellcodeFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(44)))));
            this.ShellcodeFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ShellcodeFile.Font = new System.Drawing.Font("Alef", 10.25F, System.Drawing.FontStyle.Bold);
            this.ShellcodeFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.ShellcodeFile.Location = new System.Drawing.Point(14, 111);
            this.ShellcodeFile.Multiline = true;
            this.ShellcodeFile.Name = "ShellcodeFile";
            this.ShellcodeFile.Size = new System.Drawing.Size(237, 24);
            this.ShellcodeFile.TabIndex = 4;
            this.ShellcodeFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ShellcodeFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.ShellcodeFile_DragDrop);
            this.ShellcodeFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.ShellcodeFile_DragEnter);
            // 
            // XorKeys
            // 
            this.XorKeys.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(44)))));
            this.XorKeys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.XorKeys.Font = new System.Drawing.Font("Alef", 10.25F, System.Drawing.FontStyle.Bold);
            this.XorKeys.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.XorKeys.Location = new System.Drawing.Point(15, 156);
            this.XorKeys.Multiline = true;
            this.XorKeys.Name = "XorKeys";
            this.XorKeys.Size = new System.Drawing.Size(302, 24);
            this.XorKeys.TabIndex = 5;
            this.XorKeys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BuildBtn
            // 
            this.BuildBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.BuildBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuildBtn.Font = new System.Drawing.Font("Alef", 13.25F, System.Drawing.FontStyle.Bold);
            this.BuildBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(67)))));
            this.BuildBtn.Location = new System.Drawing.Point(16, 324);
            this.BuildBtn.Name = "BuildBtn";
            this.BuildBtn.Size = new System.Drawing.Size(202, 40);
            this.BuildBtn.TabIndex = 6;
            this.BuildBtn.Text = "Compile";
            this.BuildBtn.UseVisualStyleBackColor = false;
            this.BuildBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // SelectFileBtn
            // 
            this.SelectFileBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.SelectFileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectFileBtn.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold);
            this.SelectFileBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(67)))));
            this.SelectFileBtn.Location = new System.Drawing.Point(255, 111);
            this.SelectFileBtn.Name = "SelectFileBtn";
            this.SelectFileBtn.Size = new System.Drawing.Size(62, 24);
            this.SelectFileBtn.TabIndex = 7;
            this.SelectFileBtn.Text = "Choose";
            this.SelectFileBtn.UseVisualStyleBackColor = false;
            this.SelectFileBtn.Click += new System.EventHandler(this.SelectFileBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.label1.Location = new System.Drawing.Point(13, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select file with Shellcode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.label2.Location = new System.Drawing.Point(12, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Random Xor-Key";
            // 
            // GenerateInit
            // 
            this.GenerateInit.Enabled = true;
            this.GenerateInit.Tick += new System.EventHandler(this.GenerateInit_Tick);
            // 
            // arch86
            // 
            this.arch86.AutoSize = true;
            this.arch86.BackColor = System.Drawing.Color.Transparent;
            this.arch86.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold);
            this.arch86.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.arch86.Location = new System.Drawing.Point(224, 254);
            this.arch86.Name = "arch86";
            this.arch86.Size = new System.Drawing.Size(44, 19);
            this.arch86.TabIndex = 10;
            this.arch86.TabStop = true;
            this.arch86.Text = "x86";
            this.arch86.UseVisualStyleBackColor = false;
            // 
            // arch64
            // 
            this.arch64.AutoSize = true;
            this.arch64.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold);
            this.arch64.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.arch64.Location = new System.Drawing.Point(272, 254);
            this.arch64.Name = "arch64";
            this.arch64.Size = new System.Drawing.Size(45, 19);
            this.arch64.TabIndex = 11;
            this.arch64.TabStop = true;
            this.arch64.Text = "x64";
            this.arch64.UseVisualStyleBackColor = true;
            // 
            // SelectIconBtn
            // 
            this.SelectIconBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.SelectIconBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectIconBtn.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold);
            this.SelectIconBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(67)))));
            this.SelectIconBtn.Location = new System.Drawing.Point(120, 250);
            this.SelectIconBtn.Name = "SelectIconBtn";
            this.SelectIconBtn.Size = new System.Drawing.Size(98, 33);
            this.SelectIconBtn.TabIndex = 12;
            this.SelectIconBtn.Text = "Select Icon";
            this.SelectIconBtn.UseVisualStyleBackColor = false;
            this.SelectIconBtn.Click += new System.EventHandler(this.SelectIconBtn_Click);
            // 
            // AssembyCloneBtn
            // 
            this.AssembyCloneBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.AssembyCloneBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AssembyCloneBtn.Font = new System.Drawing.Font("Alef", 7.25F, System.Drawing.FontStyle.Bold);
            this.AssembyCloneBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(67)))));
            this.AssembyCloneBtn.Location = new System.Drawing.Point(16, 250);
            this.AssembyCloneBtn.Name = "AssembyCloneBtn";
            this.AssembyCloneBtn.Size = new System.Drawing.Size(100, 33);
            this.AssembyCloneBtn.TabIndex = 13;
            this.AssembyCloneBtn.Text = "Assembly Clone";
            this.AssembyCloneBtn.UseVisualStyleBackColor = false;
            this.AssembyCloneBtn.Click += new System.EventHandler(this.AssembyCloneBtn_Click);
            // 
            // ResetBtn
            // 
            this.ResetBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.ResetBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetBtn.Font = new System.Drawing.Font("Alef", 10.25F, System.Drawing.FontStyle.Bold);
            this.ResetBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(67)))));
            this.ResetBtn.Location = new System.Drawing.Point(16, 287);
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.Size = new System.Drawing.Size(202, 33);
            this.ResetBtn.TabIndex = 14;
            this.ResetBtn.Text = "Reset All Parameters";
            this.ResetBtn.UseVisualStyleBackColor = false;
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // UseObfuscate
            // 
            this.UseObfuscate.AutoSize = true;
            this.UseObfuscate.BackColor = System.Drawing.Color.Transparent;
            this.UseObfuscate.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold);
            this.UseObfuscate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.UseObfuscate.Location = new System.Drawing.Point(16, 225);
            this.UseObfuscate.Name = "UseObfuscate";
            this.UseObfuscate.Size = new System.Drawing.Size(88, 19);
            this.UseObfuscate.TabIndex = 16;
            this.UseObfuscate.Text = "Obfuscator";
            this.UseObfuscate.UseVisualStyleBackColor = false;
            // 
            // ProcessInjectCmb
            // 
            this.ProcessInjectCmb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(44)))));
            this.ProcessInjectCmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ProcessInjectCmb.Font = new System.Drawing.Font("Franklin Gothic Medium", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProcessInjectCmb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.ProcessInjectCmb.Items.AddRange(new object[] {
            "explorer",
            "notepad"});
            this.ProcessInjectCmb.Location = new System.Drawing.Point(124, 189);
            this.ProcessInjectCmb.Name = "ProcessInjectCmb";
            this.ProcessInjectCmb.Size = new System.Drawing.Size(155, 24);
            this.ProcessInjectCmb.Sorted = true;
            this.ProcessInjectCmb.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.label3.Location = new System.Drawing.Point(12, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Process To Inject";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Builder.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(262, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 58);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.label4.Location = new System.Drawing.Point(119, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Shellcode Loader [1.0]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.label5.Location = new System.Drawing.Point(143, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = "By K3rnel-Dev";
            // 
            // AboutShowBtn
            // 
            this.AboutShowBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.AboutShowBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AboutShowBtn.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold);
            this.AboutShowBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(67)))));
            this.AboutShowBtn.Location = new System.Drawing.Point(285, 188);
            this.AboutShowBtn.Name = "AboutShowBtn";
            this.AboutShowBtn.Size = new System.Drawing.Size(32, 25);
            this.AboutShowBtn.TabIndex = 22;
            this.AboutShowBtn.Text = "?";
            this.AboutShowBtn.UseVisualStyleBackColor = false;
            this.AboutShowBtn.Click += new System.EventHandler(this.AboutShowBtn_Click);
            // 
            // AboutBtn
            // 
            this.AboutBtn.AutoSize = true;
            this.AboutBtn.BackColor = System.Drawing.Color.Transparent;
            this.AboutBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AboutBtn.Font = new System.Drawing.Font("Alef", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.AboutBtn.Location = new System.Drawing.Point(273, 69);
            this.AboutBtn.Name = "AboutBtn";
            this.AboutBtn.Size = new System.Drawing.Size(41, 15);
            this.AboutBtn.TabIndex = 23;
            this.AboutBtn.Text = "About";
            this.AboutBtn.Click += new System.EventHandler(this.AboutBtn_Click);
            // 
            // AutorunChk
            // 
            this.AutorunChk.AutoSize = true;
            this.AutorunChk.BackColor = System.Drawing.Color.Transparent;
            this.AutorunChk.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold);
            this.AutorunChk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.AutorunChk.Location = new System.Drawing.Point(245, 225);
            this.AutorunChk.Name = "AutorunChk";
            this.AutorunChk.Size = new System.Drawing.Size(72, 19);
            this.AutorunChk.TabIndex = 24;
            this.AutorunChk.Text = "Autorun";
            this.AutorunChk.UseVisualStyleBackColor = false;
            // 
            // CompressChk
            // 
            this.CompressChk.AutoSize = true;
            this.CompressChk.BackColor = System.Drawing.Color.Transparent;
            this.CompressChk.Font = new System.Drawing.Font("Alef", 8.25F, System.Drawing.FontStyle.Bold);
            this.CompressChk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(133)))));
            this.CompressChk.Location = new System.Drawing.Point(116, 225);
            this.CompressChk.Name = "CompressChk";
            this.CompressChk.Size = new System.Drawing.Size(112, 19);
            this.CompressChk.TabIndex = 25;
            this.CompressChk.Text = "Compress (x64)";
            this.CompressChk.UseVisualStyleBackColor = false;
            // 
            // CMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(336, 376);
            this.Controls.Add(this.CompressChk);
            this.Controls.Add(this.AutorunChk);
            this.Controls.Add(this.AboutBtn);
            this.Controls.Add(this.AboutShowBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ProcessInjectCmb);
            this.Controls.Add(this.UseObfuscate);
            this.Controls.Add(this.ResetBtn);
            this.Controls.Add(this.AssembyCloneBtn);
            this.Controls.Add(this.SelectIconBtn);
            this.Controls.Add(this.arch64);
            this.Controls.Add(this.arch86);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectFileBtn);
            this.Controls.Add(this.BuildBtn);
            this.Controls.Add(this.XorKeys);
            this.Controls.Add(this.ShellcodeFile);
            this.Controls.Add(this.MinimazeBtn);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.IconBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cobalt Shellcode Builder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.MinimazeBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox IconBox;
        private System.Windows.Forms.PictureBox CloseBtn;
        private System.Windows.Forms.PictureBox MinimazeBtn;
        private System.Windows.Forms.TextBox ShellcodeFile;
        private System.Windows.Forms.TextBox XorKeys;
        private System.Windows.Forms.Button BuildBtn;
        private System.Windows.Forms.Button SelectFileBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer GenerateInit;
        private System.Windows.Forms.RadioButton arch86;
        private System.Windows.Forms.RadioButton arch64;
        private System.Windows.Forms.Button SelectIconBtn;
        private System.Windows.Forms.Button AssembyCloneBtn;
        private System.Windows.Forms.Button ResetBtn;
        private System.Windows.Forms.CheckBox UseObfuscate;
        private System.Windows.Forms.ComboBox ProcessInjectCmb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button AboutShowBtn;
        private System.Windows.Forms.Label AboutBtn;
        private System.Windows.Forms.CheckBox AutorunChk;
        private System.Windows.Forms.CheckBox CompressChk;
    }
}

