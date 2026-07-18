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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Create a DefaultAppearance for the free‑text annotation.
            // The constructor expects a System.Drawing.Color for the text color.
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Define the rectangle where the annotation will be displayed.
            // Fully qualify Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the FreeTextAnnotation on the first page.
            FreeTextAnnotation freeText = new FreeTextAnnotation(doc.Pages[1], rect, appearance)
            {
                Contents = "Callout annotation with leader line",
                // Set the intent to indicate this annotation functions as a callout.
                Intent = FreeTextIntent.FreeTextCallout,
                // Optional visual styling
                Color = Aspose.Pdf.Color.Yellow,
                Opacity = 0.8
            };

            // Define the three points required for the Callout property:
            // 1. Start point (inside the annotation rectangle)
            // 2. Knee point (where the line bends)
            // 3. End point (the tip of the leader line)
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 540), // start inside annotation
                new Aspose.Pdf.Point(200, 580), // knee
                new Aspose.Pdf.Point(250, 620)  // end (leader line tip)
            };

            // Add the annotation to the page's annotation collection.
            doc.Pages[1].Annotations.Add(freeText);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with callout annotation to '{outputPath}'.");
    }
}