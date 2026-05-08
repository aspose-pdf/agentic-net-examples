using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImagePath = "stamp.png";

        // Verify that the source PDF and stamp image exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document (wrapped in a using block for deterministic disposal)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an image stamp from the file and set low quality (10%)
            ImageStamp imgStamp = new ImageStamp(stampImagePath);
            imgStamp.Quality = 10;                     // low‑quality to improve performance
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;
            imgStamp.Opacity = 0.5;                    // optional semi‑transparent appearance

            // Apply the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with low‑quality image stamp to '{outputPath}'.");
    }
}