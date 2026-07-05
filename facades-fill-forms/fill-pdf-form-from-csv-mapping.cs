using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath           = "data.csv";          // Excel exported as CSV
        const string pdfTemplatePath   = "template.pdf";
        const string outputPdfPath     = "filled.pdf";
        const string mappingConfigPath = "fieldMap.json";

        // Load the column‑to‑field mapping (Excel column name → PDF field name)
        Dictionary<string, string> fieldMap = LoadMapping(mappingConfigPath) ?? new Dictionary<string, string>();
        if (fieldMap.Count == 0)
        {
            Console.Error.WriteLine("Mapping configuration is empty or invalid.");
            return;
        }

        // Load the CSV (exported from Excel) into a DataTable
        DataTable dataTable = LoadCsvToDataTable(csvPath);
        if (dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("No data rows found in the CSV file.");
            return;
        }

        // For demonstration we fill the PDF with the first data row
        DataRow dataRow = dataTable.Rows[0];

        // Use the Form facade to bind the template and fill fields
        using (Form form = new Form(pdfTemplatePath))
        {
            foreach (KeyValuePair<string, string> mapping in fieldMap)
            {
                string excelColumn = mapping.Key;   // column name in CSV (original Excel header)
                string pdfField    = mapping.Value; // corresponding PDF field name

                // Verify that the CSV column exists
                if (!dataTable.Columns.Contains(excelColumn))
                {
                    Console.Error.WriteLine($"Excel column \"{excelColumn}\" not found – skipping.");
                    continue;
                }

                // Retrieve the cell value and convert to string (null → empty)
                string fieldValue = dataRow[excelColumn]?.ToString() ?? string.Empty;

                // Fill the PDF field; FillField returns true if the field was found
                bool filled = form.FillField(pdfField, fieldValue);
                if (!filled)
                {
                    Console.Error.WriteLine($"PDF field \"{pdfField}\" not found – skipping.");
                }
            }

            // Save the filled PDF
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF successfully saved to \"{outputPdfPath}\".");
    }

    // Loads a JSON file that maps Excel column names to PDF field names.
    // Expected format: { "ExcelColumn1": "PdfField1", "ExcelColumn2": "PdfField2", ... }
    static Dictionary<string, string>? LoadMapping(string configPath)
    {
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Mapping file not found: {configPath}");
            return null;
        }

        using (FileStream fs = File.OpenRead(configPath))
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(fs);
        }
    }

    // Reads a CSV file into a DataTable. The first line is treated as column headers.
    // This replaces the previous OleDb‑based Excel reader and works cross‑platform.
    static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        if (!File.Exists(csvFilePath))
            throw new FileNotFoundException($"CSV file not found: {csvFilePath}");

        DataTable table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            // Read header line
            string? headerLine = reader.ReadLine();
            if (headerLine == null)
                throw new InvalidOperationException("CSV file is empty.");

            string[] headers = SplitCsvLine(headerLine);
            foreach (string header in headers)
            {
                // Trim spaces and ensure unique column names
                string colName = header.Trim();
                if (table.Columns.Contains(colName))
                    colName = MakeUniqueColumnName(table, colName);
                table.Columns.Add(colName, typeof(string));
            }

            // Read data rows
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (line == null) continue; // safety
                string[] values = SplitCsvLine(line);
                DataRow row = table.NewRow();
                for (int i = 0; i < table.Columns.Count && i < values.Length; i++)
                {
                    row[i] = values[i].Trim();
                }
                table.Rows.Add(row);
            }
        }
        return table;
    }

    // Simple CSV splitter that respects quoted fields.
    static string[] SplitCsvLine(string line)
    {
        var fields = new List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                // Toggle in‑quotes state; handle escaped double quotes "" inside a quoted field
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++; // skip the escaped quote
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        fields.Add(current.ToString());
        return fields.ToArray();
    }

    // Generates a unique column name if a duplicate is found.
    static string MakeUniqueColumnName(DataTable table, string baseName)
    {
        int suffix = 1;
        string newName = baseName + "_" + suffix;
        while (table.Columns.Contains(newName))
        {
            suffix++;
            newName = baseName + "_" + suffix;
        }
        return newName;
    }
}
