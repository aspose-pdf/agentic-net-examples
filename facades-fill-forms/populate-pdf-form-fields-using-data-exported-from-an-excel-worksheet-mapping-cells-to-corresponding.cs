using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfTemplatePath = "TemplateForm.pdf";
        const string csvDataPath      = "Data.csv";   // CSV file with header row matching PDF field names
        const string outputPdfPath    = "FilledForm.pdf";

        // Verify files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(csvDataPath))
        {
            Console.Error.WriteLine($"CSV data file not found: {csvDataPath}");
            return;
        }

        // Load CSV data into a DataTable.
        DataTable dataTable = LoadCsvToDataTable(csvDataPath);

        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("No data found in the CSV file.");
            return;
        }

        // Use Aspose.Pdf.Facades.Form to fill the PDF form.
        using (Form pdfForm = new Form(pdfTemplatePath))
        {
            // Iterate over each row (e.g., each record) and fill the corresponding fields.
            // Column names must match the fully qualified field names in the PDF.
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    string fieldName  = col.ColumnName;               // PDF field name
                    string fieldValue = row[col]?.ToString() ?? "";   // Value from CSV cell

                    // Fill the field; ignore the return value (true if field exists).
                    pdfForm.FillField(fieldName, fieldValue);
                }
            }

            // Save the filled PDF to the desired output path.
            pdfForm.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }

    // Helper method to load a CSV file into a DataTable.
    // The first line is treated as column headers; subsequent lines are data rows.
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var dt = new DataTable();
        try
        {
            var lines = File.ReadAllLines(csvFilePath);
            if (lines.Length == 0)
                return dt; // empty table

            // Parse header line
            var headers = ParseCsvLine(lines[0]);
            foreach (var header in headers)
                dt.Columns.Add(header.Trim());

            // Parse data lines
            for (int i = 1; i < lines.Length; i++)
            {
                var fields = ParseCsvLine(lines[i]);
                // Ensure the row has the same number of columns as the header
                var row = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row[j] = j < fields.Count ? fields[j] : string.Empty;
                }
                dt.Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file: {ex.Message}");
        }
        return dt;
    }

    // Very simple CSV parser that handles commas and quoted fields.
    private static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        if (string.IsNullOrEmpty(line))
            return result;

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
                        // Check for escaped quote ""
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
                var field = line.Substring(start, i - start);
                result.Add(field);
                if (i < line.Length && line[i] == ',') i++;
            }
        }
        return result;
    }
}
