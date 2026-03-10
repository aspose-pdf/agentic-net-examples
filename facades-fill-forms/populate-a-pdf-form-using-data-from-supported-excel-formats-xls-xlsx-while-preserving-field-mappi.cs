using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class PdfFormFiller
{
    // Reads a CSV file into a DataTable.
    // The first line must contain column headers that match the PDF form field names (case‑sensitive).
    static DataTable LoadCsv(string csvPath)
    {
        if (!File.Exists(csvPath))
            throw new FileNotFoundException($"CSV file not found: {csvPath}");

        var dt = new DataTable();
        using (var reader = new StreamReader(csvPath))
        {
            // Read header line
            string headerLine = reader.ReadLine();
            if (headerLine == null)
                throw new InvalidOperationException("CSV file is empty.");

            var headers = ParseCsvLine(headerLine);
            foreach (var h in headers)
                dt.Columns.Add(h);

            // Read data rows
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var fields = ParseCsvLine(line);
                // Ensure the row has the same number of columns as the header
                var row = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i < fields.Count)
                        row[i] = fields[i];
                }
                dt.Rows.Add(row);
            }
        }
        return dt;
    }

    // Very simple CSV line parser that handles commas and quoted fields.
    // It is sufficient for demonstration purposes and avoids external dependencies.
    static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        if (string.IsNullOrEmpty(line))
        {
            result.Add(string.Empty);
            return result;
        }

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
                        // Look ahead for escaped quote ""
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
                if (i < line.Length && line[i] == '"') i++;
                // Skip delimiter if present
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

    // Fills a PDF form using a single DataRow. The output file name is built from a base name and the row index.
    static void FillPdfForm(string templatePdfPath, DataRow row, string outputFolder, int rowIndex)
    {
        if (!File.Exists(templatePdfPath))
            throw new FileNotFoundException($"PDF template not found: {templatePdfPath}");

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        // Build output file name (e.g., FilledForm_1.pdf)
        string outputPdfPath = Path.Combine(outputFolder, $"FilledForm_{rowIndex + 1}.pdf");

        // Form class works directly with file paths; it does not need a separate Document instance.
        using (Form form = new Form(templatePdfPath))
        {
            // Iterate over each column; column name must match a field name in the PDF.
            foreach (DataColumn col in row.Table.Columns)
            {
                string fieldName = col.ColumnName;
                object valueObj = row[col];

                if (valueObj == null)
                    continue;

                string fieldValue = valueObj.ToString();
                form.FillField(fieldName, fieldValue);
            }

            // Save the filled PDF.
            form.Save(outputPdfPath);
        }

        Console.WriteLine($"Saved filled PDF: {outputPdfPath}");
    }

    static void Main()
    {
        // Paths – adjust as needed.
        const string csvPath = "Data.csv";                     // Input CSV file (exported from Excel)
        const string pdfTemplatePath = "FormTemplate.pdf";    // PDF form template
        const string outputFolder = "FilledForms";            // Folder where filled PDFs will be written

        try
        {
            // Load data from CSV.
            DataTable data = LoadCsv(csvPath);

            // Process each row – each row produces a separate filled PDF.
            for (int i = 0; i < data.Rows.Count; i++)
            {
                FillPdfForm(pdfTemplatePath, data.Rows[i], outputFolder, i);
            }

            Console.WriteLine("All forms have been populated successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
