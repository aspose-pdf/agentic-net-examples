using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color used by DefaultAppearance
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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
            // Ensure there is at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("Document has no pages.");
                return;
            }

            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance (requires System.Drawing.Color)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Create the FreeTextAnnotation
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Visible border and background color (optional)
                Color = Aspose.Pdf.Color.Yellow,
                Contents = "Callout example",
                // Set the intent to indicate a callout annotation
                Intent = FreeTextIntent.FreeTextCallout
            };

            // Define the three points for the callout leader line:
            // 1. Start point (inside the annotation rectangle)
            // 2. Knee point (where the line bends)
            // 3. End point (target point the callout points to)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(200, 525), // start (center of the annotation)
                new Aspose.Pdf.Point(250, 600), // knee
                new Aspose.Pdf.Point(350, 650)  // end (pointing to target)
            };

            // Add the annotation to the page
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with callout annotation to '{outputPath}'.");
    }
}