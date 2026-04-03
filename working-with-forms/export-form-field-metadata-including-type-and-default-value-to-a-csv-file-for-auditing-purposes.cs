using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "form_fields_audit.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Open a CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("FieldName,PartialName,FullName,Type,DefaultValue");

                // Iterate over all form fields using the Form.Fields collection
                foreach (Field field in doc.Form.Fields)
                {
                    // Basic field information
                    string fieldName = field.Name ?? string.Empty;               // field name (may be null)
                    string partialName = field.PartialName ?? string.Empty;     // partial name
                    string fullName = field.FullName ?? string.Empty;           // full name
                    string fieldType = field.GetType().Name;                    // .NET type of the field (e.g., TextBoxField, CheckBoxField)

                    // Default value (may be null)
                    string defaultValue = field.Value?.ToString() ?? string.Empty;

                    // Escape commas and quotes for CSV compliance
                    fieldName = EscapeCsv(fieldName);
                    partialName = EscapeCsv(partialName);
                    fullName = EscapeCsv(fullName);
                    fieldType = EscapeCsv(fieldType);
                    defaultValue = EscapeCsv(defaultValue);

                    // Write the CSV line
                    writer.WriteLine($"{fieldName},{partialName},{fullName},{fieldType},{defaultValue}");
                }
            }
        }

        Console.WriteLine($"Form field metadata exported to '{outputCsvPath}'.");
    }

    // Simple CSV escaping: wrap in quotes if the value contains a comma or quote,
    // and double any embedded quotes.
    static string EscapeCsv(string value)
    {
        if (value.Contains("\""))
            value = value.Replace("\"", "\"\"");

        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            return $"\"{value}\"";

        return value;
    }
}
