using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Target RGB color to match (example: pure red)
        const int targetR = 255;
        const int targetG = 0;
        const int targetB = 0;
        // Aspose.Pdf.Color expects values in the range 0..1 (double), so normalize the 0‑255 components
        Aspose.Pdf.Color targetColor = Aspose.Pdf.Color.FromRgb(
            targetR / 255.0,
            targetG / 255.0,
            targetB / 255.0);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document directly (PdfAnnotationEditor does not exist)
        using (Document pdfDocument = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing is handled by the foreach iterator)
            foreach (Page page in pdfDocument.Pages)
            {
                AnnotationCollection annots = page.Annotations;

                // Iterate backwards so that deletions do not affect the loop index
                for (int i = annots.Count; i >= 1; i--)
                {
                    Annotation annot = annots[i];
                    // Compare the annotation color with the target RGB value
                    if (annot.Color != null && annot.Color.Equals(targetColor))
                    {
                        // Delete the matching annotation
                        annots.Delete(i);
                    }
                }
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Annotations with the specified color have been removed. Output saved to '{outputPath}'.");
    }
}
