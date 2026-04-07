using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled_output.pdf";
        const string fieldName = "MyTextField";   // exact name of the text field in the PDF
        const string fieldValue = "Provided string value";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
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
                return;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with filled field to '{outputPath}'.");
    }
}
