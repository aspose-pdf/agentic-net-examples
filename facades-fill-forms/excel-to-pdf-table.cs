using System;
using System.IO;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string csvPath = "input.csv"; // CSV replaces the original Excel file
        const string pdfPath = "output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load the CSV file into a DataTable (cross‑platform, no OleDb)
        DataTable sourceTable = LoadCsvToDataTable(csvPath);

        // Filter rows (example: keep rows where the first column value > 100)
        DataTable filteredTable = sourceTable.Clone();
        foreach (DataRow row in sourceTable.Rows)
        {
            if (row[0] != DBNull.Value && int.TryParse(row[0].ToString(), out int val) && val > 100)
            {
                filteredTable.ImportRow(row);
            }
        }

        // Create a PDF document and import the filtered DataTable into a table
        using (Document pdfDocument = new Document())
        {
            Page page = pdfDocument.Pages.Add();

            Table pdfTable = new Table();
            int columnCount = filteredTable.Columns.Count;
            var widthsBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < columnCount; i++)
                widthsBuilder.Append("100 ");
            pdfTable.ColumnWidths = widthsBuilder.ToString().Trim();

            // Import data (include column names as the first row)
            pdfTable.ImportDataTable(filteredTable, true, 0, 0);
            page.Paragraphs.Add(pdfTable);

            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF saved to '{pdfPath}'.");
    }

    // Cross‑platform CSV → DataTable implementation (replaces OleDb based Excel reader)
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var values = line.Split(',');
                if (isFirstLine)
                {
                    foreach (var header in values)
                        dt.Columns.Add(header.Trim());
                    isFirstLine = false;
                }
                else
                {
                    var row = dt.NewRow();
                    for (int i = 0; i < values.Length && i < dt.Columns.Count; i++)
                        row[i] = values[i].Trim();
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }
}
