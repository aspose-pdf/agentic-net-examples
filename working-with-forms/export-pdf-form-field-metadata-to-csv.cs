using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";
        // Output CSV file for auditing
        const string outputCsvPath = "form_audit.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Open a StreamWriter for the CSV output
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("FieldName,FieldType,DefaultValue");

                // Iterate over all form fields
                foreach (Field field in pdfDocument.Form.Fields)
                {
                    // The field name (full name)
                    string fieldName = field.FullName ?? string.Empty;

                    // Determine the concrete field type (e.g., TextBoxField, CheckboxField, etc.)
                    string fieldType = field.GetType().Name;

                    // Retrieve the default/value of the field
                    string fieldValue = string.Empty;
                    try
                    {
                        // Most field types expose a Value property that holds the current/default value
                        fieldValue = field.Value?.ToString() ?? string.Empty;
                    }
                    catch
                    {
                        // If accessing Value throws, leave it empty
                        fieldValue = string.Empty;
                    }

                    // Escape commas and double quotes in CSV values by surrounding with double quotes
                    string escapedName = $"\"{fieldName.Replace("\"", "\"\"")}\"";
                    string escapedType = $"\"{fieldType.Replace("\"", "\"\"")}\"";
                    string escapedValue = $"\"{fieldValue.Replace("\"", "\"\"")}\"";

                    // Write the CSV line
                    writer.WriteLine($"{escapedName},{escapedType},{escapedValue}");
                }
            }
        }

        Console.WriteLine($"Form field metadata exported to '{outputCsvPath}'.");
    }
}
