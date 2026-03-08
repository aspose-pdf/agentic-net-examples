using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class PdfFormFiller
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfTemplatePath = "template.pdf";
        const string dataCsvPath   = "data.csv";   // CSV file (can be exported from XLS/XLSX)
        const string outputPdfPath   = "filled_form.pdf";

        // Verify files exist
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }
        if (!File.Exists(dataCsvPath))
        {
            Console.Error.WriteLine($"Data file not found: {dataCsvPath}");
            return;
        }

        // Load CSV data into a DataTable
        DataTable dataTable = LoadCsvToDataTable(dataCsvPath);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("No data found in the CSV file.");
            return;
        }

        // Use the first row of the CSV for filling the form
        DataRow row = dataTable.Rows[0];

        // Create a Form facade bound to the PDF template
        using (Form pdfForm = new Form(pdfTemplatePath))
        {
            // Iterate over each column – column name must match the full field name in the PDF
            foreach (DataColumn column in dataTable.Columns)
            {
                string fieldName = column.ColumnName;               // expected to be the full field name
                string fieldValue = row[column]?.ToString() ?? "";

                // Check if the PDF actually contains this field
                bool fieldExists = false;
                foreach (string existingName in pdfForm.FieldNames)
                {
                    if (existingName.Equals(fieldName, StringComparison.Ordinal))
                    {
                        fieldExists = true;
                        break;
                    }
                }

                if (fieldExists)
                {
                    // Fill the field with the value from CSV
                    bool filled = pdfForm.FillField(fieldName, fieldValue);
                    if (!filled)
                    {
                        Console.Error.WriteLine($"Failed to fill field '{fieldName}'.");
                    }
                }
                else
                {
                    // Field not present – you may log or ignore
                    Console.WriteLine($"Field '{fieldName}' not found in the PDF form; skipping.");
                }
            }

            // Save the filled PDF
            pdfForm.Save(outputPdfPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdfPath}'.");
    }

    // Helper method to load a CSV file into a DataTable.
    // The first line is treated as column headers; subsequent lines are data rows.
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
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

                    // Simple CSV split – handles commas inside quoted fields.
                    var fields = ParseCsvLine(line);

                    if (isFirstLine)
                    {
                        // Create columns from header row
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
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file: {ex.Message}");
            return null;
        }
        return dt;
    }

    // Minimal CSV parser that respects quoted fields and escaped quotes.
    private static string[] ParseCsvLine(string line)
    {
        var result = new System.Collections.Generic.List<string>();
        bool inQuotes = false;
        var value = new System.Text.StringBuilder();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote "" inside a quoted field
                    value.Append('"');
                    i++; // skip next quote
                }
                else
                {
                    inQuotes = !inQuotes; // toggle quoting state
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(value.ToString());
                value.Clear();
            }
            else
            {
                value.Append(c);
            }
        }
        result.Add(value.ToString()); // add last field
        return result.ToArray();
    }
}
