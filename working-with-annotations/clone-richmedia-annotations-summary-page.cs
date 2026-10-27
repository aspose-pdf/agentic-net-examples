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

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new blank page that will serve as the summary page
            Page summaryPage = doc.Pages.Add();

            // Position where the first cloned annotation will be placed
            // (adjust as needed; here we start near the top-left corner)
            double startX = 50;
            double startY = summaryPage.PageInfo.Height - 50;
            double offsetY = 150; // vertical spacing between cloned annotations

            // Iterate over all existing pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page srcPage = doc.Pages[i];

                // Examine each annotation on the current page
                foreach (Annotation annot in srcPage.Annotations)
                {
                    // Process only RichMediaAnnotations
                    if (annot is RichMediaAnnotation richMedia)
                    {
                        // Clone the annotation.
                        // RichMediaAnnotation implements ICloneable, so we can use Clone().
                        RichMediaAnnotation clone = (RichMediaAnnotation)richMedia.Clone();

                        // Place the clone on the summary page.
                        // Adjust the rectangle so that annotations are stacked vertically.
                        // Original rectangle is preserved relative to its size.
                        double width  = clone.Rect.URX - clone.Rect.LLX;
                        double height = clone.Rect.URY - clone.Rect.LLY;

                        // Define a new rectangle for the clone on the summary page
                        clone.Rect = new Rectangle(
                            startX,
                            startY - height,
                            startX + width,
                            startY
                        );

                        // Add the cloned annotation to the summary page
                        summaryPage.Annotations.Add(clone);

                        // Update the Y position for the next annotation
                        startY -= (height + offsetY);
                    }
                }
            }

            // Save the modified document (PDF format) inside the using block
            doc.Save(outputPath);
        }

        Console.WriteLine($"Summary PDF saved to '{outputPath}'.");
    }
}