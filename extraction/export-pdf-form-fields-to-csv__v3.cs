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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Open the CSV file for writing
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // Write CSV header
                writer.WriteLine("FieldName,FieldValue");

                // Iterate over all form fields in the PDF
                foreach (Field field in doc.Form)
                {
                    // Retrieve the field name and its value as strings
                    string name = field.PartialName ?? string.Empty;
                    string value = field.Value?.ToString() ?? string.Empty;

                    // Escape special characters for CSV compliance
                    name = EscapeCsv(name);
                    value = EscapeCsv(value);

                    // Write the name/value pair to the CSV
                    writer.WriteLine($"{name},{value}");
                }
            }
        }

        Console.WriteLine($"Form fields exported to {outputCsv}");
    }

    // Helper method to escape commas, quotes, and newlines in CSV fields
    static string EscapeCsv(string text)
    {
        if (text.Contains("\"") || text.Contains(",") || text.Contains("\n"))
        {
            text = text.Replace("\"", "\"\"");
            return $"\"{text}\"";
        }
        return text;
    }
}