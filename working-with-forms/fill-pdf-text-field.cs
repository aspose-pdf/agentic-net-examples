using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";
        const string fieldName = "myField";          // Name of the text field to fill
        const string fieldValue = "Hello World";     // Value to set

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to a Field.
            Field? field = doc.Form[fieldName] as Field;

            if (field != null)
            {
                // Set the field's value
                field.Value = fieldValue;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with filled field to '{outputPath}'.");
    }
}
