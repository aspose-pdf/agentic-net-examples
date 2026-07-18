using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_screen.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                AnnotationCollection annots = page.Annotations;

                // Delete ScreenAnnotation instances.
                // Iterate backwards to avoid index shifting when deleting.
                for (int i = annots.Count; i >= 1; i--)
                {
                    Annotation ann = annots[i];
                    if (ann is ScreenAnnotation)
                    {
                        annots.Delete(ann);
                    }
                }
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Screen annotations removed. Saved to '{outputPath}'.");
    }
}