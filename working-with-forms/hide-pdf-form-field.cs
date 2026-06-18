using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF containing the form field
        const string outputPath = "output_hidden.pdf";
        const string fieldName = "myTextField"; // Fully‑qualified name of the field to hide

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation. Cast it to Field (which derives from WidgetAnnotation).
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field \"{fieldName}\" not found or is not a form field.");
                return;
            }

            // Hide the widget by setting the Hidden flag.
            field.Flags |= AnnotationFlags.Hidden; // make the field invisible on the page

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field \"{fieldName}\" hidden and saved to '{outputPath}'.");
    }
}
