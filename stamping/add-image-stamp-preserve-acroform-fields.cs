using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF with AcroForm fields
        const string stampImg  = "stamp.png";   // image to be used as stamp
        const string outputPdf = "output.pdf";  // result PDF (fields preserved)

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

        // Load the PDF document (AcroForm fields are loaded automatically)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp instance – this does NOT affect form fields
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Example visual settings (optional)
                Background          = false,                     // stamp on top of page content
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Opacity             = 0.5f                       // 50% transparent
            };

            // Apply the stamp to every page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF – AcroForm fields remain intact
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}