using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Create the annotation editor (lifecycle: create)
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document into the editor (lifecycle: load)
                editor.BindPdf(inputPath);

                // Determine the page range to extract from (1‑based indexing)
                int startPage = 1;
                int endPage = editor.Document.Pages.Count;

                // Extract all annotations of all types within the range.
                // Use Array.Empty<AnnotationType>() instead of a null literal to avoid CS8600 warning.
                IList<Annotation> annotations = editor.ExtractAnnotations(startPage, endPage, Array.Empty<AnnotationType>());

                // Output basic information about each annotation
                Console.WriteLine($"Total annotations found: {annotations.Count}");
                foreach (var annot in annotations)
                {
                    // Title is only available on TextAnnotation (and its derived types).
                    // Cast safely and fallback to an empty string for other annotation types.
                    string title = string.Empty;
                    if (annot is TextAnnotation textAnnot)
                    {
                        title = textAnnot.Title;
                    }

                    Console.WriteLine($"- Type: {annot.AnnotationType}, Title: {title}, Contents: {annot.Contents}");
                }

                // Save the (unchanged) PDF to a new file (lifecycle: save)
                editor.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
