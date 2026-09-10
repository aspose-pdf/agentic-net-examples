using System;
using System.IO;
using System.Data;
using Aspose.Pdf.Facades;

class PdfFormFiller
{
    static void Main(string[] args)
    {
        // Expect three arguments: template PDF, source CSV (originally XLSX), output PDF
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: PdfFormFiller <template.pdf> <data.csv> <output.pdf>");
            return;
        }

        string pdfPath      = args[0];
        string csvPath      = args[1]; // CSV file that contains the data (replaces XLSX/OleDb)
        string outputPath   = args[2];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"Data file not found: {csvPath}");
            return;
        }

        // Load the CSV file into a DataTable
        DataTable dataTable = LoadCsvToDataTable(csvPath);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("No data found in the CSV file.");
            return;
        }

        // Use the first row of the table to fill the form
        DataRow row = dataTable.Rows[0];

        // Fill the PDF form using Aspose.Pdf.Facades.Form
        using (Form form = new Form(pdfPath))
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                string fieldName  = column.ColumnName; // PDF field name
                string fieldValue = row[column]?.ToString() ?? string.Empty; // Value from CSV
                form.FillField(fieldName, fieldValue);
            }

            // Save the filled PDF to the specified output path
            form.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }

    // Simple CSV → DataTable loader (comma‑separated, first line = headers)
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                // Split on commas – this is a minimal parser; it does not handle escaped commas or quotes.
                var values = line.Split(',');
                if (isFirstLine)
                {
                    // Create columns from header line
                    foreach (var header in values)
                    {
                        var colName = header.Trim();
                        if (string.IsNullOrEmpty(colName))
                            colName = $"Column{table.Columns.Count}"; // fallback name
                        table.Columns.Add(colName, typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    var row = table.NewRow();
                    for (int i = 0; i < table.Columns.Count && i < values.Length; i++)
                    {
                        row[i] = values[i].Trim();
                    }
                    table.Rows.Add(row);
                }
            }
        }
        return table;
    }
}
