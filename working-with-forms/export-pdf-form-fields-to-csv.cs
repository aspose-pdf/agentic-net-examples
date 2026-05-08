using System;
using System.IO;
using System.Text;
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
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document actually contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Prepare a StreamWriter for CSV output
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("FieldName,Value");

                // Iterate over all form fields in the document
                foreach (Field field in doc.Form.Fields)
                {
                    // FullName provides the fully qualified field name; fall back to Name if FullName is null
                    string fieldName = field.FullName ?? field.Name ?? string.Empty;

                    // Value may be null; convert to string safely
                    string fieldValue = field.Value?.ToString() ?? string.Empty;

                    // Escape double quotes by doubling them and wrap fields containing commas or quotes in quotes
                    fieldName = EscapeCsv(fieldName);
                    fieldValue = EscapeCsv(fieldValue);

                    writer.WriteLine($"{fieldName},{fieldValue}");
                }
            }

            Console.WriteLine($"Form fields exported to CSV: {outputCsvPath}");
        }
    }

    // Helper method to escape CSV fields according to RFC 4180
    private static string EscapeCsv(string input)
    {
        if (input.Contains("\""))
            input = input.Replace("\"", "\"\"");

        if (input.Contains(",") || input.Contains("\"") || input.Contains("\n") || input.Contains("\r"))
            return $"\"{input}\"";

        return input;
    }
}
