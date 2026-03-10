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
        const string csvPath     = "Data.csv";          // CSV file containing the data (replaces Excel)
        const string pdfTemplate = "FormTemplate.pdf";   // PDF form with AcroForm fields
        const string outputPdf   = "FilledForm.pdf";     // Resulting PDF after filling

        // -----------------------------------------------------------------
        // 1. Load data from the CSV file into a DataTable
        // -----------------------------------------------------------------
        DataTable dataTable = LoadCsvToDataTable(csvPath);

        if (dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("CSV file contains no data.");
            return;
        }

        // -----------------------------------------------------------------
        // 2. Open the PDF form using the Aspose.Pdf.Facades.Form facade
        // -----------------------------------------------------------------
        using (Form form = new Form(pdfTemplate))
        {
            // -----------------------------------------------------------------
            // 3. Map each column (field name) to the corresponding form field
            //    and fill it with the value from the first row of the CSV.
            // -----------------------------------------------------------------
            DataRow row = dataTable.Rows[0]; // Use the first data row; adjust as needed

            foreach (DataColumn column in dataTable.Columns)
            {
                string fieldName  = column.ColumnName;               // Expected to match PDF field name (case‑sensitive)
                string fieldValue = row[column]?.ToString() ?? string.Empty;

                // Fill the field; FillField returns true if the field exists and is filled
                bool filled = form.FillField(fieldName, fieldValue);
                if (!filled)
                {
                    Console.WriteLine($"Warning: Field \"{fieldName}\" not found in the PDF form.");
                }
            }

            // -----------------------------------------------------------------
            // 4. Save the filled PDF – using the built‑in Save method (lifecycle rule)
            // -----------------------------------------------------------------
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form filled and saved to \"{outputPdf}\".");
    }

    // ---------------------------------------------------------------------
    // Helper: Load a simple CSV file into a DataTable.
    // Handles commas and quoted fields ("" for escaped quotes).
    // ---------------------------------------------------------------------
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var dt = new DataTable();
        if (!File.Exists(csvFilePath))
            return dt;

        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                    continue;

                var fields = ParseCsvLine(line);
                if (isFirstLine)
                {
                    foreach (var header in fields)
                        dt.Columns.Add(header);
                    isFirstLine = false;
                }
                else
                {
                    var row = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count && i < fields.Length; i++)
                        row[i] = fields[i];
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }

    // Simple CSV parser that respects quoted fields.
    private static string[] ParseCsvLine(string line)
    {
        var result = new List<string>();
        var value = new System.Text.StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote
                    value.Append('"');
                    i++; // Skip the next quote
                }
                else
                {
                    inQuotes = !inQuotes; // Toggle quoting state
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
        result.Add(value.ToString());
        return result.ToArray();
    }
}
