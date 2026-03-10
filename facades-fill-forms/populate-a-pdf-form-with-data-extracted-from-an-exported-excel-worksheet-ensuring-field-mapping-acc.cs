using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class PdfFormFiller
{
    // Reads a CSV file (first line = column headers) into a DataTable.
    // This replaces the previous OLE DB based Excel loader to avoid the
    // Windows‑only System.Data.OleDb dependency.
    private static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        if (!File.Exists(csvFilePath))
            throw new FileNotFoundException($"CSV file not found: {csvFilePath}");

        var dt = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;

                // Simple CSV split – handles commas inside double quotes.
                var fields = ParseCsvLine(line);

                if (isFirstLine)
                {
                    // Create columns from header row.
                    foreach (var header in fields)
                    {
                        // Trim possible surrounding quotes/spaces.
                        var colName = header.Trim().Trim('"');
                        if (string.IsNullOrEmpty(colName))
                            colName = $"Column{dt.Columns.Count + 1}";
                        dt.Columns.Add(colName, typeof(string));
                    }
                    isFirstLine = false;
                }
                else
                {
                    // Add data rows.
                    var row = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string value = i < fields.Length ? fields[i] : string.Empty;
                        row[i] = value.Trim().Trim('"');
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }

    // Minimal CSV line parser that respects quoted fields.
    private static string[] ParseCsvLine(string line)
    {
        var result = new System.Collections.Generic.List<string>();
        bool inQuotes = false;
        var value = string.Empty;
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (c == '"')
            {
                // Toggle quote state. If double quote inside quoted field, treat as escaped quote.
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    value += '"';
                    i++; // skip escaped quote
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(value);
                value = string.Empty;
            }
            else
            {
                value += c;
            }
        }
        result.Add(value);
        return result.ToArray();
    }

    // Fills a PDF form using data from a DataTable.
    // Assumes each column name matches a PDF form field name exactly (case‑sensitive).
    private static void FillPdfForm(string pdfTemplatePath, string outputPdfPath, DataTable data)
    {
        if (!File.Exists(pdfTemplatePath))
            throw new FileNotFoundException($"PDF template not found: {pdfTemplatePath}");

        using (Form pdfForm = new Form(pdfTemplatePath))
        {
            var pdfFieldNames = pdfForm.FieldNames;

            if (data.Rows.Count == 0)
                throw new InvalidOperationException("The CSV file does not contain any data rows.");

            DataRow row = data.Rows[0]; // Fill with the first record only (example).

            foreach (DataColumn col in data.Columns)
            {
                string fieldName = col.ColumnName; // Expected to match PDF field name.
                string fieldValue = row[col]?.ToString() ?? string.Empty;

                bool fieldExists = false;
                foreach (string pdfField in pdfFieldNames)
                {
                    if (pdfField == fieldName)
                    {
                        fieldExists = true;
                        break;
                    }
                }

                if (fieldExists)
                {
                    pdfForm.FillField(fieldName, fieldValue);
                }
                else
                {
                    Console.WriteLine($"Warning: PDF does not contain field '{fieldName}'. Skipping.");
                }
            }

            pdfForm.Save(outputPdfPath);
        }
    }

    static void Main()
    {
        // Adjust paths as needed.
        const string csvPath = "DataExport.csv";               // Exported CSV (converted from Excel).
        const string pdfTemplatePath = "FormTemplate.pdf";    // PDF with AcroForm fields.
        const string outputPdfPath = "FormFilled.pdf";

        try
        {
            // Load data from CSV.
            DataTable csvData = LoadCsvToDataTable(csvPath);

            // Populate the PDF form.
            FillPdfForm(pdfTemplatePath, outputPdfPath, csvData);

            Console.WriteLine($"PDF form successfully saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
