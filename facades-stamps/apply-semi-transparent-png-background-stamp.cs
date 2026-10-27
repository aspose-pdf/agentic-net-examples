using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string pngPath   = "background.png";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pngPath))
        {
            Console.Error.WriteLine($"Background PNG not found: {pngPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Apply the semi‑transparent background image to every page
        foreach (Page page in pdfDocument.Pages)
        {
            ImageStamp stamp = new ImageStamp(pngPath)
            {
                // Place the image behind existing page content
                Background = true,
                // Semi‑transparent (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.5,
                // Stretch the image to cover the whole page
                Width  = page.PageInfo.Width,
                Height = page.PageInfo.Height,
                // Align to the lower‑left corner (origin) of the page
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Bottom,
                XIndent = 0,
                YIndent = 0
            };

            page.AddStamp(stamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Background stamp applied and saved to '{outputPdf}'.");
    }
}
