using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades; // Form class resides here

class Program
{
    static void Main()
    {
        const string csvPath      = "data.csv";          // CSV source (replaces Excel)
        const string templatePdf  = "template.pdf";      // PDF form template
        const string outputDir    = "FilledForms";       // Output folder

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"PDF template not found: {templatePdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load CSV data into a DataTable (first row = column names)
        DataTable sheet = LoadCsvToDataTable(csvPath);

        // Iterate over each data row and fill a new PDF form
        for (int rowIndex = 0; rowIndex < sheet.Rows.Count; rowIndex++)
        {
            DataRow row = sheet.Rows[rowIndex];
            string outputPdf = Path.Combine(outputDir, $"FilledForm_{rowIndex + 1}.pdf");

            // Use Form facade to bind the template PDF
            using (Form form = new Form(templatePdf))
            {
                // Map each column (field name) to the cell value
                foreach (DataColumn col in sheet.Columns)
                {
                    string fieldName  = col.ColumnName;               // Expected to match PDF field name
                    string fieldValue = row[col]?.ToString() ?? string.Empty;

                    // Fill the field; ignore result (true/false) for simplicity
                    form.FillField(fieldName, fieldValue);
                }

                // Save the filled PDF
                form.Save(outputPdf);
            }

            Console.WriteLine($"Created: {outputPdf}");
        }
    }

    // Helper: loads a CSV file into a DataTable. First line is treated as header.
    static DataTable LoadCsvToDataTable(string csvFilePath)
    {
        var dt = new DataTable();
        using (var reader = new StreamReader(csvFilePath))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Simple CSV split – works for basic comma‑separated values without escaped commas.
                var values = line.Split(',');
                if (isFirstLine)
                {
                    foreach (var header in values)
                        dt.Columns.Add(header.Trim());
                    isFirstLine = false;
                }
                else
                {
                    var row = dt.NewRow();
                    for (int i = 0; i < values.Length && i < dt.Columns.Count; i++)
                        row[i] = values[i].Trim();
                    dt.Rows.Add(row);
                }
            }
        }
        return dt;
    }
}
