using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "logo.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(stampImg))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from a file (ImageStamp constructor)
            ImageStamp imgStamp = new ImageStamp(stampImg);

            // Align to the top‑right corner
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;   // right side
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;      // top side

            // Set margin offsets (distance from the page edges)
            imgStamp.RightMargin = 20; // 20 points from the right edge
            imgStamp.TopMargin   = 20; // 20 points from the top edge

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle rule: use using, then Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}