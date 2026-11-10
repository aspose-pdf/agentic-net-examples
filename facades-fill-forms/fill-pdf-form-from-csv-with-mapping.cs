using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Path to the Excel‑like CSV file
    const string DataCsvPath = "data.csv";

    // Path to the JSON configuration that maps Excel column names to PDF field names
    const string MappingJsonPath = "fieldMapping.json";

    // Path to the PDF form template
    const string PdfTemplatePath = "template.pdf";

    // Output folder for the generated PDFs
    const string OutputFolder = "FilledPdfs";

    static void Main()
    {
        // Validate required files
        if (!File.Exists(DataCsvPath))
        {
            Console.Error.WriteLine($"Data file not found: {DataCsvPath}");
            return;
        }

        if (!File.Exists(MappingJsonPath))
        {
            Console.Error.WriteLine($"Mapping file not found: {MappingJsonPath}");
            return;
        }

        if (!File.Exists(PdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {PdfTemplatePath}");
            return;
        }

        // Load the column‑to‑field mapping
        Dictionary<string, string> columnToFieldMap = LoadMapping(MappingJsonPath);

        // Read CSV data (first line = header)
        List<Dictionary<string, string>> rows = LoadCsvData(DataCsvPath);

        // Ensure output directory exists
        Directory.CreateDirectory(OutputFolder);

        int rowIndex = 1;
        foreach (var row in rows)
        {
            // Create a fresh copy of the template for each row
            string outputPath = Path.Combine(OutputFolder, $"filled_{rowIndex}.pdf");

            // Use Aspose.Pdf.Facades.Form to fill fields
            using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form())
            {
                // Bind the template PDF
                pdfForm.BindPdf(PdfTemplatePath);

                // Iterate over the mapping and set field values
                foreach (var kvp in columnToFieldMap)
                {
                    string excelColumn = kvp.Key;
                    string pdfFieldName = kvp.Value;

                    // If the CSV row contains the column, fill the corresponding PDF field
                    if (row.TryGetValue(excelColumn, out string value))
                    {
                        // FillField expects a string value; nulls are converted to empty strings
                        pdfForm.FillField(pdfFieldName, value ?? string.Empty);
                    }
                }

                // Save the filled PDF
                pdfForm.Save(outputPath);
            }

            Console.WriteLine($"Row {rowIndex} saved to '{outputPath}'.");
            rowIndex++;
        }

        Console.WriteLine("All rows processed.");
    }

    // Loads the JSON mapping file into a dictionary
    static Dictionary<string, string> LoadMapping(string jsonPath)
    {
        string jsonContent = File.ReadAllText(jsonPath);
        // The JSON should represent an object where property names are Excel columns
        // and property values are the corresponding PDF field names.
        return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
    }

    // Loads CSV data into a list of dictionaries (one per row)
    static List<Dictionary<string, string>> LoadCsvData(string csvPath)
    {
        var rows = new List<Dictionary<string, string>>();
        using (var reader = new StreamReader(csvPath))
        {
            // Read header line
            string headerLine = reader.ReadLine();
            if (headerLine == null)
                return rows; // empty file

            string[] headers = SplitCsvLine(headerLine);

            // Read each subsequent line as a data row
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = SplitCsvLine(line);
                var rowDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < headers.Length && i < fields.Length; i++)
                {
                    rowDict[headers[i]] = fields[i];
                }
                rows.Add(rowDict);
            }
        }
        return rows;
    }

    // Simple CSV splitter that handles commas inside quoted fields
    static string[] SplitCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();

        foreach (char c in line)
        {
            if (c == '\"')
            {
                inQuotes = !inQuotes;
                continue;
            }

            if (c == ',' && !inQuotes)
            {
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        result.Add(current.ToString());
        return result.ToArray();
    }
}