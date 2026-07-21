using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_gif.pdf"; // result PDF
        const string gifPath   = "animation.gif";      // animated GIF to embed

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine($"GIF file not found: {gifPath}");
            return;
        }

        // Load the PDF (using the recommended load pattern)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document must contain at least three pages.");
                return;
            }

            // Page 3 (1‑based indexing)
            Page page = doc.Pages[3];

            // Define the rectangle where the annotation will appear
            // (left, bottom, right, top) – adjust as needed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ScreenAnnotation with the GIF file
            ScreenAnnotation screenAnn = new ScreenAnnotation(page, rect, gifPath);

            // The ScreenAnnotation automatically plays the media; most viewers loop GIFs by default.
            // If additional looping control is required, it can be set via the underlying RichMediaAction,
            // but the basic constructor is sufficient for a continuously looping animated GIF.

            // Add the annotation to the page
            page.Annotations.Add(screenAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with animated GIF saved to '{outputPdf}'.");
    }
}