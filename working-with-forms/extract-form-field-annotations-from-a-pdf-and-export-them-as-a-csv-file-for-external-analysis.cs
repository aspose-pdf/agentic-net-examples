using System;
using System.IO;
using System.Text;
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
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPdf))
            {
                // Check if the PDF contains any form fields
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Create (or overwrite) the CSV file
                using (StreamWriter writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
                {
                    // Write CSV header
                    writer.WriteLine("FullName,PartialName,MappingName,Value,FieldType,ReadOnly,Required");

                    // Iterate over each form field (Field) in the document
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Extract relevant properties
                        string fullName   = EscapeCsv(field.FullName ?? string.Empty);
                        string partialName = EscapeCsv(field.PartialName ?? string.Empty);
                        string mappingName = EscapeCsv(field.MappingName ?? string.Empty);
                        string value       = EscapeCsv(field.Value?.ToString() ?? string.Empty);
                        string fieldType   = EscapeCsv(field.GetType().Name);
                        string readOnly    = field.ReadOnly ? "True" : "False";
                        string required    = field.Required ? "True" : "False";

                        // Write a CSV line for the current field
                        writer.WriteLine($"{fullName},{partialName},{mappingName},{value},{fieldType},{readOnly},{required}");
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

    // Helper method to escape commas, quotes, and line breaks in CSV fields
    static string EscapeCsv(string input)
    {
        if (input.Contains('"'))
            input = input.Replace("\"", "\"\"");
        if (input.Contains(',') || input.Contains('"') || input.Contains('\n') || input.Contains('\r'))
            return $"\"{input}\"";
        return input;
    }
}
