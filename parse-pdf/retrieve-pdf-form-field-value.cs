using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for Field type

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // Path to the PDF containing the form
        const string fieldName = "FieldName";          // Name of the form field to retrieve

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name using the Form indexer.
            // The indexer returns a WidgetAnnotation; cast it to Field.
            Field field = doc.Form[fieldName] as Field;

            if (field == null)
            {
                Console.WriteLine($"Form field \"{fieldName}\" not found or is not a field type.");
            }
            else
            {
                // Output the field's value. Most field types expose a Value property.
                Console.WriteLine($"Field \"{fieldName}\" value: {field.Value}");
            }
        }
    }
}
