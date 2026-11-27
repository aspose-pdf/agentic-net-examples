using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable in recent Aspose.PDF versions.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor.
            editor.BindPdf(inputPath);

            // Extract all annotations on page 6 (pages are 1‑based).
            // Passing null for the annotation types returns every type.
            // Cast null to the specific overload to avoid ambiguity.
            var pageAnnotations = editor.ExtractAnnotations(6, 6, (AnnotationType[])null);

            // Modify the second annotation (index 1) if it exists.
            if (pageAnnotations != null && pageAnnotations.Count > 1)
            {
                // Most annotation types expose a Title/Subject (e.g., TextAnnotation).
                // Cast to TextAnnotation to access those properties.
                if (pageAnnotations[1] is TextAnnotation textAnn)
                {
                    // "Author" in a PDF Text annotation maps to the Subject field.
                    textAnn.Subject = "Bob";   // Author
                    textAnn.Title   = "Draft"; // Title
                }
                else
                {
                    // For non‑text annotations we can still set generic properties if needed.
                    // As a fallback, set the Contents field to convey the same information.
                    pageAnnotations[1].Contents = "Bob - Draft";
                }
            }
            else
            {
                Console.WriteLine("Annotation at page 6 index 1 not found.");
            }

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
