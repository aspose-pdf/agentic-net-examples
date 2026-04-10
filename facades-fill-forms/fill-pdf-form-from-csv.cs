using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class PdfFormFiller
{
    static void Main(string[] args)
    {
        // Expected arguments: inputPdfPath inputCsvPath outputPdfPath
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: PdfFormFiller <inputPdf> <inputCsv> <outputPdf>");
            return;
        }

        string inputPdfPath   = args[0];
        string inputCsvPath   = args[1];
        string outputPdfPath  = args[2];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputCsvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {inputCsvPath}");
            return;
        }

        try
        {
            // Load CSV data into a DataTable (first row = column names)
            DataTable csvData = LoadCsvToDataTable(inputCsvPath);

            // Fill the PDF form using Aspose.Pdf.Facades.Form
            using (Form form = new Form(inputPdfPath))
            {
                // Iterate over each row (typically only one row for a single form)
                foreach (DataRow row in csvData.Rows)
                {
                    foreach (DataColumn column in csvData.Columns)
                    {
                        string fieldName  = column.ColumnName;               // CSV header must match PDF field name
                        string fieldValue = row[column]?.ToString() ?? string.Empty;
                        form.FillField(fieldName, fieldValue);
                    }
                }

                // Save the filled PDF to the specified output path
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form filled and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to read a CSV file into a DataTable.
    // The first line is treated as the header row.
    private static DataTable LoadCsvToDataTable(string csvPath)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(csvPath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var fields = ParseCsvLine(line);

                if (isFirstLine)
                {
                    // Create columns from header
                    foreach (var header in fields)
                    {
                        dt.Columns.Add(header.Trim());
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add data row
                    var dr = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count && i < fields.Length; i++)
                    {
                        dr[i] = fields[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }

    // Very simple CSV parser that handles commas inside quoted fields.
    private static string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        var value = string.Empty;
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote
                    value += '"';
                    i++; // skip next quote
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(value);
                value = string.Empty;
            }
            else
            {
                value += c;
            }
        }
        result.Add(value);
        return result.ToArray();
    }
}
