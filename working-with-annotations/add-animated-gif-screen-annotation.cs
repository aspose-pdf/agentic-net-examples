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
                Console.Error.WriteLine("The document must contain at least three pages.");
                return;
            }

            // Define the rectangle where the annotation will appear on page 3
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ScreenAnnotation on page 3 with the animated GIF
            // The GIF will loop automatically when displayed by PDF viewers that support it
            ScreenAnnotation screenAnn = new ScreenAnnotation(doc.Pages[3], rect, gifPath);

            // Optionally set a title or contents (tooltip) for the annotation
            screenAnn.Title    = "Animated GIF";
            screenAnn.Contents = "This annotation displays an animated GIF that loops continuously.";

            // Add the annotation to the page's annotation collection
            doc.Pages[3].Annotations.Add(screenAnn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with ScreenAnnotation: {outputPdf}");
    }
}