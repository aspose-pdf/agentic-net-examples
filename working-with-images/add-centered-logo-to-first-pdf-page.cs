using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string logoImagePath = "logo.png";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF and logo image exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; get the first page
            Page firstPage = pdfDoc.Pages[1];

            // Create an ImageStamp from the logo file
            ImageStamp logoStamp = new ImageStamp(logoImagePath)
            {
                // Center the stamp horizontally and vertically on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Optional: place the stamp on top of existing content
                // Background = false
            };

            // Add the stamp only to the first page
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Company logo added to first page. Saved as '{outputPdfPath}'.");
    }
}