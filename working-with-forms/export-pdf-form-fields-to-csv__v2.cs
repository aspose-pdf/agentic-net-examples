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

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPdf))
            {
                // Check that the document actually contains a form
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Create a StreamWriter for the CSV output (standard .NET I/O)
                using (StreamWriter writer = new StreamWriter(outputCsv, false, System.Text.Encoding.UTF8))
                {
                    // Write CSV header
                    writer.WriteLine("FieldName,Value");

                    // Iterate over each form field
                    foreach (Field field in doc.Form)
                    {
                        // Retrieve the field's full name and its current value
                        string name = field.FullName ?? string.Empty;
                        string value = field.Value?.ToString() ?? string.Empty;

                        // Escape values that may contain commas, quotes or line breaks
                        name = EscapeCsv(name);
                        value = EscapeCsv(value);

                        // Write a CSV line
                        writer.WriteLine($"{name},{value}");
                    }
                }
            }

            Console.WriteLine($"Form fields exported successfully to '{outputCsv}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields according to RFC 4180
    static string EscapeCsv(string text)
    {
        if (text.Contains("\""))
            text = text.Replace("\"", "\"\"");

        if (text.Contains(",") || text.Contains("\"") || text.Contains("\n") || text.Contains("\r"))
            text = $"\"{text}\"";

        return text;
    }
}