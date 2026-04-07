using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string stampImg  = "logo.png";       // image to use as stamp
        const string outputPdf = "output.pdf";     // result PDF

        // Ensure input files exist
        if (!File.Exists(inputPdf) || !File.Exists(stampImg))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Align to top‑right corner
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;

            // Set margin offsets (example: 20 points from right and top edges)
            imgStamp.RightMargin = 20; // distance from the right edge
            imgStamp.TopMargin   = 20; // distance from the top edge

            // Optional: adjust opacity or scaling if needed
            // imgStamp.Opacity = 0.8;
            // imgStamp.Zoom = 0.5;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}