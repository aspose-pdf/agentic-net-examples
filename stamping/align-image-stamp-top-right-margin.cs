using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for HorizontalAlignment and VerticalAlignment enums

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "logo.png";          // image to use as stamp
        const string outputPdf = "output.pdf";

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

        // Load the PDF document (lifecycle rule: using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Align to top‑right corner
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right; // right side
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;    // top side

            // Set margin offsets (distance from the page edges)
            imgStamp.RightMargin = 20; // 20 points from the right edge
            imgStamp.TopMargin   = 15; // 15 points from the top edge

            // Apply the stamp to every page (or choose a specific page)
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