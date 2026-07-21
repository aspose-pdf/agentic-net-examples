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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Open a CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, System.Text.Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("FieldName,Value");

                // Iterate over all form fields
                foreach (Field field in doc.Form.Fields)
                {
                    string name = field.FullName ?? string.Empty;
                    string value = field.Value?.ToString() ?? string.Empty;

                    // Escape values that may contain commas, quotes or newlines
                    string escapedName = EscapeCsv(name);
                    string escapedValue = EscapeCsv(value);

                    writer.WriteLine($"{escapedName},{escapedValue}");
                }
            }

            Console.WriteLine($"Form fields exported to '{outputCsvPath}'.");
        }
    }

    // Helper to escape CSV fields according to RFC 4180
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