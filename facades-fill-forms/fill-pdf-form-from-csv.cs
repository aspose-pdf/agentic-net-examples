using System;
using System.IO;
using System.Data;
using Aspose.Pdf.Facades;

class PdfFormFiller
{
    static void Main(string[] args)
    {
        // Expect three arguments: input PDF, input CSV (originally XLSX), output PDF
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: PdfFormFiller <inputPdf> <inputCsv> <outputPdf>");
            return;
        }

        string pdfPath = args[0];
        string csvPath = args[1];
        string outputPath = args[2];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load CSV data into a DataTable (first row contains column names)
        DataTable data = LoadCsvToDataTable(csvPath);
        if (data.Rows.Count == 0)
        {
            Console.Error.WriteLine("CSV file contains no data rows.");
            return;
        }

        // Use the first data row to fill the form fields
        DataRow row = data.Rows[0];

        // Open the PDF form, fill fields, and save the result
        using (Form form = new Form(pdfPath))
        {
            foreach (DataColumn col in data.Columns)
            {
                string fieldName = col.ColumnName;                     // Full field name must match PDF form field
                string fieldValue = row[col]?.ToString() ?? string.Empty;
                // Fill the field; ignore the boolean result (true if field found)
                form.FillField(fieldName, fieldValue);
            }

            // Save the filled PDF to the specified output path
            form.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }

    // Helper: reads a CSV file into a DataTable. The first line is treated as column headers.
    static DataTable LoadCsvToDataTable(string filePath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(filePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var values = ParseCsvLine(line);

                if (isFirstLine)
                {
                    // Create columns based on header row
                    foreach (var header in values)
                    {
                        string colName = string.IsNullOrWhiteSpace(header) ? "Column" + table.Columns.Count : header.Trim();
                        // All columns are treated as string for simplicity
                        table.Columns.Add(colName, typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add data rows
                    var row = table.NewRow();
                    for (int i = 0; i < table.Columns.Count && i < values.Length; i++)
                    {
                        row[i] = values[i];
                    }
                    table.Rows.Add(row);
                }
            }
        }
        return table;
    }

    // Very simple CSV line parser that handles commas inside quoted fields.
    static string[] ParseCsvLine(string line)
    {
        if (string.IsNullOrEmpty(line)) return new string[0];
        var result = new System.Collections.Generic.List<string>();
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
                        // Look ahead for escaped quote
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
                result.Add(field);
                // Skip closing quote
                i++;
                // Skip following comma, if any
                if (i < line.Length && line[i] == ',') i++;
            }
            else
            {
                // Unquoted field
                var start = i;
                while (i < line.Length && line[i] != ',') i++;
                var field = line.Substring(start, i - start).Trim();
                result.Add(field);
                if (i < line.Length && line[i] == ',') i++;
            }
        }
        return result.ToArray();
    }
}
