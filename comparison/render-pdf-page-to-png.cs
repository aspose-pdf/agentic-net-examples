using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";          // kept for parity, not used in this example
        const string destinationPdfPath = "destination.pdf";
        const string outputImagePath = "destination_page.png";
        const int pageNumber = 1; // 1‑based page index

        // Verify input files exist
        if (!File.Exists(sourcePdfPath) || !File.Exists(destinationPdfPath))
        {
            Console.Error.WriteLine("Source or destination PDF not found.");
            return;
        }

        // Load the destination PDF
        Document destDoc = new Document(destinationPdfPath);

        // Ensure the requested page exists
        if (pageNumber < 1 || pageNumber > destDoc.Pages.Count)
        {
            Console.Error.WriteLine($"Page {pageNumber} does not exist in the destination PDF.");
            return;
        }

        // Render the specified page to a PNG image using the core Aspose.Pdf.Devices API
        using (var imageStream = new MemoryStream())
        {
            // You can adjust the resolution (DPI) as needed; 300 DPI gives good quality
            var pngDevice = new PngDevice(new Resolution(300));
            pngDevice.Process(destDoc.Pages[pageNumber], imageStream);

            // Write the PNG bytes to the output file
            File.WriteAllBytes(outputImagePath, imageStream.ToArray());
        }

        Console.WriteLine($"Destination page {pageNumber} image saved to '{outputImagePath}'.");
    }
}
