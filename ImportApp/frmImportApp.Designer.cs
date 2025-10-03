namespace ImportApp
{
    partial class frmEgeriaImport
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEgeriaImport));
            progressBarDbConnect = new ProgressBar();
            lblDbAddress = new Label();
            lblDbUsername = new Label();
            lblDbPassword = new Label();
            txtDbAddress = new TextBox();
            txtDbUserId = new TextBox();
            txtDbPassword = new TextBox();
            btnDbConnect = new Button();
            lblDbStateTitle = new Label();
            lblDbState = new Label();
            groupBoxDb = new GroupBox();
            lblTable = new Label();
            txtTable = new TextBox();
            lblFilePath = new Label();
            txtFilePath = new TextBox();
            btnChooseFile = new Button();
            btnImport = new Button();
            txtImportLog = new TextBox();
            btnCreateTable = new Button();
            groupBoxDb.SuspendLayout();
            SuspendLayout();
            // 
            // progressBarDbConnect
            // 
            progressBarDbConnect.Dock = DockStyle.Bottom;
            progressBarDbConnect.Location = new Point(0, 351);
            progressBarDbConnect.Name = "progressBarDbConnect";
            progressBarDbConnect.Size = new Size(1163, 25);
            progressBarDbConnect.Style = ProgressBarStyle.Continuous;
            progressBarDbConnect.TabIndex = 1;
            progressBarDbConnect.Visible = false;
            // 
            // lblDbAddress
            // 
            lblDbAddress.AutoSize = true;
            lblDbAddress.Location = new Point(74, 32);
            lblDbAddress.Name = "lblDbAddress";
            lblDbAddress.Size = new Size(40, 15);
            lblDbAddress.TabIndex = 0;
            lblDbAddress.Text = "Adres:";
            // 
            // lblDbUsername
            // 
            lblDbUsername.AutoSize = true;
            lblDbUsername.Location = new Point(38, 64);
            lblDbUsername.Name = "lblDbUsername";
            lblDbUsername.Size = new Size(76, 15);
            lblDbUsername.TabIndex = 1;
            lblDbUsername.Text = "Kullanıcı Adı:";
            // 
            // lblDbPassword
            // 
            lblDbPassword.AutoSize = true;
            lblDbPassword.Location = new Point(74, 97);
            lblDbPassword.Name = "lblDbPassword";
            lblDbPassword.Size = new Size(33, 15);
            lblDbPassword.TabIndex = 2;
            lblDbPassword.Text = "Şifre:";
            // 
            // txtDbAddress
            // 
            txtDbAddress.Location = new Point(120, 29);
            txtDbAddress.Name = "txtDbAddress";
            txtDbAddress.PlaceholderText = "1.1.1.1:1521/PROD";
            txtDbAddress.Size = new Size(263, 23);
            txtDbAddress.TabIndex = 3;
            // 
            // txtDbUserId
            // 
            txtDbUserId.Location = new Point(120, 61);
            txtDbUserId.Name = "txtDbUserId";
            txtDbUserId.PlaceholderText = "IFSAPP";
            txtDbUserId.Size = new Size(263, 23);
            txtDbUserId.TabIndex = 4;
            // 
            // txtDbPassword
            // 
            txtDbPassword.Location = new Point(120, 94);
            txtDbPassword.Name = "txtDbPassword";
            txtDbPassword.PlaceholderText = "Şifre";
            txtDbPassword.Size = new Size(263, 23);
            txtDbPassword.TabIndex = 5;
            txtDbPassword.UseSystemPasswordChar = true;
            // 
            // btnDbConnect
            // 
            btnDbConnect.Location = new Point(293, 123);
            btnDbConnect.Name = "btnDbConnect";
            btnDbConnect.Size = new Size(90, 36);
            btnDbConnect.TabIndex = 6;
            btnDbConnect.Text = "Bağlan";
            btnDbConnect.UseVisualStyleBackColor = true;
            btnDbConnect.Click += btnDbConnect_Click;
            // 
            // lblDbStateTitle
            // 
            lblDbStateTitle.AutoSize = true;
            lblDbStateTitle.Location = new Point(14, 134);
            lblDbStateTitle.Name = "lblDbStateTitle";
            lblDbStateTitle.Size = new Size(100, 15);
            lblDbStateTitle.TabIndex = 7;
            lblDbStateTitle.Text = "Bağlantı Durumu:";
            // 
            // lblDbState
            // 
            lblDbState.AutoSize = true;
            lblDbState.Location = new Point(120, 134);
            lblDbState.Name = "lblDbState";
            lblDbState.Size = new Size(66, 15);
            lblDbState.TabIndex = 8;
            lblDbState.Text = "Bağlı Değil.";
            // 
            // groupBoxDb
            // 
            groupBoxDb.Controls.Add(txtDbPassword);
            groupBoxDb.Controls.Add(lblDbState);
            groupBoxDb.Controls.Add(lblDbPassword);
            groupBoxDb.Controls.Add(lblDbUsername);
            groupBoxDb.Controls.Add(lblDbStateTitle);
            groupBoxDb.Controls.Add(txtDbUserId);
            groupBoxDb.Controls.Add(btnDbConnect);
            groupBoxDb.Controls.Add(lblDbAddress);
            groupBoxDb.Controls.Add(txtDbAddress);
            groupBoxDb.Location = new Point(27, 26);
            groupBoxDb.Name = "groupBoxDb";
            groupBoxDb.Size = new Size(410, 179);
            groupBoxDb.TabIndex = 9;
            groupBoxDb.TabStop = false;
            groupBoxDb.Text = "Veritabanı Bağlantısı";
            // 
            // lblTable
            // 
            lblTable.AutoSize = true;
            lblTable.Location = new Point(101, 235);
            lblTable.Name = "lblTable";
            lblTable.Size = new Size(39, 15);
            lblTable.TabIndex = 10;
            lblTable.Text = "Tablo:";
            // 
            // txtTable
            // 
            txtTable.Location = new Point(147, 232);
            txtTable.Name = "txtTable";
            txtTable.Size = new Size(263, 23);
            txtTable.TabIndex = 11;
            // 
            // lblFilePath
            // 
            lblFilePath.AutoSize = true;
            lblFilePath.Location = new Point(37, 275);
            lblFilePath.Name = "lblFilePath";
            lblFilePath.Size = new Size(103, 15);
            lblFilePath.TabIndex = 12;
            lblFilePath.Text = "Aktarılacak Dosya:";
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(147, 272);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.PlaceholderText = "Dosya seçiniz...";
            txtFilePath.Size = new Size(182, 23);
            txtFilePath.TabIndex = 13;
            // 
            // btnChooseFile
            // 
            btnChooseFile.Location = new Point(335, 272);
            btnChooseFile.Name = "btnChooseFile";
            btnChooseFile.Size = new Size(75, 23);
            btnChooseFile.TabIndex = 14;
            btnChooseFile.Text = "Seç";
            btnChooseFile.UseVisualStyleBackColor = true;
            btnChooseFile.Click += btnChooseFile_Click;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(147, 301);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(182, 43);
            btnImport.TabIndex = 16;
            btnImport.Text = "Aktar";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // txtImportLog
            // 
            txtImportLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtImportLog.Location = new Point(480, 26);
            txtImportLog.Multiline = true;
            txtImportLog.Name = "txtImportLog";
            txtImportLog.ReadOnly = true;
            txtImportLog.ScrollBars = ScrollBars.Vertical;
            txtImportLog.Size = new Size(645, 269);
            txtImportLog.TabIndex = 17;
            // 
            // btnCreateTable
            // 
            btnCreateTable.Location = new Point(335, 301);
            btnCreateTable.Name = "btnCreateTable";
            btnCreateTable.Size = new Size(75, 44);
            btnCreateTable.TabIndex = 18;
            btnCreateTable.Text = "Tablo Oluştur";
            btnCreateTable.UseVisualStyleBackColor = true;
            btnCreateTable.Click += btnCreateTable_Click;
            // 
            // frmEgeriaImport
            // 
            AcceptButton = btnDbConnect;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1163, 376);
            Controls.Add(btnCreateTable);
            Controls.Add(txtImportLog);
            Controls.Add(btnImport);
            Controls.Add(btnChooseFile);
            Controls.Add(lblFilePath);
            Controls.Add(txtFilePath);
            Controls.Add(lblTable);
            Controls.Add(txtTable);
            Controls.Add(groupBoxDb);
            Controls.Add(progressBarDbConnect);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmEgeriaImport";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aktarım Uygulaması";
            groupBoxDb.ResumeLayout(false);
            groupBoxDb.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ProgressBar progressBarDbConnect;
        private Label lblDbAddress;
        private Label lblDbUsername;
        private Label lblDbPassword;
        private TextBox txtDbAddress;
        private TextBox txtDbUserId;
        private TextBox txtDbPassword;
        private Button btnDbConnect;
        private Label lblDbStateTitle;
        private Label lblDbState;
        private GroupBox groupBoxDb;
        private Label lblTable;
        private TextBox txtTable;
        private Label lblFilePath;
        private TextBox txtFilePath;
        private Button btnChooseFile;
        private Button btnImport;
        private TextBox txtImportLog;
        private Button btnCreateTable;
    }
}
