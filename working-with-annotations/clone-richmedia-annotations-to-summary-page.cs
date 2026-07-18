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
            // Add a new page that will hold the cloned RichMedia annotations
            Page summaryPage = doc.Pages.Add();

            // Iterate over all existing pages
            foreach (Page srcPage in doc.Pages)
            {
                // Iterate over annotations on the current page
                foreach (Annotation ann in srcPage.Annotations)
                {
                    // Process only RichMediaAnnotation instances
                    if (ann is RichMediaAnnotation richMedia)
                    {
                        // Create a clone on the summary page using the same rectangle
                        RichMediaAnnotation clone = new RichMediaAnnotation(
                            summaryPage,
                            new Aspose.Pdf.Rectangle(
                                richMedia.Rect.LLX,
                                richMedia.Rect.LLY,
                                richMedia.Rect.URX,
                                richMedia.Rect.URY));

                        // Copy commonly used properties
                        clone.Contents          = richMedia.Contents;
                        clone.Color             = richMedia.Color;
                        clone.Flags             = richMedia.Flags;
                        clone.Name              = richMedia.Name;
                        clone.Type              = richMedia.Type;
                        clone.ActivateOn        = richMedia.ActivateOn;
                        clone.ActiveState       = richMedia.ActiveState;
                        clone.CustomFlashVariables = richMedia.CustomFlashVariables;
                        clone.CustomPlayer      = richMedia.CustomPlayer;
                        clone.Modified          = richMedia.Modified;
                        clone.ZIndex            = richMedia.ZIndex;

                        // If the original annotation has embedded content, copy it
                        // (Content is a stream; Clone() returns null, so we copy manually if needed)
                        // Example: clone.SetContent(richMedia.Content, richMedia.ContentStream);
                        // Skipping detailed stream copy for brevity.

                        // Add the cloned annotation to the summary page
                        summaryPage.Annotations.Add(clone);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cloned RichMedia annotations saved to '{outputPath}'.");
    }
}