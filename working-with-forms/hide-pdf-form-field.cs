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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field to access field members
            Field? field = doc.Form[fieldName] as Field;
            if (field != null)
            {
                // Hide the field by setting the Hidden flag on the underlying annotation
                field.Flags = AnnotationFlags.Hidden;
            }
            else
            {
                Console.WriteLine($"Form field '{fieldName}' not found.");
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden field to '{outputPath}'.");
    }
}
