using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string bgImage = "background.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(bgImage))
        {
            Console.Error.WriteLine($"Background image not found: {bgImage}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Iterate through each page and add a background image stamp
        foreach (Page page in pdfDocument.Pages)
        {
            // Create an ImageStamp from the PNG file
            ImageStamp stamp = new ImageStamp(bgImage)
            {
                // Place the stamp behind existing page content
                Background = true,
                // 50 % transparency
                Opacity = 0.5,
                // Size the stamp to the page dimensions
                Width = page.Rect.Width,
                Height = page.Rect.Height,
                // Position at the lower‑left corner of the page
                XIndent = 0,
                YIndent = 0
            };

            // Add the stamp to the current page
            page.AddStamp(stamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Background stamp applied and saved to '{outputPdf}'.");
    }
}
