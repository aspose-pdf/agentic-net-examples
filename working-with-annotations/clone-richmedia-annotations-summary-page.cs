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

            // Optional: set the size of the summary page to match the first page
            if (doc.Pages.Count > 1)
            {
                Page firstPage = doc.Pages[1];
                summaryPage.PageInfo.Width  = firstPage.PageInfo.Width;
                summaryPage.PageInfo.Height = firstPage.PageInfo.Height;
            }

            // Iterate over all existing pages and their annotations
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page srcPage = doc.Pages[i];
                foreach (Annotation ann in srcPage.Annotations)
                {
                    // Identify RichMediaAnnotation instances
                    if (ann is RichMediaAnnotation richMedia)
                    {
                        // Clone by creating a new instance with the same rectangle
                        // Use fully qualified Rectangle to avoid ambiguity
                        Aspose.Pdf.Rectangle rect = richMedia.Rect;

                        RichMediaAnnotation clone = new RichMediaAnnotation(summaryPage, rect);

                        // Copy commonly used properties (add more as needed)
                        clone.Contents          = richMedia.Contents;
                        clone.Type              = richMedia.Type;
                        clone.ActivateOn        = richMedia.ActivateOn;
                        clone.Color             = richMedia.Color;
                        clone.Flags             = richMedia.Flags;
                        clone.Name              = richMedia.Name;
                        clone.Width             = richMedia.Width;
                        clone.Height            = richMedia.Height;
                        clone.ZIndex            = richMedia.ZIndex;
                        clone.CustomPlayer      = richMedia.CustomPlayer;
                        clone.CustomFlashVariables = richMedia.CustomFlashVariables;

                        // Add the cloned annotation to the summary page
                        summaryPage.Annotations.Add(clone);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with cloned RichMediaAnnotations on summary page: {outputPath}");
    }
}