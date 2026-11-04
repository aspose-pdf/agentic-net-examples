using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "fields.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the document contains a form with fields
                if (doc.Form == null || !doc.Form.Fields.Any())
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Create (or overwrite) the CSV file
                using (StreamWriter writer = new StreamWriter(outputCsv, false))
                {
                    // CSV header
                    writer.WriteLine("FieldName,Value");

                    // Iterate over each form field
                    foreach (var field in doc.Form.Fields)
                    {
                        // Retrieve the field name (FullName is preferred, fallback to Name)
                        string name = field.FullName ?? field.Name ?? string.Empty;

                        // Retrieve the current value; convert nulls to empty string
                        string value = field.Value?.ToString() ?? string.Empty;

                        // Escape commas, quotes, and newlines for CSV compliance
                        name = EscapeCsv(name);
                        value = EscapeCsv(value);

                        writer.WriteLine($"{name},{value}");
                    }
                }

                Console.WriteLine($"Form fields exported to '{outputCsv}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields according to RFC 4180
    static string EscapeCsv(string text)
    {
        if (text.Contains("\"") || text.Contains(",") || text.Contains("\n") || text.Contains("\r"))
        {
            text = text.Replace("\"", "\"\"");
            return $"\"{text}\"";
        }
        return text;
    }
}
