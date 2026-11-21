using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "renamed_fields.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Example: rename a specific form field from old naming to new naming.
            const string oldFieldName = "CustomerName";   // existing field name
            const string newFieldName = "cust_name";      // new naming convention

            // Retrieve the field by its current name. The Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Field (or use the 'as' operator) before accessing Field members.
            Field? field = doc.Form[oldFieldName] as Field;

            if (field != null)
            {
                // Rename the field by setting its Name property.
                field.Name = newFieldName;
                Console.WriteLine($"Field '{oldFieldName}' renamed to '{newFieldName}'.");
            }
            else
            {
                Console.WriteLine($"Field '{oldFieldName}' not found.");
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
