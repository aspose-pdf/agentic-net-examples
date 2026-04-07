using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_highlights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                AnnotationCollection annots = page.Annotations;

                // Delete annotations in reverse order to keep indices valid
                for (int i = annots.Count; i >= 1; i--)
                {
                    Annotation ann = annots[i];
                    // Check if the annotation is a HighlightAnnotation
                    if (ann is HighlightAnnotation)
                    {
                        // Remove the highlight annotation
                        annots.Delete(i);
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations removed. Saved to '{outputPath}'.");
    }
}