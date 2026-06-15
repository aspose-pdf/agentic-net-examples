using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF with header
        const string logoPath  = "logo.png";    // company logo image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp for the logo
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                // Place the stamp in the header area (top‑left)
                Background = false,                     // draw on top of page content
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                // Optional offsets from the page edges
                XIndent = 10,   // 10 points from the left edge
                YIndent = 10    // 10 points from the top edge
            };

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF (lifecycle rule: use Save without extra options)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header with logo added to each page. Saved as '{outputPdf}'.");
    }
}