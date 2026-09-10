using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths and field data
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";
        const string fieldName = "myTextField";
        const string fieldValue = "Hello World";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field safely
            Field? field = doc.Form[fieldName] as Field;
            if (field != null)
            {
                // Set the field's value
                field.Value = fieldValue;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a text field.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}
