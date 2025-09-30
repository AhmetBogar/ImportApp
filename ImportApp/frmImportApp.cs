using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ImportApp
{
    public partial class frmEgeriaImport : Form
    {
        public frmEgeriaImport()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private async Task ConnectDbAsync()
        {
            progressBarDbConnect.Style = ProgressBarStyle.Marquee;
            progressBarDbConnect.Visible = true;
            lblDbState.Text = "Bağlanıyor...";
            lblDbState.ForeColor = Color.Black;

            string connStr = BuildConnectionString();

            try
            {
                using (OracleConnection con = new OracleConnection(connStr))
                {
                    await con.OpenAsync();
                }
                lblDbState.Text = "Bağlantı başarılı.";
                lblDbState.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblDbState.Text = "Bağlantı başarısız.";
                lblDbState.ForeColor = Color.Red;
                MessageBox.Show("Veritabanına bağlanırken hata oluştu:\n" + ex.Message,
                                "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBarDbConnect.Visible = false;
            }
        }

        private string BuildConnectionString()
        {
            return $"User Id={txtDbUserId.Text};Password={txtDbPassword.Text};Data Source={txtDbAddress.Text};";
        }

        private class ExcelRow
        {
            public int RowNumber { get; set; }
            public Dictionary<string, object> Values { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        }

        private class ErrorRecord
        {
            public int RowNumber { get; set; }
            public string? ColumnName { get; set; }
            public string? BadValue { get; set; }
            public string? Message { get; set; }
            public int RowIndex { get; set; }
            public string ErrorMessage { get; set; }
        }

        private Dictionary<string, string> GetDbColumnsWithTypes(string connStr, string tableName)
        {
            var dbColumns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using (var conn = new OracleConnection(connStr))
            {
                conn.Open();
                string query = @"
                    SELECT COLUMN_NAME, DATA_TYPE 
                    FROM USER_TAB_COLUMNS 
                    WHERE TABLE_NAME = :tblName";

                using (var cmd = new OracleCommand(query, conn))
                {
                    cmd.Parameters.Add(new OracleParameter(":tblName", tableName.ToUpper()));
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string colName = reader.GetString(0);
                            string dataType = reader.GetString(1);
                            dbColumns[colName] = dataType;
                        }
                    }
                }
            }
            return dbColumns;
        }

        public void InsertIntoOracle(DataTable dt, string connectionString, string tableName)
        {
            using (var conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (var bulkCopy = new OracleBulkCopy(conn, OracleBulkCopyOptions.Default))
                {
                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BatchSize = 5000;
                    bulkCopy.BulkCopyTimeout = 0;

                    foreach (DataColumn col in dt.Columns)
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);

                    bulkCopy.WriteToServer(dt);
                }
            }
        }

        private void AddLog(string message)
        {
            txtImportLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            txtImportLog.SelectionStart = txtImportLog.Text.Length;
            txtImportLog.ScrollToCaret();
        }

        private List<ExcelRow> ReadExcel(string filePath, out List<string> excelColumns)
        {
            excelColumns = new List<string>();
            var rows = new List<ExcelRow>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var ws = package.Workbook.Worksheets.FirstOrDefault();
                if (ws == null || ws.Dimension == null) return rows;

                int colCount = ws.Dimension.End.Column;
                int rowCount = ws.Dimension.End.Row;

                for (int c = 1; c <= colCount; c++)
                {
                    string header = ws.Cells[1, c].Text.Trim();
                    if (!string.IsNullOrWhiteSpace(header) && !excelColumns.Contains(header, StringComparer.OrdinalIgnoreCase))
                        excelColumns.Add(header);
                }

                for (int r = 2; r <= rowCount; r++)
                {
                    var rowDict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    foreach (var col in excelColumns)
                    {
                        int colIndex = excelColumns.IndexOf(col) + 1;
                        object val = ws.Cells[r, colIndex].Value;
                        rowDict[col] = val ?? DBNull.Value;
                    }
                    rows.Add(new ExcelRow { RowNumber = r, Values = rowDict });
                }
            }
            return rows;
        }

        private void WriteErrorLogExcel(string desktopPath, List<ErrorRecord> errors)
        {
            if (errors == null || errors.Count == 0) return;

            string filePath = Path.Combine(desktopPath, $"ImportErrors_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");

            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add("ImportErrors");

                ws.Cells[1, 1].Value = "Satır No";
                ws.Cells[1, 2].Value = "Kolon Adı";
                ws.Cells[1, 3].Value = "Hatalı Değer";
                ws.Cells[1, 4].Value = "Hata Mesajı";

                int r = 2;
                foreach (var e in errors)
                {
                    ws.Cells[r, 1].Value = e.RowNumber;
                    ws.Cells[r, 2].Value = e.ColumnName;
                    ws.Cells[r, 3].Value = e.BadValue;
                    ws.Cells[r, 4].Value = e.Message;
                    r++;
                }

                ws.Cells[1, 1, r, 4].AutoFitColumns();
                p.SaveAs(new FileInfo(filePath));
            }

            AddLog($"Hata detayları Excel’e kaydedildi: {filePath}");
        }


        private async Task StartImportAsync()
        {
            string connStr = BuildConnectionString();
            string excelPath = txtFilePath.Text;
            string tableName = txtTable.Text.Trim();
            List<string> excelCols = null;

            progressBarDbConnect.Style = ProgressBarStyle.Marquee;
            progressBarDbConnect.MarqueeAnimationSpeed = 30;
            progressBarDbConnect.Visible = true;

            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("Lütfen hedef tablo adını girin.");
                progressBarDbConnect.Visible = false;
                return;
            }

            if (!File.Exists(excelPath))
            {
                MessageBox.Show("Excel dosyası bulunamadı!");
                progressBarDbConnect.Visible = false;
                return;
            }

            txtImportLog.Clear();
            AddLog("Excel dosyası okunuyor...");

            /*    var excelData = ReadExcel(excelPath, out List<string> excelCols);
                AddLog($"Excel kolonları: {string.Join(", ", excelCols)}");*/

            var excelData = await Task.Run(() =>
            {
                return ReadExcel(excelPath, out excelCols);
            });

            AddLog($"Excel kolonları: {string.Join(", ", excelCols)}");


            var dbColumns = GetDbColumnsWithTypes(connStr, tableName);
            if (dbColumns == null || dbColumns.Count == 0)
            {
                AddLog("Tablo bulunamadı veya kolon bilgisi alınamadı.");
                progressBarDbConnect.Visible = false;
                return;
            }

            var matchedCols = excelCols
                .Where(ec => dbColumns.Keys.Any(k => k.Equals(ec, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            if (!matchedCols.Any())
            {
                AddLog("Eşleşen kolon bulunamadı. İşlem durduruldu.");
                progressBarDbConnect.Visible = false;
                return;
            }

            var columnsForInsert = matchedCols
                .Select(ec => dbColumns.Keys.First(k => k.Equals(ec, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            AddLog($"Eşleşen kolonlar: {string.Join(", ", columnsForInsert)}");
            AddLog("Veri aktarımı başlıyor...");

            var errorRecords = new List<ErrorRecord>();

            try
            {
                var dt = await Task.Run(() =>
                {
                    DataTable dtLocal = new DataTable();

                    foreach (var col in columnsForInsert)
                    {
                        string dbType = dbColumns[col].ToUpper();
                        switch (dbType)
                        {
                            case "NUMBER":
                            case "FLOAT":
                            case "INTEGER":
                                dtLocal.Columns.Add(col, typeof(decimal));
                                break;
                            case "DATE":
                            case "TIMESTAMP":
                            case "TIMESTAMP(6)":
                                dtLocal.Columns.Add(col, typeof(DateTime));
                                break;
                            default:
                                dtLocal.Columns.Add(col, typeof(string));
                                break;
                        }
                    }

                    int rowIndex = 0;
                    foreach (var excelRow in excelData)
                    {
                        rowIndex++;
                        var row = dtLocal.NewRow();
                        bool rowHasError = false;

                        foreach (var col in columnsForInsert)
                        {
                            try
                            {
                                if (excelRow.Values.ContainsKey(col))
                                {
                                    var value = excelRow.Values[col];
                                    if (value == null)
                                    {
                                        row[col] = DBNull.Value;
                                    }
                                    else if (dbColumns[col].ToUpper().Contains("DATE"))
                                    {
                                        row[col] = DateTime.TryParse(value.ToString(), out var dtVal) ? dtVal : DBNull.Value;
                                    }
                                    else if (dbColumns[col].ToUpper().Contains("NUMBER") ||
                                             dbColumns[col].ToUpper().Contains("FLOAT") ||
                                             dbColumns[col].ToUpper().Contains("INTEGER"))
                                    {
                                        row[col] = decimal.TryParse(value.ToString(), out var decVal) ? decVal : DBNull.Value;
                                    }
                                    else
                                    {
                                        row[col] = value.ToString();
                                    }
                                }
                                else
                                {
                                    row[col] = DBNull.Value;
                                }
                            }
                            catch (Exception ex)
                            {
                                rowHasError = true;
                                errorRecords.Add(new ErrorRecord
                                {
                                    RowIndex = rowIndex,
                                    ColumnName = col,
                                    ErrorMessage = ex.Message
                                });
                                break;
                            }
                        }

                        if (!rowHasError)
                            dtLocal.Rows.Add(row);
                    }

                    return dtLocal;
                });

                await Task.Run(() => InsertIntoOracle(dt, connStr, tableName));

                progressBarDbConnect.Style = ProgressBarStyle.Blocks;
                progressBarDbConnect.Value = 100;

                string summary = $"Aktarım tamamlandı. Başarılı satır: {dt.Rows.Count}, Hatalı satır: {errorRecords.Count}";
                AddLog(summary);
                MessageBox.Show(summary, "İşlem Özeti", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AddLog("Aktarım sırasında hata: " + ex.Message);
                MessageBox.Show("Hata: " + ex.Message, "Aktarım Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private bool TryParseExcelDate(object rawVal, out DateTime result)
        {
            result = default;
            if (rawVal == null || rawVal == DBNull.Value) return false;

            if (rawVal is DateTime dt)
            {
                result = dt;
                return true;
            }

            if (rawVal is double d)
            {
                try { result = DateTime.FromOADate(d); return true; }
                catch { }
            }

            string s = rawVal.ToString().Trim();
            if (string.IsNullOrEmpty(s)) return false;

            // Eğer string numeric ise OADate olma ihtimali
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out var dblCurr))
            {
                try { result = DateTime.FromOADate(dblCurr); return true; }
                catch { /* geç */ }
            }
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var dblInv))
            {
                try { result = DateTime.FromOADate(dblInv); return true; }
                catch { /* geç */ }
            }

            // Yaygın tarih formatlarını dene (ilk tercihler lokal formatlar)
            string[] formats = new[]
            {
        "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyy/MM/dd", "dd.MM.yyyy",
        "d.M.yyyy", "MM/dd/yyyy", "M/d/yyyy", "dd/MM/yyyy", "yyyyMMdd",
        "dd.MM.yyyy HH:mm:ss", "yyyy-MM-ddTHH:mm:ss"
    };

            if (DateTime.TryParseExact(s, formats, CultureInfo.CurrentCulture, DateTimeStyles.None, out var dt2))
            {
                result = dt2;
                return true;
            }

            if (DateTime.TryParse(s, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal, out var dt3))
            {
                result = dt3;
                return true;
            }

            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal, out var dt4))
            {
                result = dt4;
                return true;
            }

            return false;
        }

        private bool TryParseDecimal(object rawVal, out decimal result)
        {
            result = 0m;
            if (rawVal == null || rawVal == DBNull.Value) return false;

            if (rawVal is decimal dec) { result = dec; return true; }
            if (rawVal is double d) { result = Convert.ToDecimal(d); return true; }
            if (rawVal is int i) { result = i; return true; }
            if (rawVal is long l) { result = l; return true; }

            string s = rawVal.ToString().Trim();
            if (string.IsNullOrEmpty(s)) return false;

            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out var r1))
            {
                result = r1; return true;
            }
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var r2))
            {
                result = r2; return true;
            }

            string cleaned = s.Replace(".", "").Replace(",", ".");
            if (decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out var r3))
            {
                result = r3; return true;
            }

            return false;
        }


        private async void btnDbConnect_Click(object sender, EventArgs e) => await ConnectDbAsync();
        private async void btnImport_Click(object sender, EventArgs e) => await StartImportAsync();

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dlg = new()
            {
                Filter = "Excel Dosyası|*.xlsx;*.xlsm",
                Title = "Excel Dosyası Seçiniz"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
                txtFilePath.Text = dlg.FileName;
        }
    }
}
