using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string stampImagePath = "logo.png";   // image to use as stamp
        const string outputPdfPath = "output.pdf";  // result PDF

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Error: Image file not found – {stampImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Select the page to stamp (Aspose.Pdf uses 1‑based indexing)
            Page targetPage = pdfDoc.Pages[1];

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);

            // Set exact position:
            // XIndent – distance from the left edge of the page
            // YIndent – distance from the bottom edge of the page
            imgStamp.XIndent = 100; // 100 points from the left
            imgStamp.YIndent = 150; // 150 points from the bottom

            // Optional visual adjustments
            imgStamp.Width   = 200;   // stamp width in points
            imgStamp.Height  = 100;   // stamp height in points
            imgStamp.Opacity = 0.8;   // 80 % opacity (semi‑transparent)

            // Add the stamp to the selected page
            targetPage.AddStamp(imgStamp);

            // Save the modified document
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp placed at (X=100, Y=150) and saved to '{outputPdfPath}'.");
    }
}