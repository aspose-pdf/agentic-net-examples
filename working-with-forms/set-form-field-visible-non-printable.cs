using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for AnnotationFlags
using Aspose.Pdf.Forms;      // for Field

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "myField";   // name of the form field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field from the form collection by its fully qualified name.
            // The Form indexer returns a WidgetAnnotation, so we cast it to Field.
            Field? field = doc.Form[fieldName] as Field;

            if (field != null)
            {
                // Make the field visible on screen but non‑printable.
                // Clear the Print flag while leaving other flags untouched.
                field.Flags = field.Flags & ~AnnotationFlags.Print;
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
