using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

#nullable enable

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "template.pdf";
        const string csvFilePath      = "data.csv";   // Excel data exported as CSV
        const string mappingFilePath  = "mapping.json";
        const string outputFolder     = "FilledPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load column‑to‑field mapping from a JSON file.
        // Example content: { "ExcelColumnA": "PdfField1", "ExcelColumnB": "PdfField2" }
        // Guard against a missing or malformed mapping file.
        if (!File.Exists(mappingFilePath))
        {
            Console.Error.WriteLine($"Mapping file not found: {mappingFilePath}");
            return;
        }

        string mappingJson = File.ReadAllText(mappingFilePath);
        Dictionary<string, string>? rawMap = JsonSerializer.Deserialize<Dictionary<string, string>>(mappingJson);
        // If deserialization fails, fall back to an empty map to avoid null‑reference warnings.
        Dictionary<string, string> columnToFieldMap = rawMap ?? new Dictionary<string, string>();

        // Load CSV data into a DataTable (cross‑platform, no OleDb).
        DataTable dataTable = LoadCsvToDataTable(csvFilePath);

        // Iterate over each row and fill a new PDF using the mapping.
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow row = dataTable.Rows[i];

            // Bind the template PDF via the Form facade.
            using (Form pdfForm = new Form(templatePdfPath))
            {
                if (pdfForm == null)
                {
                    Console.Error.WriteLine($"Failed to load PDF template: {templatePdfPath}");
                    continue;
                }

                // Fill each mapped field.
                foreach (KeyValuePair<string, string> kvp in columnToFieldMap)
                {
                    string excelColumn = kvp.Key;
                    string pdfField    = kvp.Value;

                    // Guard against missing columns.
                    if (!dataTable.Columns.Contains(excelColumn))
                        continue;

                    // row[excelColumn] can be DBNull – treat it as empty string.
                    object? rawValue = row[excelColumn];
                    string fieldValue = rawValue is DBNull ? string.Empty : rawValue?.ToString() ?? string.Empty;

                    pdfForm.FillField(pdfField, fieldValue);
                }

                // Save the filled PDF – one file per CSV row.
                string outputPath = Path.Combine(outputFolder, $"filled_{i + 1}.pdf");
                pdfForm.Save(outputPath);
            }
        }

        Console.WriteLine("All PDFs have been generated.");
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. The first line is treated as the header row.
    /// Simple comma‑separated parsing; does not handle escaped commas or newlines inside quotes.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
        if (!File.Exists(csvFilePath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvFilePath}");
            return table;
        }

        using (var reader = new StreamReader(csvFilePath))
        {
            if (reader.EndOfStream)
                return table; // empty file

            // Read header line
            string headerLine = reader.ReadLine() ?? string.Empty;
            string[] headers = headerLine.Split(',');
            foreach (string header in headers)
            {
                // Trim whitespace and use string type for all columns
                table.Columns.Add(header.Trim(), typeof(string));
            }

            // Read data rows
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] values = line.Split(',');
                DataRow row = table.NewRow();
                int colCount = Math.Min(values.Length, table.Columns.Count);
                for (int i = 0; i < colCount; i++)
                {
                    row[i] = values[i].Trim();
                }
                table.Rows.Add(row);
            }
        }
        return table;
    }
}
