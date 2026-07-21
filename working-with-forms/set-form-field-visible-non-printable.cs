using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "myField";   // replace with your field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm object
            Form form = doc.Form;

            // Verify that the field exists
            if (!form.HasField(fieldName))
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Retrieve the field (WidgetAnnotation)
            WidgetAnnotation field = form[fieldName];

            // -----------------------------------------------------------------
            // Set visibility to visible (clear Hidden/Invisible flags)
            // and make the field non‑printable (clear the Print flag).
            // -----------------------------------------------------------------
            field.Flags = (field.Flags & ~AnnotationFlags.Hidden)      // ensure not hidden
                        & ~AnnotationFlags.Invisible                     // ensure not invisible
                        & ~AnnotationFlags.Print;                       // suppress printing

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' updated: visible on screen, not printable. Saved to '{outputPath}'.");
    }
}