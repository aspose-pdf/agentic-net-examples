using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name. The Form indexer returns a WidgetAnnotation,
            // so we need to cast it to Aspose.Pdf.Forms.Field (or a derived field type).
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field in the document.");
                return;
            }

            // Mark the field as required so validation will fail if left empty
            field.Required = true;

            // Ensure the field is editable (optional)
            field.ReadOnly = false;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with required field saved to '{outputPath}'.");
    }
}
