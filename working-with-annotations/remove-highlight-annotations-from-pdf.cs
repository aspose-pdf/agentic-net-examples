using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Gather all highlight annotations on the current page
                var highlights = new System.Collections.Generic.List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is HighlightAnnotation)
                        highlights.Add(ann);
                }

                // Delete each collected highlight annotation
                foreach (Annotation ann in highlights)
                {
                    page.Annotations.Delete(ann);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All highlight annotations removed. Saved to '{outputPath}'.");
    }
}