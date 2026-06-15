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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Get page three (Aspose.Pdf uses 1‑based indexing)
            Page pageThree = doc.Pages[3];

            // Define the rectangle where the annotation will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the ScreenAnnotation with the animated GIF
            // Constructor: ScreenAnnotation(Page, Rectangle, string mediaFile)
            ScreenAnnotation screenAnn = new ScreenAnnotation(pageThree, rect, gifPath);

            // Add the annotation to the page
            pageThree.Annotations.Add(screenAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with animated GIF screen annotation: {outputPdf}");
    }
}