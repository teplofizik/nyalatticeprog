namespace NyaLatticeProg
{
    partial class fMain
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
            this.bCancel = new System.Windows.Forms.Button();
            this.bProgram = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.tFilename = new System.Windows.Forms.TextBox();
            this.bLoad = new System.Windows.Forms.Button();
            this.lStatus = new System.Windows.Forms.Label();
            this.tTimer = new System.Windows.Forms.Timer(this.components);
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.pbStatus = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(390, 96);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 0;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bProgram
            // 
            this.bProgram.Location = new System.Drawing.Point(309, 96);
            this.bProgram.Name = "bProgram";
            this.bProgram.Size = new System.Drawing.Size(75, 23);
            this.bProgram.TabIndex = 1;
            this.bProgram.Text = "Program";
            this.bProgram.UseVisualStyleBackColor = true;
            this.bProgram.Click += new System.EventHandler(this.bProgram_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(12, 67);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(453, 23);
            this.pbProgress.TabIndex = 2;
            // 
            // tFilename
            // 
            this.tFilename.Location = new System.Drawing.Point(12, 12);
            this.tFilename.Name = "tFilename";
            this.tFilename.Size = new System.Drawing.Size(416, 20);
            this.tFilename.TabIndex = 3;
            this.tFilename.TextChanged += new System.EventHandler(this.tFilename_TextChanged);
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(434, 10);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(31, 23);
            this.bLoad.TabIndex = 4;
            this.bLoad.Text = "...";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(12, 51);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(52, 13);
            this.lStatus.TabIndex = 5;
            this.lStatus.Text = "Status: ...";
            // 
            // tTimer
            // 
            this.tTimer.Interval = 5;
            this.tTimer.Tick += new System.EventHandler(this.tTimer_Tick);
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "Binary (*.bin)|*bin|All files (*.*)|*.*";
            this.dlgOpen.Title = "Open lattice bitmap...";
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(12, 98);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(16, 17);
            this.pbStatus.TabIndex = 7;
            this.pbStatus.TabStop = false;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 125);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.tFilename);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.bProgram);
            this.Controls.Add(this.bCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "fMain";
            this.Text = "Lattice iCE5LP programmer";
            this.Load += new System.EventHandler(this.fMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bProgram;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.TextBox tFilename;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.Timer tTimer;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.PictureBox pbStatus;
    }
}

