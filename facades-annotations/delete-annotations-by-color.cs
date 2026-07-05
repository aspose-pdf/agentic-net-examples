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
        const string outputPath = "output_filtered.pdf";

        // Target RGB color to match (example: pure red)
        Aspose.Pdf.Color targetColor = Aspose.Pdf.Color.FromArgb(255, 0, 0);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (facade) to work with annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Access the underlying Document object
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                AnnotationCollection annotations = doc.Pages[pageIndex].Annotations;

                // Iterate backwards to safely delete by index
                for (int annIndex = annotations.Count; annIndex >= 1; annIndex--)
                {
                    Annotation annotation = annotations[annIndex];

                    // Compare annotation color with the target RGB value
                    if (annotation.Color != null && annotation.Color.Equals(targetColor))
                    {
                        // Delete the matching annotation
                        annotations.Delete(annIndex);
                    }
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
    }
}