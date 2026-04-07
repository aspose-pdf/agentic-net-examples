using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "form_fields.csv";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load PDF document (dispose deterministically)
        using (Document doc = new Document(inputPdf))
        {
            // Check that the PDF contains a form with fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Create CSV file for output
            using (StreamWriter writer = new StreamWriter(outputCsv, false, System.Text.Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("FieldName,Value");

                // Iterate over each form field
                foreach (Field field in doc.Form)
                {
                    // Field name (full hierarchical name)
                    string name = EscapeCsv(field.FullName);

                    // Field value (may be null)
                    string value = EscapeCsv(field.Value?.ToString() ?? string.Empty);

                    // Write a CSV line
                    writer.WriteLine($"{name},{value}");
                }
            }
        }

        Console.WriteLine($"Form fields exported to '{outputCsv}'.");
    }

    // Helper to escape commas, quotes and line breaks according to CSV rules
    static string EscapeCsv(string input)
    {
        if (input.Contains("\"") || input.Contains(",") || input.Contains("\n") || input.Contains("\r"))
        {
            string escaped = input.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return input;
    }
}