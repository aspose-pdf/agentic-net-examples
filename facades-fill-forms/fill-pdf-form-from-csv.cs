using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Entry point: expects three arguments - input PDF form, input CSV data file, output PDF path
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputFormPdf> <inputDataCsv> <outputPdf>");
            return;
        }

        string inputPdfPath   = args[0];
        string inputCsvPath   = args[1];
        string outputPdfPath  = args[2];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF form not found at '{inputPdfPath}'.");
            return;
        }

        if (!File.Exists(inputCsvPath))
        {
            Console.Error.WriteLine($"Error: CSV data file not found at '{inputCsvPath}'.");
            return;
        }

        // Load data from the CSV file (first line must contain column headers "FieldName,FieldValue").
        DataTable dataTable = LoadCsvData(inputCsvPath);
        if (dataTable == null)
        {
            Console.Error.WriteLine("Error: Failed to load data from CSV.");
            return;
        }

        // Fill the PDF form using Aspose.Pdf.Facades.Form.
        // The Form class implements IDisposable, so we wrap it in a using block.
        using (Form form = new Form(inputPdfPath))
        {
            foreach (DataRow row in dataTable.Rows)
            {
                // Guard against missing columns.
                if (row.ItemArray.Length < 2) continue;

                string fieldName  = row[0]?.ToString() ?? string.Empty;
                string fieldValue = row[1]?.ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(fieldName)) continue;

                // Fill the field; FillField returns true if the field exists.
                bool filled = form.FillField(fieldName, fieldValue);
                if (!filled)
                {
                    Console.WriteLine($"Warning: Field '{fieldName}' not found in the PDF form.");
                }
            }

            // Save the filled PDF to the specified output path.
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdfPath}'.");
    }

    // Helper method to read a CSV file into a DataTable.
    // Expected format: two columns – FieldName,FieldValue (header row optional).
    private static DataTable LoadCsvData(string csvPath)
    {
        try
        {
            using (var reader = new StreamReader(csvPath))
            {
                var table = new DataTable();
                bool hasHeader = true; // assume first line contains headers
                string headerLine = reader.ReadLine();
                if (headerLine == null) return null;

                var headers = headerLine.Split(',');
                // If the first header does not look like a field name, treat the file as having no header.
                if (headers.Length == 2 &&
                    (string.Equals(headers[0].Trim(), "FieldName", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(headers[1].Trim(), "FieldValue", StringComparison.OrdinalIgnoreCase)))
                {
                    // Use the header names (or fallback to generic names).
                    table.Columns.Add(string.IsNullOrWhiteSpace(headers[0]) ? "FieldName" : headers[0].Trim(), typeof(string));
                    table.Columns.Add(string.IsNullOrWhiteSpace(headers[1]) ? "FieldValue" : headers[1].Trim(), typeof(string));
                }
                else
                {
                    // No header – treat the first line as data and create generic column names.
                    hasHeader = false;
                    table.Columns.Add("FieldName", typeof(string));
                    table.Columns.Add("FieldValue", typeof(string));
                    // Process the first line as a data row.
                    var firstValues = headerLine.Split(',');
                    var firstRow = table.NewRow();
                    firstRow[0] = firstValues.Length > 0 ? firstValues[0].Trim() : string.Empty;
                    firstRow[1] = firstValues.Length > 1 ? firstValues[1].Trim() : string.Empty;
                    table.Rows.Add(firstRow);
                }

                // Read remaining lines.
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var values = line.Split(',');
                    var row = table.NewRow();
                    row[0] = values.Length > 0 ? values[0].Trim() : string.Empty;
                    row[1] = values.Length > 1 ? values[1].Trim() : string.Empty;
                    table.Rows.Add(row);
                }

                return table;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV: {ex.Message}");
            return null;
        }
    }
}
