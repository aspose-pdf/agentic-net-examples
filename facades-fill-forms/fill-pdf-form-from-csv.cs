using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input PDF form, input CSV data (originally XLSX), output PDF path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <program> <form.pdf> <data.csv> <output.pdf>");
            return;
        }

        string pdfFormPath   = args[0];
        string csvDataPath   = args[1]; // CSV file that contains the data (replaces XLSX)
        string outputPdfPath = args[2];

        if (!File.Exists(pdfFormPath))
        {
            Console.Error.WriteLine($"Form PDF not found: {pdfFormPath}");
            return;
        }

        if (!File.Exists(csvDataPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvDataPath}");
            return;
        }

        // Load data from the CSV file into a DataTable
        DataTable dataTable = LoadCsvToDataTable(csvDataPath);
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("No data found in the CSV file.");
            return;
        }

        // Use the first row to fill the form fields
        DataRow row = dataTable.Rows[0];

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(pdfFormPath))
        {
            // Iterate over each column; column name must match the full field name in the PDF
            foreach (DataColumn column in dataTable.Columns)
            {
                string fieldName = column.ColumnName;
                string fieldValue = row[column]?.ToString() ?? string.Empty;

                // Fill the field; ignore the return value (true if field exists)
                form.FillField(fieldName, fieldValue);
            }

            // Save the filled PDF to the specified output path
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdfPath}'.");
    }

    // Helper method to load a CSV file into a DataTable.
    // The first line is treated as the header row.
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var table = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            if (reader.EndOfStream)
                return table; // empty file

            // Read header line
            string headerLine = reader.ReadLine();
            if (headerLine == null)
                return table;

            string[] headers = SplitCsvLine(headerLine);
            foreach (var header in headers)
            {
                // Trim spaces and use string type for all columns (sufficient for form filling)
                table.Columns.Add(header.Trim(), typeof(string));
            }

            // Read data rows
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // skip empty lines

                string[] fields = SplitCsvLine(line);
                var row = table.NewRow();
                for (int i = 0; i < table.Columns.Count && i < fields.Length; i++)
                {
                    row[i] = fields[i].Trim();
                }
                table.Rows.Add(row);
            }
        }
        return table;
    }

    // Very simple CSV splitter that handles commas inside quoted fields.
    private static string[] SplitCsvLine(string line)
    {
        var fields = new System.Collections.Generic.List<string>();
        bool inQuotes = false;
        var current = new System.Text.StringBuilder();
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                // Toggle quote state; if double quote inside quoted field, treat as literal quote
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++; // skip the escaped quote
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
