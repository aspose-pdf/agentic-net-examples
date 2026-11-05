using System;
using System.IO;
using System.Linq;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document actually contains a form
            if (doc.Form == null || doc.Form.Fields.Count() == 0)
            {
                Console.WriteLine("No form fields found in the document.");
                return;
            }

            // Create (or overwrite) the CSV file
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header
                writer.WriteLine("FieldName,FieldType,DefaultValue");

                // Iterate over all form fields
                foreach (Field field in doc.Form.Fields)
                {
                    // Field name (as defined in the PDF)
                    string name = field.PartialName ?? string.Empty;

                    // Determine the concrete field type name
                    string typeName = field.GetType().Name;

                    // Retrieve the default/value of the field. Most field types expose a Value property.
                    string defaultValue = field.Value?.ToString() ?? string.Empty;

                    // Escape commas and double quotes in CSV values
                    string csvName = $"\"{name.Replace("\"", "\"\"")}\"";
                    string csvType = $"\"{typeName.Replace("\"", "\"\"")}\"";
                    string csvDefault = $"\"{defaultValue.Replace("\"", "\"\"")}\"";

                    writer.WriteLine($"{csvName},{csvType},{csvDefault}");
                }
            }

            Console.WriteLine($"Form field metadata exported to '{outputCsvPath}'.");
        }
    }
}
