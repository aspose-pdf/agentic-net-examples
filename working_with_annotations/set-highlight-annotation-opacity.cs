using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_highlights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through all annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Identify HighlightAnnotation instances and set opacity to 60%
                    if (ann is HighlightAnnotation highlight)
                    {
                        highlight.Opacity = 0.6; // Opacity range is 0.0 (transparent) to 1.0 (opaque)
                    }
                }
            }

            // Save the modified document (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations updated and saved to '{outputPath}'.");
    }
}