using System;
using System.IO;
using System.Drawing;                     // Required for DefaultAppearance constructor
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;                    // For DefaultAppearance

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_callout.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create DefaultAppearance (font, size, color). The constructor requires System.Drawing.Color.
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Callout example",
                Color    = Aspose.Pdf.Color.Yellow,               // Border color of the annotation
                Intent   = FreeTextIntent.FreeTextCallout        // Specify that this is a callout
            };

            // Define the three points for the callout leader line:
            //   1. Start point (inside the annotation)
            //   2. Knee point (bend)
            //   3. End point (target point)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 525),   // Inside the annotation rectangle
                new Aspose.Pdf.Point(200, 600),   // Knee point
                new Aspose.Pdf.Point(250, 650)    // Target point
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with callout annotation to '{outputPath}'.");
    }
}