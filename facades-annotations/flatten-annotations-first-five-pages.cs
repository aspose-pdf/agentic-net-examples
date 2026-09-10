using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_first5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (Facade) to manipulate annotations.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Get all possible annotation types.
            AnnotationType[] allTypes = (AnnotationType[])Enum.GetValues(typeof(AnnotationType));

            // Flatten annotations on pages 1 through 5 for all annotation types.
            editor.FlatteningAnnotations(1, 5, allTypes);

            // Save the resulting PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations on the first five pages have been flattened and saved to '{outputPath}'.");
    }
}