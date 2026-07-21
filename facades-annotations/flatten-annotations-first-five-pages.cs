using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_first5_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the annotation editor, bind the PDF, flatten annotations on pages 1‑5, and save.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // Get all possible annotation types to flatten every annotation.
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Flatten annotations from page 1 to page 5 (inclusive).
            editor.FlatteningAnnotations(1, 5, allTypes);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations on the first five pages have been flattened and saved to '{outputPath}'.");
    }
}