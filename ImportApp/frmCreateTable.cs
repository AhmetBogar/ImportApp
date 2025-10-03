using ImportApp.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImportApp
{
    public partial class frmCreateTable : Form
    {
        private OracleConnection _connection;
        private List<ColumnDefinition> _columns;

        public string TableName
        {
            get => txtTableName.Text;
            set => txtTableName.Text = value;
        }

        public frmCreateTable(OracleConnection connection, List<ColumnDefinition> columns)
        {
            InitializeComponent();
            _connection = connection;
            _columns = columns;
            this.Load += frmCreateTable_Load;
        }

        private void frmCreateTable_Load(object sender, EventArgs e)
        {
            FillColumnsFromExcel();
        }

        private void FillColumnsFromExcel()
        {
            if (_columns == null || _columns.Count == 0) return;

            foreach (var col in _columns)
            {
                int rowIndex = dataGridView1.Rows.Add();
                var row = dataGridView1.Rows[rowIndex];

                row.Cells["txtColumnName"].Value = col.ColumnName;
                row.Cells["txtColumnType"].Value = string.IsNullOrEmpty(col.DataType) ? "VARCHAR2" : col.DataType;
                row.Cells["txtTextLength"].Value = "4000";   
                row.Cells["txtIsRequired"].Value = false;        
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> columnDefs = new List<string>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    string colName = row.Cells["txtColumnName"].Value?.ToString()?.Trim().ToUpper();
                    string colType = row.Cells["txtColumnType"].Value?.ToString()?.Trim().ToUpper();
                    bool isRequired = row.Cells["txtIsRequired"].Value is bool b && b;

                    if (string.IsNullOrWhiteSpace(colName) || string.IsNullOrWhiteSpace(colType))
                        continue;

                    string colLength = row.Cells["txtTextLength"].Value?.ToString()?.Trim();
                    string typeDef = colType switch
                    {
                        "VARCHAR2" => $"VARCHAR2({(string.IsNullOrEmpty(colLength) ? "4000" : colLength)})",
                        "NUMBER" => "NUMBER",
                        "DATE" => "DATE",
                        "TIMESTAMP" => "TIMESTAMP",
                        "CLOB" => "CLOB",
                        _ => colType
                    };

                    string notNull = isRequired ? "NOT NULL" : "";
                    columnDefs.Add($"\"{colName}\" {typeDef} {notNull}".Trim());
                }

                if (columnDefs.Count == 0)
                {
                    MessageBox.Show("En az bir kolon tanımlamalısınız!",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tableName = txtTableName.Text.Trim().ToUpper();
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    MessageBox.Show("Lütfen tablo adı giriniz!",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string createSql = $"CREATE TABLE \"{tableName}\" (\n    {string.Join(",\n    ", columnDefs)}\n)";

                using (var cmd = new OracleCommand(createSql, _connection))
                {
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Tablo başarıyla oluşturuldu!",
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OracleException oex)
            {
                MessageBox.Show($"Oracle hatası:\n{oex.Message}\n\nHata Kodu: {oex.Number}",
                                "Oracle Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen hata oluştu:\n{ex.Message}\n\nDetaylar:\n{ex}",
                                "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
