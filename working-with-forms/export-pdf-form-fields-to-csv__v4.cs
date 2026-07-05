using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormFieldsToCsv
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
            // Open a stream for the CSV output
            using (FileStream csvStream = new FileStream(outputCsvPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(csvStream))
            {
                // Write CSV header
                writer.WriteLine("FieldName,FieldType,DefaultValue");

                // Helper to escape CSV values
                string Escape(string s) => $"\"{s.Replace("\"", "\"\"")}\"";

                // Iterate over all form fields
                foreach (Field field in doc.Form.Fields)
                {
                    // Field name (use FullName if available, otherwise Name)
                    string fieldName = field.FullName ?? field.Name ?? string.Empty;

                    // Field type – use the concrete class name (e.g., TextBoxField, CheckBoxField)
                    string fieldType = field.GetType().Name;

                    // Default/value – may be null
                    string defaultValue = field.Value?.ToString() ?? string.Empty;

                    // Escape values for CSV
                    fieldName = Escape(fieldName);
                    fieldType = Escape(fieldType);
                    defaultValue = Escape(defaultValue);

                    // Write CSV line
                    writer.WriteLine($"{fieldName},{fieldType},{defaultValue}");
                }
            }
        }

        Console.WriteLine($"Form field metadata exported to '{outputCsvPath}'.");
    }
}
