using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class PdfFormFiller
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath      = "Data.csv";          // CSV file with data (replaces Excel)
        const string templatePdf  = "FormTemplate.pdf";   // PDF form template
        const string outputDir    = "FilledForms";

        // Verify input files
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"PDF template not found: {templatePdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load CSV data into a DataTable
        DataTable dataTable = LoadCsvToDataTable(csvPath);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("No data found in the CSV file.");
            return;
        }

        // Process each row – one filled PDF per row
        for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
        {
            DataRow row = dataTable.Rows[rowIndex];

            // Create a Form facade bound to the template PDF
            Form form = new Form(templatePdf);

            // Fill each field using the column name as the field name
            foreach (DataColumn column in dataTable.Columns)
            {
                string fieldName  = column.ColumnName;                     // Must match PDF field name (case‑sensitive)
                string fieldValue = row[column]?.ToString() ?? string.Empty;

                // Attempt to fill the field; ignore result for optional fields
                try
                {
                    form.FillField(fieldName, fieldValue);
                }
                catch (Exception ex)
                {
                    // Log but continue – some fields may not exist in the PDF
                    Console.Error.WriteLine($"Row {rowIndex + 1}: Unable to fill field '{fieldName}'. {ex.Message}");
                }
            }

            // Save the filled PDF – one file per row
            string outputPath = Path.Combine(outputDir, $"Filled_{rowIndex + 1}.pdf");
            try
            {
                form.Save(outputPath);
                Console.WriteLine($"Row {rowIndex + 1}: Saved filled form to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Row {rowIndex + 1}: Failed to save PDF. {ex.Message}");
            }
            finally
            {
                // Release resources held by the Form facade
                form.Close();
            }
        }
    }

    // Helper: loads a CSV file (first line = headers) into a DataTable.
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        if (!File.Exists(csvFilePath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvFilePath}");
            return null;
        }

        var dt = new DataTable();
        try
        {
            using (var reader = new StreamReader(csvFilePath))
            {
                bool isFirstLine = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) continue;

                    // Simple CSV split – handles commas inside quotes
                    var fields = ParseCsvLine(line);

                    if (isFirstLine)
                    {
                        // Create columns
                        foreach (var header in fields)
                        {
                            dt.Columns.Add(header.Trim());
                        }
                        isFirstLine = false;
                    }
                    else
                    {
                        // Add row data
                        var row = dt.NewRow();
                        for (int i = 0; i < dt.Columns.Count && i < fields.Length; i++)
                        {
                            row[i] = fields[i];
                        }
                        dt.Rows.Add(row);
                    }
                }
            }
            return dt;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file: {ex.Message}");
            return null;
        }
    }

    // Minimal CSV parser that respects quoted fields and escaped quotes.
    private static string[] ParseCsvLine(string line)
    {
        if (string.IsNullOrEmpty(line))
            return new string[0];

        var fields = new System.Collections.Generic.List<string>();
        int i = 0;
        while (i < line.Length)
        {
            if (line[i] == '"')
            {
                // Quoted field
                i++; // skip opening quote
                var start = i;
                var sb = new System.Text.StringBuilder();
                while (i < line.Length)
                {
                    if (line[i] == '"')
                    {
                        // Look ahead for escaped quote
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            sb.Append('"');
                            i += 2; // skip escaped quote
                        }
                        else
                        {
                            i++; // closing quote
                            break;
                        }
                    }
                    else
                    {
                        sb.Append(line[i]);
                        i++;
                    }
                }
                fields.Add(sb.ToString());
                // Skip optional comma after quoted field
                if (i < line.Length && line[i] == ',') i++;
            }
            else
            {
                // Unquoted field
                var start = i;
                while (i < line.Length && line[i] != ',') i++;
                var field = line.Substring(start, i - start);
                fields.Add(field);
                if (i < line.Length && line[i] == ',') i++;
            }
        }
        return fields.ToArray();
    }
}
