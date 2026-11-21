using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output_centered.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Verify that page 8 exists (Aspose.Pdf uses 1‑based indexing)
        if (doc.Pages.Count < 8)
        {
            Console.Error.WriteLine("PDF contains fewer than 8 pages.");
            return;
        }

        // Get page 8 (no need to read dimensions when using alignment)
        Page page = doc.Pages[8];

        // Define stamp size (in points). Adjust as needed.
        const double stampWidth  = 200; // points
        const double stampHeight = 100; // points

        // Create an ImageStamp and position it at the centre of the page
        ImageStamp stamp = new ImageStamp(stampImg)
        {
            Width  = stampWidth,
            Height = stampHeight,
            // Center horizontally and vertically
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center,
            // Ensure the stamp is drawn in the foreground (default)
            Background = false
        };

        // Add the stamp to page 8
        page.AddStamp(stamp);

        // Save the result
        doc.Save(outputPdf);

        Console.WriteLine($"Centered stamp saved to '{outputPdf}'.");
    }
}
