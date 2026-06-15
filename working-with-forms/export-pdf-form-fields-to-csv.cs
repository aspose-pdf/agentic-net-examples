using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for form field access

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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create a CSV file to store the extracted form field data
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // Write CSV header line
                writer.WriteLine("FullName,PartialName,Name,Value,ReadOnly,Required,FieldType");

                // Iterate over all form fields in the document
                foreach (Field field in doc.Form.Fields)
                {
                    // Extract relevant properties; use null‑coalescing for Value which may be null
                    string fullName    = EscapeCsv(field.FullName ?? string.Empty);
                    string partialName = EscapeCsv(field.PartialName ?? string.Empty);
                    string name        = EscapeCsv(field.Name ?? string.Empty);
                    string value       = EscapeCsv(field.Value?.ToString() ?? string.Empty);
                    string readOnly    = field.ReadOnly.ToString();
                    string required    = field.Required.ToString();
                    string fieldType   = EscapeCsv(field.GetType().Name);

                    // Write a CSV record for the current field
                    writer.WriteLine($"{fullName},{partialName},{name},{value},{readOnly},{required},{fieldType}");
                }
            }
        }

        Console.WriteLine($"Form fields exported to '{outputCsv}'.");
    }

    // Helper method to escape commas, quotes, and line breaks in CSV fields
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
