using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_required.pdf";
        const string fieldName  = "MyTextField"; // name of the field to mark as required

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form and retrieve the field by its name
            // The Form indexer returns a WidgetAnnotation; cast it to Field.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field in the document.");
                return;
            }

            // Mark the field as required; validation will fail if left empty
            field.Required = true;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with required field: '{outputPath}'.");
    }
}
