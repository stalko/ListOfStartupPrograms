namespace ListOfStartupPrograms
{
    partial class Form1
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
            this.listViewPrograms = new System.Windows.Forms.ListView();
            this.Icon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeAutorun = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsDigitalSignatureExists = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsDigitalSignatureCorrect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CompanyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewPrograms
            // 
            this.listViewPrograms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPrograms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Icon,
            this.FileName,
            this.Command,
            this.FilePath,
            this.TypeAutorun,
            this.IsDigitalSignatureExists,
            this.IsDigitalSignatureCorrect,
            this.CompanyName});
            this.listViewPrograms.GridLines = true;
            this.listViewPrograms.Location = new System.Drawing.Point(12, 12);
            this.listViewPrograms.MultiSelect = false;
            this.listViewPrograms.Name = "listViewPrograms";
            this.listViewPrograms.Size = new System.Drawing.Size(547, 335);
            this.listViewPrograms.TabIndex = 0;
            this.listViewPrograms.UseCompatibleStateImageBehavior = false;
            this.listViewPrograms.View = System.Windows.Forms.View.Details;
            this.listViewPrograms.DoubleClick += new System.EventHandler(this.listViewPrograms_DoubleClick);
            // 
            // Icon
            // 
            this.Icon.Text = "Icon";
            this.Icon.Width = 71;
            // 
            // FileName
            // 
            this.FileName.Text = "File Name";
            this.FileName.Width = 129;
            // 
            // Command
            // 
            this.Command.Text = "Command";
            this.Command.Width = 132;
            // 
            // FilePath
            // 
            this.FilePath.Text = "FilePath";
            this.FilePath.Width = 113;
            // 
            // TypeAutorun
            // 
            this.TypeAutorun.Text = "Type Autorun";
            this.TypeAutorun.Width = 88;
            // 
            // IsDigitalSignatureExists
            // 
            this.IsDigitalSignatureExists.Text = "Is Digital Signature Exists";
            // 
            // IsDigitalSignatureCorrect
            // 
            this.IsDigitalSignatureCorrect.Text = "Is Digital Signature Correct";
            // 
            // CompanyName
            // 
            this.CompanyName.Text = "Company Name";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 359);
            this.Controls.Add(this.listViewPrograms);
            this.Name = "Form1";
            this.Text = "List Of Startup Programs";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewPrograms;
        private System.Windows.Forms.ColumnHeader Icon;
        private System.Windows.Forms.ColumnHeader FileName;
        private System.Windows.Forms.ColumnHeader Command;
        private System.Windows.Forms.ColumnHeader FilePath;
        private System.Windows.Forms.ColumnHeader TypeAutorun;
        private System.Windows.Forms.ColumnHeader IsDigitalSignatureExists;
        private System.Windows.Forms.ColumnHeader IsDigitalSignatureCorrect;
        private System.Windows.Forms.ColumnHeader CompanyName;
    }
}

