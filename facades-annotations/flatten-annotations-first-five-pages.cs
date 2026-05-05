using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor to flatten annotations on the first five pages
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Page range (Aspose.Pdf uses 1‑based indexing)
            int startPage = 1;
            int endPage   = 5;

            // Flatten all annotation types within the specified range.
            // Passing null for the annotation type array flattens every type.
            editor.FlatteningAnnotations(startPage, endPage, null);

            // Save the result
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations flattened on pages 1‑5. Output saved to '{outputPath}'.");
    }
}