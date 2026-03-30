using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class CsvToPdfMapper
{
    public static void Main()
    {
        const string csvPath = "data.csv";               // CSV file instead of Excel
        const string pdfTemplatePath = "template.pdf";
        const string mappingPath = "mapping.txt";

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(mappingPath))
        {
            Console.Error.WriteLine($"Mapping file not found: {mappingPath}");
            return;
        }

        // Load column‑to‑field mapping from a simple key=value text file
        Dictionary<string, string> columnToField = LoadMapping(mappingPath);

        // Load CSV data (first row = headers)
        List<Dictionary<string, string>> rows = LoadCsvData(csvPath);

        int rowIndex = 1; // start counting at 1 for output file naming
        foreach (var row in rows)
        {
            using (Document pdfDoc = new Document(pdfTemplatePath))
            {
                using (Form pdfForm = new Form(pdfDoc))
                {
                    // Fill fields based on the mapping
                    foreach (var kvp in columnToField)
                    {
                        string excelColumnName = kvp.Key;   // column name from CSV header
                        string pdfFieldName = kvp.Value;    // target PDF field name

                        if (row.TryGetValue(excelColumnName, out string cellValue))
                        {
                            pdfForm.FillField(pdfFieldName, cellValue ?? string.Empty);
                        }
                    }

                    // Save the filled PDF – filename based on the row number
                    string outputFileName = $"filled_{rowIndex}.pdf";
                    pdfDoc.Save(outputFileName);
                }
            }

            Console.WriteLine($"Row {rowIndex} saved to filled_{rowIndex}.pdf");
            rowIndex++;
        }
    }

    // Reads a simple key=value mapping file (ignores empty lines and comments starting with '#')
    private static Dictionary<string, string> LoadMapping(string path)
    {
        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue;

            var parts = line.Split(new[] { '=' }, 2);
            if (parts.Length == 2)
            {
                var excelColumn = parts[0].Trim();
                var pdfField = parts[1].Trim();
                if (!string.IsNullOrEmpty(excelColumn) && !string.IsNullOrEmpty(pdfField))
                    map[excelColumn] = pdfField;
            }
        }
        return map;
    }

    // Loads CSV data into a list of dictionaries where each dictionary represents a row
    // and maps column header -> cell value.
    private static List<Dictionary<string, string>> LoadCsvData(string csvPath)
    {
        var rows = new List<Dictionary<string, string>>();
        using (var reader = new StreamReader(csvPath))
        {
            // Read header line
            string headerLine = reader.ReadLine();
            if (headerLine == null)
                return rows; // empty file

            var headers = ParseCsvLine(headerLine);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = ParseCsvLine(line);
                var rowDict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < headers.Count; i++)
                {
                    string header = headers[i];
                    string value = i < values.Count ? values[i] : string.Empty;
                    rowDict[header] = value;
                }
                rows.Add(rowDict);
            }
        }
        return rows;
    }

    // Very simple CSV parser that handles commas and quoted fields ("...")
    private static List<string> ParseCsvLine(string line)
    {
        var fields = new List<string>();
        if (string.IsNullOrEmpty(line))
            return fields;

        int i = 0;
        while (i < line.Length)
        {
            if (line[i] == '"')
            {
                // Quoted field
                i++; // skip opening quote
                var start = i;
                while (i < line.Length)
                {
                    if (line[i] == '"')
                    {
                        // Look ahead for escaped quote "" 
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            i += 2; // skip escaped quote
                        }
                        else
                        {
                            break; // end of quoted field
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
                var field = line.Substring(start, i - start).Replace("\"\"", "\"");
                fields.Add(field);
                // Skip closing quote
                if (i < line.Length && line[i] == '"') i++;
                // Skip following comma
                if (i < line.Length && line[i] == ',') i++;
            }
            else
            {
                // Unquoted field
                var start = i;
                while (i < line.Length && line[i] != ',') i++;
                var field = line.Substring(start, i - start).Trim();
                fields.Add(field);
                if (i < line.Length && line[i] == ',') i++;
            }
        }
        return fields;
    }
}
