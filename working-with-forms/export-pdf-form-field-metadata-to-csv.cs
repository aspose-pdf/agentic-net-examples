using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "form_fields_audit.csv";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use provided load)
        using (Document doc = new Document(inputPdf))
        {
            // Check if the document contains any form fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Create (or overwrite) the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsv, false))
            {
                // Write CSV header
                writer.WriteLine("FieldName,FieldType,DefaultValue");

                // Iterate over each form field
                foreach (Field field in doc.Form.Fields)
                {
                    // Field name (full hierarchical name)
                    string name = field.FullName ?? string.Empty;

                    // Runtime type of the field (e.g., TextBoxField, CheckBoxField)
                    string type = field.GetType().Name;

                    // Default/value of the field; may be null
                    string value = field.Value?.ToString() ?? string.Empty;

                    // Escape values that may contain commas or quotes
                    name = EscapeCsv(name);
                    type = EscapeCsv(type);
                    value = EscapeCsv(value);

                    // Write a CSV line
                    writer.WriteLine($"{name},{type},{value}");
                }
            }
        }

        Console.WriteLine($"Form field metadata exported to '{outputCsv}'.");
    }

    // Helper to escape CSV fields according to RFC 4180
    static string EscapeCsv(string s)
    {
        if (s.Contains("\""))
            s = s.Replace("\"", "\"\"");
        if (s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r"))
            s = $"\"{s}\"";
        return s;
    }
}