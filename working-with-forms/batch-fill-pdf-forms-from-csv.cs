using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfFiller
{
    static void Main()
    {
        // Paths – adjust as needed
        const string csvPath      = "data.csv";          // CSV file with data rows
        const string templatePath = "template.pdf";      // PDF template with form fields
        const string outputFolder = "FilledOutputs";    // Folder to store filled PDFs

        // Verify input files and create output directory
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {templatePath}");
            return;
        }
        Directory.CreateDirectory(outputFolder);

        // Read CSV – first line contains column headers
        string[] allLines = File.ReadAllLines(csvPath);
        if (allLines.Length < 2)
        {
            Console.Error.WriteLine("CSV file must contain at least a header line and one data row.");
            return;
        }

        // Parse header
        string[] headers = SplitCsvLine(allLines[0]);

        // Process each data row
        for (int rowIndex = 1; rowIndex < allLines.Length; rowIndex++)
        {
            string[] values = SplitCsvLine(allLines[rowIndex]);
            if (values.Length != headers.Length)
            {
                Console.Error.WriteLine($"Row {rowIndex} column count mismatch – skipping.");
                continue;
            }

            // Build a dictionary of column name → value for easy lookup
            var rowData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < headers.Length; i++)
                rowData[headers[i]] = values[i];

            // Load a fresh copy of the template for each row
            using (Document pdfDoc = new Document(templatePath))
            {
                // Fill form fields – field names are expected to match CSV column names
                foreach (var kvp in rowData)
                {
                    // Retrieve the field; the Form indexer returns a WidgetAnnotation, so cast to Field
                    Field? field = pdfDoc.Form[kvp.Key] as Field;
                    if (field != null)
                    {
                        // Most field types expose a Value property (object). Setting it works for TextBox, ComboBox, etc.
                        field.Value = kvp.Value;
                    }
                }

                // Determine output file name – use a column named "FileName" if present, otherwise use row index
                string outputFileName;
                if (rowData.TryGetValue("FileName", out string? customName) && !string.IsNullOrWhiteSpace(customName))
                {
                    // Ensure the name is a valid file name
                    foreach (char c in Path.GetInvalidFileNameChars())
                        customName = customName.Replace(c, '_');
                    outputFileName = $"{customName}.pdf";
                }
                else
                {
                    outputFileName = $"filled_{rowIndex}.pdf";
                }

                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Save the filled PDF – Document.Save(string) writes a PDF regardless of extension
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Saved filled PDF: {outputPath}");
            }
        }
    }

    // Simple CSV line splitter handling commas inside quoted fields
    private static string[] SplitCsvLine(string line)
    {
        var fields = new List<string>();
        bool inQuotes = false;
        System.Text.StringBuilder current = new System.Text.StringBuilder();

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }

            if (c == ',' && !inQuotes)
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
