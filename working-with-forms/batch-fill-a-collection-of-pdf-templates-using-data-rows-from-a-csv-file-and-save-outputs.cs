using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfFiller
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePath = "template.pdf";          // PDF with form fields
        const string csvPath      = "data.csv";             // CSV file with data rows
        const string outputFolder = "FilledPdfs";           // Folder for generated PDFs

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Read all lines from the CSV
        string[] allLines = File.ReadAllLines(csvPath);
        if (allLines.Length < 2)
        {
            Console.Error.WriteLine("CSV must contain a header line and at least one data row.");
            return;
        }

        // First line contains column names
        string[] headers = SplitCsvLine(allLines[0]);

        // Process each data row
        for (int rowIndex = 1; rowIndex < allLines.Length; rowIndex++)
        {
            string[] values = SplitCsvLine(allLines[rowIndex]);

            // Guard against mismatched column count
            if (values.Length != headers.Length)
            {
                Console.Error.WriteLine($"Row {rowIndex + 1} column count does not match header.");
                continue;
            }

            // Load a fresh copy of the template for each row
            using (Document pdfDoc = new Document(templatePath))
            {
                // Fill form fields where the field name matches a CSV header
                for (int i = 0; i < headers.Length; i++)
                {
                    string fieldName = headers[i];
                    string fieldValue = values[i];

                    // The Form indexer returns a WidgetAnnotation; cast it to Field safely
                    Field field = pdfDoc.Form[fieldName] as Field;
                    if (field != null)
                    {
                        // All field types inherit the Value property; assign the CSV value directly
                        field.Value = fieldValue;
                    }
                }

                // Determine output file name – use first column value if possible, otherwise row index
                string safeName = MakeFileNameSafe(values[0]);
                if (string.IsNullOrWhiteSpace(safeName))
                {
                    safeName = $"Row{rowIndex}";
                }

                string outputPath = Path.Combine(outputFolder, $"{safeName}.pdf");

                // Save the filled PDF
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Saved filled PDF: {outputPath}");
            }
        }
    }

    // Simple CSV line splitter handling commas; does not cover quoted commas for brevity
    private static string[] SplitCsvLine(string line)
    {
        return line.Split(new[] { ',' }, StringSplitOptions.None);
    }

    // Remove invalid filename characters
    private static string MakeFileNameSafe(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c.ToString(), string.Empty);
        }
        return name.Trim();
    }
}
