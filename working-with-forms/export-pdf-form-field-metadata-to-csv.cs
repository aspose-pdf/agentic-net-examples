using System;
using System.IO;
using System.Linq;
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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document contains a form with fields
            var form = doc.Form;
            if (form == null || form.Fields == null || !form.Fields.Any())
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Create the CSV file inside a using block to ensure proper disposal
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                // Write CSV header
                writer.WriteLine("FieldName,FieldType,DefaultValue");

                // Iterate over each form field via the Fields collection
                foreach (Field field in form.Fields)
                {
                    // Full field name
                    string name = field.Name ?? string.Empty;

                    // Determine field type via runtime type name (e.g., TextBoxField, CheckBoxField)
                    string type = field.GetType().Name;

                    // Retrieve the field's current/value which serves as the default for auditing purposes
                    string defaultValue = field.Value?.ToString() ?? string.Empty;

                    // Escape CSV values that may contain commas or quotes
                    name = EscapeCsv(name);
                    type = EscapeCsv(type);
                    defaultValue = EscapeCsv(defaultValue);

                    writer.WriteLine($"{name},{type},{defaultValue}");
                }
            }

            Console.WriteLine($"Form field metadata exported to '{outputCsv}'.");
        }
    }

    // Helper method to escape CSV fields containing commas, quotes, or line breaks
    static string EscapeCsv(string value)
    {
        if (value.Contains('"'))
            value = value.Replace("\"", "\"\"");
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
            value = $"\"{value}\"";
        return value;
    }
}
