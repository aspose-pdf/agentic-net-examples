using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "form_fields.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // If the document has no form fields, exit early
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Create (or overwrite) the CSV file
                using (StreamWriter writer = new StreamWriter(csvPath, false))
                {
                    // Write CSV header line
                    writer.WriteLine("Name,FullName,PartialName,Value,FieldType,ReadOnly,Required");

                    // Iterate over each form field (Field is the base class for all AcroForm fields)
                    foreach (Field field in doc.Form)
                    {
                        // Gather required properties; use null‑coalescing for optional values
                        string name       = EscapeCsv(field.Name);
                        string fullName   = EscapeCsv(field.FullName);
                        string partial    = EscapeCsv(field.PartialName);
                        string value      = EscapeCsv(field.Value?.ToString() ?? string.Empty);
                        string fieldType  = EscapeCsv(field.GetType().Name);
                        string readOnly   = field.ReadOnly ? "True" : "False";
                        string required   = field.Required ? "True" : "False";

                        // Write a single CSV record
                        writer.WriteLine($"{name},{fullName},{partial},{value},{fieldType},{readOnly},{required}");
                    }
                }

                Console.WriteLine($"Form fields exported to '{csvPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
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