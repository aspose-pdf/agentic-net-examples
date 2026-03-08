using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source CSV file, PDF form template and output folder
        const string csvPath = "data.csv";               // Changed from Excel to CSV
        const string templatePdf = "form_template.pdf";
        const string outputDir = "FilledForms";

        // Validate input files
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load CSV data into a DataTable
        DataTable dataTable = LoadCsvToDataTable(csvPath);

        // Iterate over each row and generate a filled PDF form
        int rowIndex = 1;
        foreach (DataRow row in dataTable.Rows)
        {
            string outputPath = Path.Combine(outputDir, $"FilledForm_{rowIndex}.pdf");
            FillPdfForm(templatePdf, outputPath, row);
            Console.WriteLine($"Saved: {outputPath}");
            rowIndex++;
        }
    }

    // Reads a CSV file (first line = headers) into a DataTable.
    // This replaces the previous OLE DB based Excel reader and removes the Windows‑only dependency.
    static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null) continue;
                // Simple CSV split – assumes no commas inside quoted fields.
                var values = line.Split(',');
                if (isFirstLine)
                {
                    foreach (var header in values)
                    {
                        dt.Columns.Add(header.Trim());
                    }
                    isFirstLine = false;
                }
                else
                {
                    var row = dt.NewRow();
                    for (int i = 0; i < dt.Columns.Count && i < values.Length; i++)
                    {
                        row[i] = values[i].Trim();
                    }
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }

    // Fills a PDF form using a DataRow where column names match PDF field names
    static void FillPdfForm(string templatePath, string outputPath, DataRow dataRow)
    {
        // Form facade works directly with file paths
        using (Form form = new Form(templatePath))
        {
            // Iterate through each column; column name must match a PDF field name (case‑sensitive)
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                string fieldName = column.ColumnName;
                string fieldValue = dataRow[column]?.ToString() ?? string.Empty;

                // Attempt to fill the field; ignore failures for missing fields
                try
                {
                    form.FillField(fieldName, fieldValue);
                }
                catch (Exception ex)
                {
                    // Log the issue but continue processing other fields
                    Console.Error.WriteLine($"Could not fill field '{fieldName}': {ex.Message}");
                }
            }

            // Save the filled PDF document
            form.Save(outputPath);
        }
    }
}
