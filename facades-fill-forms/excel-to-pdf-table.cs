using System;
using System.IO;
using System.Data;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string csvPath = "input.csv"; // CSV file replaces the original XLSX
        const string pdfPath = "output.pdf";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load the CSV file into a DataTable (cross‑platform, no OleDb dependency)
        DataTable sourceTable = LoadCsvToDataTable(csvPath);

        // Filter rows – keep only rows where the column "Amount" is greater than 100.
        DataRow[] filteredRows = sourceTable.Select("Amount > 100");
        DataTable filteredTable = sourceTable.Clone(); // copy structure
        foreach (DataRow row in filteredRows)
        {
            filteredTable.ImportRow(row);
        }

        // Create a PDF document and import the filtered DataTable into a table.
        using (Document pdfDocument = new Document())
        {
            Page page = pdfDocument.Pages.Add();
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Import the DataTable. Column names are added as the first row.
            table.ImportDataTable(filteredTable, true, 0, 0);

            pdfDocument.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated: {pdfPath}");
    }

    // ---------------------------------------------------------------------
    // Simple CSV → DataTable parser (no external libraries required).
    // Assumes the first line contains column headers and values are comma
    // separated. Trims whitespace and skips empty lines.
    // ---------------------------------------------------------------------
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
