using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string stampImagePath = "stamp.png";  // image to use as stamp
        const string outputPdfPath = "output.pdf";  // result PDF

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

        // Load the PDF document – use a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Optional: configure stamp appearance
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center; // center horizontally
            imgStamp.VerticalAlignment   = VerticalAlignment.Bottom; // place near bottom
            imgStamp.Opacity = 0.5; // 50 % transparent

            // Apply the stamp to every page (Pages collection is 1‑based)
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified document (saving without options writes PDF)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
    }
}