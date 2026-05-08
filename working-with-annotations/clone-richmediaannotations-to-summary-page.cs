using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Add a new page that will hold the cloned annotations
            Page summaryPage = doc.Pages.Add();

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Examine each annotation on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Process only RichMediaAnnotation instances
                    if (ann is RichMediaAnnotation richMedia)
                    {
                        // Preserve the original rectangle
                        Aspose.Pdf.Rectangle rect = richMedia.Rect;

                        // Create a new RichMediaAnnotation on the summary page
                        RichMediaAnnotation clone = new RichMediaAnnotation(summaryPage, rect);

                        // Copy basic visual and behavioral properties
                        clone.Contents = richMedia.Contents;
                        clone.Type = richMedia.Type;
                        clone.Color = richMedia.Color;
                        clone.Name = richMedia.Name;
                        clone.Flags = richMedia.Flags;
                        clone.ActivateOn = richMedia.ActivateOn;

                        // Add the cloned annotation to the summary page
                        summaryPage.Annotations.Add(clone);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cloned RichMediaAnnotations saved to '{outputPath}'.");
    }
}