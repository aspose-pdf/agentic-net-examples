using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and image file paths
        const string inputPdfPath  = "input.pdf";
        const string stampImagePath = "logo.png";
        const string outputPdfPath = "output.pdf";

        // Fixed stamp dimensions (in points). These values are independent of page size.
        const double stampWidth  = 150; // width of the stamp
        const double stampHeight = 100; // height of the stamp

        // Verify that the source files exist
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

            // Set fixed dimensions – these override any automatic scaling based on page size
            imgStamp.Width  = stampWidth;
            imgStamp.Height = stampHeight;

            // Optional: position the stamp at a fixed location on each page.
            // Here we use absolute coordinates (XIndent, YIndent) measured from the lower‑left corner.
            // Adjust these values as needed; they are the same for every page.
            imgStamp.XIndent = 50; // distance from the left edge
            imgStamp.YIndent = 50; // distance from the bottom edge

            // Optional: set alignment if you prefer relative placement instead of absolute coordinates.
            // imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            // imgStamp.VerticalAlignment   = VerticalAlignment.Middle;

            // Apply the same stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}