using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "form_fields.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document actually contains a form
            if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Open a StreamWriter for the CSV output
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("FieldName,Value");

                // Iterate over all form fields
                foreach (Field field in pdfDoc.Form.Fields)
                {
                    // Get the fully qualified field name
                    string fieldName = field.FullName ?? string.Empty;

                    // Get the current value; handle nulls safely
                    string fieldValue = field.Value?.ToString() ?? string.Empty;

                    // Escape double quotes by doubling them and wrap fields containing commas or quotes in quotes
                    string escapedName = EscapeForCsv(fieldName);
                    string escapedValue = EscapeForCsv(fieldValue);

                    // Write the CSV line
                    writer.WriteLine($"{escapedName},{escapedValue}");
                }
            }

            Console.WriteLine($"Form fields exported to CSV: {outputCsvPath}");
        }
    }

    // Helper method to escape CSV fields according to RFC 4180
    private static string EscapeForCsv(string input)
    {
        if (input.Contains("\""))
            input = input.Replace("\"", "\"\"");

        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n") || input.Contains("\r"))
            return $"\"{input}\"";

        return input;
    }
}