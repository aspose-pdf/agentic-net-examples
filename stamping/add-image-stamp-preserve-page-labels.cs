using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string stampImg = "logo.png";    // image to use as stamp
        const string outputPdf = "stamped_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the file
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp (example: bottom‑right corner)
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Optional visual tweaks
                Opacity = 0.5f
            };

            // ImageStamp does not have a Margin property – use XIndent/YIndent instead.
            // Here we emulate a 10‑point margin from the right and bottom edges.
            imgStamp.XIndent = 10f; // horizontal offset (points)
            imgStamp.YIndent = 10f; // vertical offset (points)

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Page labels are preserved automatically; no extra work required.

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdf}'.");
    }
}
