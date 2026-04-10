using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to required files
        const string configPath   = "fieldMap.json"; // JSON: {"ExcelColumn":"PdfField", ...}
        const string csvPath      = "data.csv";      // Simple CSV with header row
        const string templatePdf  = "template.pdf";  // PDF containing form fields
        const string outputPdf    = "filled.pdf";

        // Verify that all input files exist
        if (!File.Exists(configPath) ||
            !File.Exists(csvPath) ||
            !File.Exists(templatePdf))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the column‑to‑field mapping from the configuration file
        Dictionary<string, string> columnToFieldMap = LoadMapping(configPath);

        // Load the first data row from the CSV (header → values)
        Dictionary<string, string> rowData = LoadFirstCsvRow(csvPath);

        // Open the PDF form, fill fields, and save the result
        using (Form pdfForm = new Form(templatePdf))
        {
            foreach (KeyValuePair<string, string> kvp in rowData)
            {
                // Resolve the PDF field name using the mapping; fall back to the same name
                if (!columnToFieldMap.TryGetValue(kvp.Key, out string pdfFieldName))
                {
                    pdfFieldName = kvp.Key;
                }

                // Fill the field; FillField returns false if the field does not exist – we ignore it
                pdfForm.FillField(pdfFieldName, kvp.Value);
            }

            // Save the filled PDF
            pdfForm.Save(outputPdf);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPdf}'.");
    }

    // Reads a JSON file that maps Excel/CSV column names to PDF field names
    static Dictionary<string, string> LoadMapping(string path)
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }

    // Parses a CSV file and returns a dictionary of column → value for the first data row
    static Dictionary<string, string> LoadFirstCsvRow(string path)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        using (var reader = new StreamReader(path))
        {
            // Header line
            string headerLine = reader.ReadLine();
            if (headerLine == null) return result;
            string[] headers = SplitCsvLine(headerLine);

            // First data line
            string dataLine = reader.ReadLine();
            if (dataLine == null) return result;
            string[] values = SplitCsvLine(dataLine);

            int count = Math.Min(headers.Length, values.Length);
            for (int i = 0; i < count; i++)
            {
                string header = headers[i].Trim();
                string value  = values[i].Trim();
                result[header] = value;
            }
        }
        return result;
    }

    // Simple CSV splitter (does not handle quoted commas)
    static string[] SplitCsvLine(string line) => line.Split(',');
}