using System;
using System.Data;
using System.Globalization;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";   // PDF with form fields
        const string outputPdfPath   = "filled.pdf";    // Resulting PDF
        const string dataTableCsv    = "data.csv";      // Example source for DataTable

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {pdfTemplatePath}");
            return;
        }

        // Load data into a DataTable (replace with actual data source as needed)
        DataTable table = LoadCsvToDataTable(dataTableCsv);
        if (table == null)
        {
            Console.Error.WriteLine("Failed to load data.");
            return;
        }

        // 1. Retrieve all field identifiers from the PDF using the Form facade
        string[] pdfFieldNames;
        using (var formFacade = new Form(pdfTemplatePath))
        {
            pdfFieldNames = formFacade.FieldNames; // Property, not a method
        }

        // 2. Adjust DataTable column names to exactly match PDF field identifiers
        AdjustColumnNames(table, pdfFieldNames);

        // 3. Fill the form using AutoFiller (the correct API for DataTable import)
        using (var filler = new AutoFiller())
        {
            filler.BindPdf(pdfTemplatePath);
            // Keep fields editable after filling (optional). By default AutoFiller flattens fields.
            filler.UnFlattenFields = pdfFieldNames; // preserve editability for all fields
            filler.ImportDataTable(table);

            // Save directly to a file stream (preferred over the obsolete overloads)
            using (var outStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
            {
                filler.Save(outStream);
            }
            filler.Close();
        }

        Console.WriteLine($"Form filled PDF saved to '{outputPdfPath}'.");
    }

    // Loads a CSV file into a DataTable (simple implementation; adapt as needed)
    static DataTable LoadCsvToDataTable(string csvPath)
    {
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return null;
        }

        var dt = new DataTable();
        using (var reader = new StreamReader(csvPath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                var values = line.Split(',');

                if (isFirstLine)
                {
                    // First line contains column headers
                    foreach (var header in values)
                        dt.Columns.Add(header.Trim(), typeof(string)); // treat all as string for simplicity
                    isFirstLine = false;
                }
                else
                {
                    // Subsequent lines contain data rows
                    // Ensure the row has the same number of columns as the header
                    var row = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count && i < values.Length; i++)
                    {
                        // Trim and keep as string; format numbers/dates beforehand if needed
                        row[i] = values[i].Trim();
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }

    // Renames DataTable columns to match PDF field names when possible
    static void AdjustColumnNames(DataTable table, string[] pdfFieldNames)
    {
        // Build a lookup for PDF fields (case‑insensitive, ignoring spaces)
        var pdfLookup = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var field in pdfFieldNames)
        {
            string key = field.Replace(" ", string.Empty).ToLowerInvariant();
            pdfLookup[key] = field; // store the exact field name
        }

        foreach (DataColumn column in table.Columns)
        {
            string colKey = column.ColumnName.Replace(" ", string.Empty).ToLowerInvariant();
            if (pdfLookup.TryGetValue(colKey, out string matchingPdfField))
            {
                // Rename column to the exact PDF field identifier
                column.ColumnName = matchingPdfField;
            }
            // If no match is found, the column name is left unchanged; it will be ignored during ImportDataTable.
        }
    }
}
