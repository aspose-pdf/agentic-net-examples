using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 6 pages.
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The PDF does not contain a page 6.");
                return;
            }

            // Page 6 (Aspose.Pdf pages are 1‑based).
            Page page = doc.Pages[6];

            // Define the rectangle that will bound the circle.
            // (llx, lly) – lower‑left corner, (urx, ury) – upper‑right corner.
            double llx = 100, lly = 400, urx = 300, ury = 600;
            Rectangle rect = new Rectangle(llx, lly, urx, ury);

            // Create the circle annotation.
            CircleAnnotation circle = new CircleAnnotation(page, rect);
            // Border (outline) color – green.
            circle.Color = Color.Green;
            // Border thickness – 3 points.
            circle.Border = new Border(circle) { Width = 3 };
            // No fill color – leave it transparent (default).

            // Add the annotation to the page.
            page.Annotations.Add(circle);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added. Output saved to '{outputPath}'.");
    }
}
