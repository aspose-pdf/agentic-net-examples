using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class PdfFormFiller
{
    static void Main(string[] args)
    {
        // ---------------------------------------------------------------------
        // 1. Resolve file paths – allow an optional command‑line argument for the
        //    CSV file. If none is supplied we fall back to the default name.
        // ---------------------------------------------------------------------
        string dataFilePath = args.Length > 0 ? args[0] : "DataExport.csv";
        const string pdfTemplatePath = "FormTemplate.pdf"; // PDF form template
        const string outputPdfPath   = "FilledForm.pdf";   // Resulting filled PDF

        // Resolve relative paths against the executable directory so the sample
        // works when launched from any working directory.
        dataFilePath   = Path.GetFullPath(dataFilePath);
        string pdfPath = Path.GetFullPath(pdfTemplatePath);
        string outPath = Path.GetFullPath(outputPdfPath);

        // ---------------------------------------------------------------------
        // 2. Validate input files – give a clear, non‑exceptional message if a file
        //    cannot be found. This prevents an unhandled FileNotFoundException.
        // ---------------------------------------------------------------------
        if (!File.Exists(dataFilePath))
        {
            Console.Error.WriteLine($"CSV file not found: '{dataFilePath}'." );
            return;
        }
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF template not found: '{pdfPath}'." );
            return;
        }

        // ---------------------------------------------------------------------
        // 3. Load CSV data into a DataTable (first row as header).
        // ---------------------------------------------------------------------
        DataTable dataTable = LoadCsvToDataTable(dataFilePath);
        if (dataTable.Rows.Count == 0)
        {
            Console.Error.WriteLine("No data rows were found in the CSV file.");
            return;
        }

        // Use the first data row for filling the form. If multiple rows are needed,
        // iterate over dataTable.Rows accordingly.
        DataRow row = dataTable.Rows[0];

        // ---------------------------------------------------------------------
        // 4. Open the PDF form using Aspose.Pdf.Facades.Form (IDisposable).
        // ---------------------------------------------------------------------
        using (Form form = new Form(pdfPath))
        {
            // Retrieve all field names present in the PDF form.
            string[] pdfFieldNames = form.FieldNames;

            // Iterate over each column in the DataTable and map it to a PDF field.
            foreach (DataColumn column in dataTable.Columns)
            {
                string fieldName = column.ColumnName; // Expected to match PDF field name exactly.

                // Verify that the PDF actually contains this field.
                bool fieldExists = Array.Exists(pdfFieldNames, f => f.Equals(fieldName, StringComparison.Ordinal));
                if (!fieldExists)
                {
                    Console.WriteLine($"Warning: PDF does not contain field '{fieldName}'. Skipping.");
                    continue;
                }

                // Get the cell value as a string (null‑safe).
                string fieldValue = row[column] == null ? string.Empty : row[column].ToString();

                // Fill the field. FillField returns true if successful.
                bool filled = form.FillField(fieldName, fieldValue);
                if (!filled)
                {
                    Console.WriteLine($"Failed to fill field '{fieldName}'.");
                }
            }

            // Save the filled PDF to the desired output path.
            form.Save(outPath);
        }

        Console.WriteLine($"Form filled and saved to '{outPath}'.");
    }

    // ---------------------------------------------------------------------
    // Helper method to load a CSV file into a DataTable.
    // The first line is treated as column headers; subsequent lines are data rows.
    // ---------------------------------------------------------------------
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue; // safety
                var values = ParseCsvLine(line);

                if (isFirstLine)
                {
                    // Create columns based on header row
                    foreach (var header in values)
                    {
                        dt.Columns.Add(header.Trim());
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add data row
                    var row = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count && i < values.Length; i++)
                    {
                        row[i] = values[i];
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }

    // ---------------------------------------------------------------------
    // Very simple CSV line parser supporting commas and quoted fields.
    // ---------------------------------------------------------------------
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
                int start = i;
                while (i < line.Length)
                {
                    if (line[i] == '"')
                    {
                        // Check for escaped quote
                        if (i + 1 < line.Length && line[i + 1] == '"')
                        {
                            i += 2; // skip escaped quote
                            continue;
                        }
                        break; // end of quoted field
                    }
                    i++;
                }
                string field = line.Substring(start, i - start).Replace("\"\"", "\"");
                fields.Add(field);
                // Skip closing quote
                i++;
                // Skip following comma, if any
                if (i < line.Length && line[i] == ',') i++;
            }
            else
            {
                // Unquoted field
                int start = i;
                while (i < line.Length && line[i] != ',') i++;
                string field = line.Substring(start, i - start);
                fields.Add(field);
                if (i < line.Length && line[i] == ',') i++;
            }
        }
        return fields.ToArray();
    }
}
