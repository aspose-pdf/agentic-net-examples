using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Annotations;   // For annotation types if needed (not used here)

class Program
{
    static void Main()
    {
        // Input PDF and image files
        const string inputPdfPath  = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath = "output_fixed_stamp.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Set fixed dimensions (in points). These values are independent of page size.
            imgStamp.Width  = 120;   // Fixed width
            imgStamp.Height = 60;    // Fixed height

            // Optional: position the stamp using margins or alignment.
            // Here we place it at the bottom‑right corner of each page.
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            imgStamp.RightMargin  = 20; // distance from the right edge
            imgStamp.BottomMargin = 20; // distance from the bottom edge

            // Apply the same stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF. Using Save(string) writes PDF regardless of extension.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with fixed‑size image stamp: {outputPdfPath}");
    }
}