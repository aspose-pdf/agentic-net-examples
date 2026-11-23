using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the PDF and ensure resources are disposed properly
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page page = doc.Pages[3];

            // Define the rectangle where the free‑text annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Build the default appearance (font, size, text colour)
            var appearance = new DefaultAppearance("Arial", 12, System.Drawing.Color.Blue);

            // Create the FreeTextAnnotation with the required DefaultAppearance
            FreeTextAnnotation freeText = new FreeTextAnnotation(page, rect, appearance)
            {
                Contents = "Important note",
                // Background colour of the annotation (optional)
                Color = Aspose.Pdf.Color.Yellow,
                // Set the annotation as a callout
                Intent = FreeTextIntent.FreeTextCallout,
                Callout = new[]
                {
                    new Aspose.Pdf.Point(80, 520),   // target point (where the leader line points to)
                    new Aspose.Pdf.Point(100, 520),  // knee point (where the line bends)
                    new Aspose.Pdf.Point(120, 540)   // point on the annotation rectangle edge
                },
                EndingStyle = LineEnding.OpenArrow
            };

            // Set a simple border for the annotation
            freeText.Border = new Border(freeText) { Width = 1 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(freeText);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"FreeTextAnnotation with callout saved to '{outputPath}'.");
    }
}
