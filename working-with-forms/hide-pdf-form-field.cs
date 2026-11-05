using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_hidden_field.pdf";
        const string fieldName = "myField"; // name of the form field to hide

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name and cast to Field
            Field? field = doc.Form[fieldName] as Field;
            if (field != null)
            {
                // Hide the field so it does not appear when the PDF is rendered
                // Visibility is controlled via the Flags property using AnnotationFlags
                field.Flags = AnnotationFlags.Hidden;
            }
            else
            {
                Console.WriteLine($"Field '{fieldName}' not found or is not a form field.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden field to '{outputPath}'.");
    }
}
