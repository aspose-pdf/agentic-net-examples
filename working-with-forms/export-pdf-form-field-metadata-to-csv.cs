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
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false, System.Text.Encoding.UTF8))
            {
                // Write CSV header
                writer.WriteLine("FieldName,FieldType,DefaultValue");

                // Iterate over all form fields
                foreach (Field field in pdfDoc.Form.Fields)
                {
                    string fieldName = field.FullName ?? string.Empty;
                    string fieldType = field.GetType().Name;
                    string defaultValue = field.Value?.ToString() ?? string.Empty;

                    // Escape commas in values if necessary
                    fieldName = EscapeCsv(fieldName);
                    fieldType = EscapeCsv(fieldType);
                    defaultValue = EscapeCsv(defaultValue);

                    writer.WriteLine($"{fieldName},{fieldType},{defaultValue}");
                }
            }

            Console.WriteLine($"Form field metadata exported to '{outputCsvPath}'.");
        }
    }

    // Simple CSV escaping for commas and double quotes
    private static string EscapeCsv(string value)
    {
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}
