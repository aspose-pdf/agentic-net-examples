using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf.Facades;      // For ImageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "watermarked.pdf"; // result PDF
        const string imagePath = "logo.png";        // watermark image

        // Validate file existence
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create an image stamp from the watermark file
                ImageStamp stamp = new ImageStamp(imagePath);

                // Set arbitrary rotation angle (45 degrees) for diagonal placement
                stamp.RotateAngle = 45;

                // Optional: make the stamp semi‑transparent and place it over the content
                stamp.Opacity   = 0.5;   // 0 = fully transparent, 1 = opaque
                stamp.Background = false; // false = overlay, true = behind page content

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified document as PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}