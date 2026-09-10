using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and image file paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "stamp.png";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Set fixed dimensions (in points) that do not depend on page size
            imgStamp.Width  = 150; // fixed width
            imgStamp.Height = 100; // fixed height

            // Optional: position the stamp at a fixed location on each page
            // Here we set the lower‑left corner 50 points from the left and 50 points from the bottom
            imgStamp.XIndent = 50;
            imgStamp.YIndent = 50;

            // Apply the stamp to every page in the document
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++) // 1‑based indexing
            {
                Page page = pdfDoc.Pages[pageNum];
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied with fixed size. Output saved to '{outputPdfPath}'.");
    }
}