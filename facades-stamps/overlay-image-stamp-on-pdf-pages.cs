using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an ImageStamp (Aspose.Pdf.Facades) from the image file
            ImageStamp imgStamp = new ImageStamp(stampPath);

            // Set Background to false so the stamp overlays existing page content
            imgStamp.Background = false;

            // Optional: set alignment or position as needed
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Apply the stamp to each page individually (Page.AddStamp)
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPath}'.");
    }
}
