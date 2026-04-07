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
            // Create a new summary page at the end of the document
            Page summaryPage = doc.Pages.Add();

            // Starting position for placing cloned annotations on the summary page
            double startX = 50;
            double startY = 750;
            double offsetY = 120; // vertical spacing between clones

            // Iterate over all existing pages (excluding the newly added summary page)
            for (int i = 1; i <= doc.Pages.Count - 1; i++)
            {
                Page srcPage = doc.Pages[i];
                foreach (Annotation ann in srcPage.Annotations)
                {
                    // Process only RichMediaAnnotation instances
                    if (ann is RichMediaAnnotation richMedia)
                    {
                        // Clone by creating a new instance with the same rectangle size
                        // Adjust position for the summary page to avoid overlap
                        Aspose.Pdf.Rectangle srcRect = richMedia.Rect;
                        double width = srcRect.URX - srcRect.LLX;
                        double height = srcRect.URY - srcRect.LLY;

                        Aspose.Pdf.Rectangle cloneRect = new Aspose.Pdf.Rectangle(
                            startX,
                            startY - offsetY * (i - 1),
                            startX + width,
                            startY - offsetY * (i - 1) + height);

                        RichMediaAnnotation clone = new RichMediaAnnotation(summaryPage, cloneRect)
                        {
                            // Copy basic visual properties
                            Color = richMedia.Color,
                            Contents = richMedia.Contents,
                            Type = richMedia.Type,
                            ActivateOn = richMedia.ActivateOn,
                            Flags = richMedia.Flags,
                            Name = richMedia.Name
                        };

                        // ----- Copy the media content (if any) -----
                        // RichMediaAnnotation does not expose the original content stream directly.
                        // The safest approach is to try to obtain it via reflection (some SDK versions expose a GetContent method).
                        try
                        {
                            Stream srcContent = null;
                            var getContentMethod = richMedia.GetType().GetMethod("GetContent");
                            if (getContentMethod != null)
                            {
                                srcContent = getContentMethod.Invoke(richMedia, null) as Stream;
                            }

                            if (srcContent != null)
                            {
                                using (var ms = new MemoryStream())
                                {
                                    srcContent.CopyTo(ms);
                                    ms.Position = 0;
                                    // Use a generic MIME type if the original type is unknown.
                                    clone.SetContent("application/octet-stream", ms);
                                }
                            }
                        }
                        catch
                        {
                            // If any step fails, continue without copying the media content.
                        }
                        // ------------------------------------------------

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
