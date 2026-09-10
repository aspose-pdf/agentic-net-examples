using System;
using System.Collections.Generic;
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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Collect all highlight annotations on the current page
                List<Annotation> highlights = new List<Annotation>();
                foreach (Annotation annotation in page.Annotations)
                {
                    if (annotation is HighlightAnnotation)
                        highlights.Add(annotation);
                }

                // Delete each collected highlight annotation
                foreach (Annotation highlight in highlights)
                {
                    page.Annotations.Delete(highlight);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations removed. Saved to '{outputPath}'.");
    }
}