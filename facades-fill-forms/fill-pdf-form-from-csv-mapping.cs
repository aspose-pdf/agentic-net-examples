using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath = "Data.csv";               // CSV version of the Excel data
        const string pdfTemplatePath = "Template.pdf";
        const string outputPdfPath = "FilledForm.pdf";
        const string mappingConfigPath = "Mapping.json";

        // Load column‑to‑field mapping from a JSON file.
        // Example content: { "ExcelColumnA": "PdfField1", "ExcelColumnB": "PdfField2" }
        Dictionary<string, string> columnToFieldMap;
        try
        {
            string json = File.ReadAllText(mappingConfigPath);
            columnToFieldMap = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read mapping file: {ex.Message}");
            return;
        }

        // Load the CSV file into a DataTable.
        DataTable dataTable;
        try
        {
            dataTable = LoadCsvToDataTable(csvPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load CSV data: {ex.Message}");
            return;
        }

        if (dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("CSV file contains no data.");
            return;
        }

        // Use the first data row for filling the PDF form.
        DataRow dataRow = dataTable.Rows[0];

        // Open the PDF form using Aspose.Pdf.Facades.Form.
        using (Form pdfForm = new Form(pdfTemplatePath))
        {
            // Iterate over the mapping and fill corresponding fields.
            foreach (KeyValuePair<string, string> map in columnToFieldMap)
            {
                string csvColumn = map.Key;
                string pdfFieldName = map.Value;

                if (!dataTable.Columns.Contains(csvColumn))
                {
                    Console.WriteLine($"Warning: CSV column '{csvColumn}' not found – skipping.");
                    continue;
                }

                object cellValue = dataRow[csvColumn];
                string fieldValue = cellValue?.ToString() ?? string.Empty;

                // Fill the PDF field; ignore the boolean result (true if field found).
                pdfForm.FillField(pdfFieldName, fieldValue);
            }

            // Save the filled PDF.
            pdfForm.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Loads a CSV file into a DataTable. The first line is treated as the header row.
    /// </summary>
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        if (!File.Exists(csvFilePath))
            throw new FileNotFoundException($"CSV file not found: {csvFilePath}");

        var table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            // Read header line
            string headerLine = reader.ReadLine();
            if (headerLine == null)
                throw new InvalidDataException("CSV file is empty.");

            string[] headers = headerLine.Split(',');
            foreach (string header in headers)
                table.Columns.Add(header.Trim(), typeof(string));

            // Read data rows
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] values = line.Split(',');
                var row = table.NewRow();
                for (int i = 0; i < headers.Length && i < values.Length; i++)
                    row[i] = values[i].Trim();
                table.Rows.Add(row);
            }
        }
        return table;
    }
}
