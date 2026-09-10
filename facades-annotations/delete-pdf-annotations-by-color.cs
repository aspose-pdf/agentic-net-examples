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
        const string outputPath = "output_filtered.pdf";

        // Target RGB color to match (example: pure red)
        const int targetR = 255;
        const int targetG = 0;
        const int targetB = 0;
        // Aspose.Pdf.Color.FromRgb expects values in the range 0..1, so normalize the 0‑255 components
        Aspose.Pdf.Color targetColor = Aspose.Pdf.Color.FromRgb(targetR / 255.0, targetG / 255.0, targetB / 255.0);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (Facades) for loading and saving
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Access the underlying Document object
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate annotations in reverse order to allow safe deletion
                for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // Compare the annotation's color with the target RGB value
                    if (annotation.Color != null && annotation.Color.Equals(targetColor))
                    {
                        // Delete the matching annotation
                        page.Annotations.Delete(annIndex);
                    }
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Filtered PDF saved to '{outputPath}'.");
    }
}
