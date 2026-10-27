using System;
using System.IO;
using System.Drawing;
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

        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury) in points
            var rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // DefaultAppearance is required for FreeTextAnnotation
            var appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Black);

            // Create the free‑text annotation
            var freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Sample free‑text annotation",
                // Border/fill colour – using the inherited Color property
                Color = Aspose.Pdf.Color.Yellow,
                // Mark this annotation as a callout
                Intent = FreeTextIntent.FreeTextCallout,
                // Optional: show an arrow at the end of the leader line
                EndingStyle = LineEnding.OpenArrow
            };

            // Set a leader line (callout) of exactly 20 points.
            // The first point is placed at the centre of the annotation's bottom edge;
            // the second point is 20 points to the left, creating a 20‑point line.
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(rect.LLX + (rect.URX - rect.LLX) / 2, rect.LLY),
                new Aspose.Pdf.Point(rect.LLX + (rect.URX - rect.LLX) / 2 - 20, rect.LLY)
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
