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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(inputPdf))
            {
                // Verify that the PDF contains a form with fields
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Create a CSV file for output
                using (StreamWriter writer = new StreamWriter(outputCsv, false, System.Text.Encoding.UTF8))
                {
                    // Write CSV header
                    writer.WriteLine("FieldName,Value");

                    // Iterate over each form field
                    foreach (Field field in doc.Form.Fields)
                    {
                        // FullName provides the qualified field name
                        string name = field.FullName ?? string.Empty;

                        // Value may be null; convert to string safely
                        string value = field.Value?.ToString() ?? string.Empty;

                        // Escape CSV special characters
                        name = EscapeCsv(name);
                        value = EscapeCsv(value);

                        // Write the CSV line
                        writer.WriteLine($"{name},{value}");
                    }
                }

                Console.WriteLine($"Form data exported to '{outputCsv}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields according to RFC 4180
    static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            field = $"\"{field}\"";

        return field;
    }
}