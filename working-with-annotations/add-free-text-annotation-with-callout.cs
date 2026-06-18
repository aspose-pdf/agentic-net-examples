using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;          // needed for DefaultAppearance
using System.Drawing;          // DefaultAppearance constructor requires System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the free‑text annotation
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a DefaultAppearance for the annotation text (font, size, color)
            // Note: DefaultAppearance constructor requires System.Drawing.Color (per rule)
            DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);

            // Instantiate the FreeTextAnnotation with page, rectangle, and appearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                // Text displayed inside the annotation
                Contents = "This is a free‑text annotation with a callout.",
                // Border/annotation color (use Aspose.Pdf.Color to avoid System.Drawing ambiguity)
                Color = Aspose.Pdf.Color.Yellow
            };

            // Set the callout line – exactly three points are required
            freeText.Callout = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(150, 540), // start point (inside the annotation)
                new Aspose.Pdf.Point(200, 600), // knee point (bend)
                new Aspose.Pdf.Point(250, 650)  // end point (arrow tip)
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation with callout saved to '{outputPath}'.");
    }
}