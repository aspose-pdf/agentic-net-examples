using System;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string csvPath = "data.csv";          // Path to the CSV file (exported from Excel)
        const string pdfTemplatePath = "template.pdf"; // Path to the PDF form template
        const string outputPdfPath = "filled.pdf";     // Path for the filled PDF

        // Load the first worksheet (now a CSV file) into a DataTable
        DataTable dataTable = LoadCsvToDataTable(csvPath);
        if (dataTable == null || dataTable.Columns.Count == 0)
        {
            Console.Error.WriteLine("No data found in the CSV file.");
            return;
        }

        // Map column names to field names and first row values to field values
        string[] fieldNames = dataTable.Columns.Cast<DataColumn>()
                                               .Select(col => col.ColumnName)
                                               .ToArray();

        string[] fieldValues = dataTable.Rows[0].ItemArray
                                                .Select(val => val?.ToString() ?? string.Empty)
                                                .ToArray();

        // Fill the PDF form fields using Aspose.Pdf.Facades.Form
        using (Form form = new Form(pdfTemplatePath))
        {
            // Fill all fields at once; the method returns the resulting PDF as a stream
            form.FillFields(fieldNames, fieldValues, out Stream resultStream);

            // Save the resulting PDF stream to a file
            using (FileStream fileOut = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
            {
                resultStream.CopyTo(fileOut);
            }

            // Dispose the temporary result stream
            resultStream.Dispose();
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }

    // Helper: reads a CSV file into a DataTable. The first line is treated as column headers.
    static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        if (!File.Exists(csvFilePath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvFilePath}");
            return null;
        }

        var dt = new DataTable();
        string[] lines = File.ReadAllLines(csvFilePath);
        if (lines.Length == 0)
            return dt; // empty table

        // Parse header line
        string[] headers = SplitCsvLine(lines[0]);
        foreach (string header in headers)
        {
            dt.Columns.Add(header.Trim());
        }

        // Parse remaining lines as rows
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue; // skip empty lines

            string[] fields = SplitCsvLine(lines[i]);
            DataRow row = dt.NewRow();
            for (int j = 0; j < dt.Columns.Count && j < fields.Length; j++)
            {
                row[j] = fields[j].Trim();
            }
            dt.Rows.Add(row);
        }

        return dt;
    }

    // Very simple CSV splitter that handles commas inside double quotes.
    static string[] SplitCsvLine(string line)
    {
        var fields = new System.Collections.Generic.List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                // Toggle in‑quotes state. If the next char is also a quote, treat it as an escaped quote.
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++; // skip escaped quote
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        fields.Add(current.ToString());
        return fields.ToArray();
    }
}
