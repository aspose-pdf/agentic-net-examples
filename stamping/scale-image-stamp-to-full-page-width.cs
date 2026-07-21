using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageStamp resides in Aspose.Pdf namespace, but Facades not needed; kept for completeness

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string imagePath = "logo.png";      // image to stamp
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp from the image file
                ImageStamp imgStamp = new ImageStamp(imagePath);

                // Set the stamp width to the full page width (minus optional margins)
                // Height is not set – Aspose.Pdf preserves the original aspect ratio
                imgStamp.Width = page.PageInfo.Width;

                // Align the stamp to the top‑center of the page
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment   = VerticalAlignment.Top;

                // Optional: remove any margins so the image spans the entire width
                imgStamp.LeftMargin   = 0;
                imgStamp.RightMargin  = 0;
                imgStamp.TopMargin    = 0;
                imgStamp.BottomMargin = 0;

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}