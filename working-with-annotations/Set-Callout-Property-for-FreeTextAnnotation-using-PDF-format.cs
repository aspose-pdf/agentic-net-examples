using System;
using System.IO;
using System.Drawing;                     // needed for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;                    // for DefaultAppearance

// Alias to disambiguate the Color type used for DefaultAppearance
using DrawingColor = System.Drawing.Color;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document has no pages.");
                return;
            }

            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the free‑text annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create DefaultAppearance via the constructor (Font is read‑only, FontColor property does not exist)
            // The third parameter expects System.Drawing.Color, not Aspose.Pdf.Color
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, DrawingColor.Black);

            // Create the FreeTextAnnotation on the selected page
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Set the visible text of the annotation
                Contents = "Callout example",
                // Set the intent to indicate this is a callout annotation
                Intent = FreeTextIntent.FreeTextCallout,
                // Optional: set a background color for better visibility
                Color = Aspose.Pdf.Color.LightYellow
            };

            // The Callout property requires exactly three points:
            //   1. Start point (inside the annotation rectangle)
            //   2. Knee point (where the line bends)
            //   3. End point (the point the callout line points to)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 550), // start inside the annotation
                new Aspose.Pdf.Point(200, 620), // knee
                new Aspose.Pdf.Point(250, 650)  // end point
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation with callout saved to '{outputPath}'.");
    }
}
