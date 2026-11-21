using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by its name and cast to Field
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Ensure the field is visible on screen (do not set Invisible/NoView flags)
            // Remove the Print flag so the field is not printed.
            field.Flags = field.Flags & ~AnnotationFlags.Print;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field visibility updated and saved to '{outputPath}'.");
    }
}
