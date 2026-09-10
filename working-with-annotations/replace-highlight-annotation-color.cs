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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate over all annotations on the current page
                for (int annotIndex = 1; annotIndex <= page.Annotations.Count; annotIndex++)
                {
                    Annotation annotation = page.Annotations[annotIndex];

                    // Process only Highlight annotations
                    if (annotation is HighlightAnnotation highlight)
                    {
                        // Example: retrieve the appearance dictionary (optional)
                        // var appearanceDict = highlight.Appearance;

                        // Replace the highlight color with a custom shade (light orange)
                        highlight.Color = Aspose.Pdf.Color.FromRgb(1.0, 0.8, 0.6);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}