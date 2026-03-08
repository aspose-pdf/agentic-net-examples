using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplate = "template.pdf";   // PDF with form fields
        const string csvFile     = "data.csv";      // CSV (exported from Excel) containing data
        const string outputPdf   = "filled.pdf";    // Resulting PDF

        if (!File.Exists(pdfTemplate))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplate}");
            return;
        }
        if (!File.Exists(csvFile))
        {
            Console.Error.WriteLine($"CSV file not found: {csvFile}");
            return;
        }

        // Load CSV data into a DataTable (first row = column names = field names)
        DataTable data = LoadCsvToDataTable(csvFile);
        if (data.Rows.Count == 0)
        {
            Console.Error.WriteLine("No data rows found in CSV file.");
            return;
        }

        // Use the first data row to fill the PDF form
        DataRow row = data.Rows[0];

        // Fill the PDF form using Aspose.Pdf.Facades.Form
        using (Form form = new Form(pdfTemplate))
        {
            foreach (DataColumn col in data.Columns)
            {
                string fieldName  = col.ColumnName;                     // Expected to match PDF field name
                string fieldValue = row[col]?.ToString() ?? string.Empty;
                form.FillField(fieldName, fieldValue);                  // Fill each field
            }

            // Save the filled PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdf}'.");
    }

    // Helper: reads a CSV file into a DataTable.
    // The first line is treated as column headers; subsequent lines are data rows.
    static DataTable LoadCsvToDataTable(string csvPath)
    {
        var dt = new DataTable();
        var lines = File.ReadAllLines(csvPath);
        if (lines.Length == 0)
            return dt; // empty table

        // Parse header line
        var headers = ParseCsvLine(lines[0]);
        foreach (var header in headers)
        {
            // Ensure unique column names
            string colName = string.IsNullOrWhiteSpace(header) ? "Column" + dt.Columns.Count : header;
            if (dt.Columns.Contains(colName))
                colName = colName + "_" + dt.Columns.Count;
            dt.Columns.Add(colName);
        }

        // Parse data lines
        for (int i = 1; i < lines.Length; i++)
        {
            var fields = ParseCsvLine(lines[i]);
            var dr = dt.NewRow();
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                dr[j] = j < fields.Length ? fields[j] : string.Empty;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    // Very simple CSV parser supporting commas and quoted fields.
    // It does not handle all edge‑cases but is sufficient for typical key/value data.
    static string[] ParseCsvLine(string line)
    {
        var result = new System.Collections.Generic.List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    // Escaped quote "" inside quoted field
                    current.Append('"');
                    i++; // skip next quote
                }
                else
                {
                    inQuotes = !inQuotes; // toggle quoting state
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        result.Add(current.ToString()); // add last field
        return result.ToArray();
    }
}
