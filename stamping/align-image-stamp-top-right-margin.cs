using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For ImageStamp (inherits from Stamp, already in Aspose.Pdf)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "logo.png";   // Image to use as stamp
        const string outputPdf = "output.pdf";

        // Margin offsets (in points)
        const double rightOffset = 20;   // distance from right edge
        const double topOffset   = 20;   // distance from top edge

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
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Align to top‑right corner
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;   // right side
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;      // top side

            // Apply margin offsets
            imgStamp.RightMargin = rightOffset;
            imgStamp.TopMargin   = topOffset;

            // Optionally set opacity (0.0‑1.0) or other visual properties here
            // imgStamp.Opacity = 0.8;

            // Add the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied. Output saved to '{outputPdf}'.");
    }
}