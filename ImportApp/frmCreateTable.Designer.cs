namespace ImportApp
{
    partial class frmCreateTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateTable));
            dataGridView1 = new DataGridView();
            txtColumnName = new DataGridViewTextBoxColumn();
            txtColumnType = new DataGridViewComboBoxColumn();
            txtTextLength = new DataGridViewTextBoxColumn();
            txtIsRequired = new DataGridViewCheckBoxColumn();
            btnCreate = new Button();
            btnCancel = new Button();
            txtTableName = new TextBox();
            lblTableName = new Label();
            panelDataGridView = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panelDataGridView.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { txtColumnName, txtColumnType, txtTextLength, txtIsRequired });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(458, 470);
            dataGridView1.TabIndex = 0;
            // 
            // txtColumnName
            // 
            txtColumnName.HeaderText = "KOLON ADI";
            txtColumnName.Name = "txtColumnName";
            // 
            // txtColumnType
            // 
            txtColumnType.HeaderText = "VERI TIPI";
            txtColumnType.Items.AddRange(new object[] { "VARCHAR2", "NUMBER", "DATE", "TIMESTAMP", "CLOB" });
            txtColumnType.Name = "txtColumnType";
            // 
            // txtTextLength
            // 
            txtTextLength.HeaderText = "UZUNLUK";
            txtTextLength.Name = "txtTextLength";
            // 
            // txtIsRequired
            // 
            txtIsRequired.HeaderText = "ZORUNLU";
            txtIsRequired.Name = "txtIsRequired";
            txtIsRequired.Resizable = DataGridViewTriState.True;
            txtIsRequired.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(312, 522);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 1;
            btnCreate.Text = "Oluştur";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(393, 522);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "İptal";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtTableName
            // 
            txtTableName.Location = new Point(57, 17);
            txtTableName.Name = "txtTableName";
            txtTableName.Size = new Size(413, 23);
            txtTableName.TabIndex = 3;
            // 
            // lblTableName
            // 
            lblTableName.AutoSize = true;
            lblTableName.Location = new Point(12, 20);
            lblTableName.Name = "lblTableName";
            lblTableName.Size = new Size(39, 15);
            lblTableName.TabIndex = 4;
            lblTableName.Text = "Tablo:";
            // 
            // panelDataGridView
            // 
            panelDataGridView.Controls.Add(dataGridView1);
            panelDataGridView.Location = new Point(12, 46);
            panelDataGridView.Name = "panelDataGridView";
            panelDataGridView.Size = new Size(458, 470);
            panelDataGridView.TabIndex = 5;
            // 
            // frmCreateTable
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(485, 557);
            Controls.Add(panelDataGridView);
            Controls.Add(lblTableName);
            Controls.Add(txtTableName);
            Controls.Add(btnCancel);
            Controls.Add(btnCreate);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmCreateTable";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tablo Oluştur";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panelDataGridView.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnCreate;
        private Button btnCancel;
        private TextBox txtTableName;
        private Label lblTableName;
        private DataGridViewTextBoxColumn txtColumnName;
        private DataGridViewComboBoxColumn txtColumnType;
        private DataGridViewTextBoxColumn txtTextLength;
        private DataGridViewCheckBoxColumn txtIsRequired;
        private Panel panelDataGridView;
    }
}