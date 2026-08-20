using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, ImageStamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string stampImg  = "logo.png";       // image to use as stamp
        const string outputPdf = "output.pdf";     // result PDF

        // Verify files exist
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page to stamp (1‑based indexing)
            Page page = doc.Pages[1];

            // Create an ImageStamp from a file path
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Set custom coordinates (XIndent = distance from left, YIndent = distance from bottom)
            imgStamp.XIndent = 150;   // 150 points from the left edge
            imgStamp.YIndent = 300;   // 300 points from the bottom edge

            // Optional: set size or zoom if needed
            // imgStamp.Width  = 100;   // explicit width in points
            // imgStamp.Height = 50;    // explicit height in points
            // imgStamp.Opacity = 0.8;  // semi‑transparent

            // Add the stamp to the selected page
            page.AddStamp(imgStamp);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}