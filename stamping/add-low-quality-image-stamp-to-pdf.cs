using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";   // source PDF
        const string stampImagePath = "stamp.png";   // image to use as stamp
        const string outputPdfPath  = "output.pdf";  // result PDF

        // Verify that required files exist
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Set low quality (10 %) to improve performance on large PDFs
            imgStamp.Quality = 10;

            // Optional: position the stamp (centered on each page)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to every page (pages are 1‑based)
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified document (lifecycle rule: use Save with path)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}