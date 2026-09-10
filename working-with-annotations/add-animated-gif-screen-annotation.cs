using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string gifFile   = "animation.gif";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(gifFile))
        {
            Console.Error.WriteLine($"Animated GIF not found: {gifFile}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap Document in using)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages (page indexing is 1‑based)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document contains fewer than 3 pages.");
                return;
            }

            // Get page three
            Page page = doc.Pages[3];

            // Define the rectangle where the annotation will be placed
            // Rectangle(left, bottom, right, top) – values are in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ScreenAnnotation that references the animated GIF
            // Constructor: ScreenAnnotation(Page, Rectangle, string mediaFile)
            ScreenAnnotation screen = new ScreenAnnotation(page, rect, gifFile);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(screen);

            // Save the modified PDF (saving without explicit SaveOptions writes PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Screen annotation with animated GIF added to page 3 and saved as '{outputPdf}'.");
    }
}