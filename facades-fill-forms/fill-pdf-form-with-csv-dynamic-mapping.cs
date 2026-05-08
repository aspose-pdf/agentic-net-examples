using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    // Load a simple JSON file that contains a dictionary of Excel column name -> PDF field name.
    // Example content: { "FirstName": "First_Name_Field", "LastName": "Last_Name_Field" }
    private static Dictionary<string, string> LoadMapping(string jsonPath)
    {
        string json = File.ReadAllText(jsonPath);
        return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }

    // Load CSV data into a DataTable. Assumes the first line contains column headers.
    private static DataTable LoadCsvToDataTable(string csvPath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvPath))
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
                    // Create columns using the header names.
                    foreach (var header in values)
                        table.Columns.Add(header.Trim(), typeof(string));
                    isFirstLine = false;
                }
                else
                {
                    var row = table.NewRow();
                    for (int i = 0; i < values.Length && i < table.Columns.Count; i++)
                        row[i] = values[i].Trim();
                    table.Rows.Add(row);
                }
            }
        }
        return table;
    }

    // Rename DataTable columns according to the mapping (Excel column -> PDF field name).
    private static void ApplyMapping(DataTable table, Dictionary<string, string> mapping)
    {
        foreach (var kvp in mapping)
        {
            string excelColumn = kvp.Key;
            string pdfField = kvp.Value;

            if (table.Columns.Contains(excelColumn))
            {
                // Change the column name to match the PDF field name.
                table.Columns[excelColumn].ColumnName = pdfField;
            }
        }
    }

    static void Main()
    {
        // Paths – adjust as needed.
        const string templatePdfPath = "template.pdf";
        const string outputPdfPath   = "filled_output.pdf";
        const string csvDataPath     = "data.csv";
        const string mappingJsonPath = "mapping.json";

        // Validate files.
        if (!File.Exists(templatePdfPath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdfPath}");
            return;
        }
        if (!File.Exists(csvDataPath))
        {
            Console.Error.WriteLine($"CSV data file not found: {csvDataPath}");
            return;
        }
        if (!File.Exists(mappingJsonPath))
        {
            Console.Error.WriteLine($"Mapping file not found: {mappingJsonPath}");
            return;
        }

        // Load mapping and data.
        var columnMapping = LoadMapping(mappingJsonPath);
        var dataTable = LoadCsvToDataTable(csvDataPath);
        ApplyMapping(dataTable, columnMapping);

        // Use AutoFiller to fill the PDF fields.
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePdfPath);          // Load the template PDF.
            filler.ImportDataTable(dataTable);        // Import the data.
            filler.Save(outputPdfPath);               // Save the filled PDF.
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }
}