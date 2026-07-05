using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_summary.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Add a new summary page at the end of the document
            Page summaryPage = doc.Pages.Add();

            // Optionally copy the size of the first page to the summary page
            if (doc.Pages.Count > 1)
                summaryPage.PageInfo = doc.Pages[1].PageInfo;

            // Iterate over all pages except the newly added summary page
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count - 1; pageIndex++)
            {
                Page srcPage = doc.Pages[pageIndex];

                // Examine each annotation on the current page
                foreach (Annotation ann in srcPage.Annotations)
                {
                    // Process only RichMediaAnnotation instances
                    if (ann is RichMediaAnnotation richMedia)
                    {
                        // Preserve the original rectangle (position and size)
                        Aspose.Pdf.Rectangle rect = richMedia.Rect;

                        // Create a new RichMediaAnnotation on the summary page
                        RichMediaAnnotation clone = new RichMediaAnnotation(summaryPage, rect);

                        // Copy commonly used properties
                        clone.Contents          = richMedia.Contents;
                        clone.Type              = richMedia.Type;
                        clone.ActivateOn        = richMedia.ActivateOn;
                        clone.Color             = richMedia.Color;
                        clone.Flags             = richMedia.Flags;
                        clone.Name              = richMedia.Name;
                        clone.Width             = richMedia.Width;
                        clone.Height            = richMedia.Height;
                        clone.ZIndex            = richMedia.ZIndex;
                        clone.TextHorizontalAlignment = richMedia.TextHorizontalAlignment;
                        clone.VerticalAlignment = richMedia.VerticalAlignment;

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