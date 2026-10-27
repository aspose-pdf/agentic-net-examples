using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputDir = "ExportedImages";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML source not found: {xmlPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document from its XML representation
        using (Document pdfDoc = new Document(xmlPath))
        {
            // Desired image resolution (e.g., 300 DPI)
            Resolution imageResolution = new Resolution(300);

            // Export each page as a PNG image using the specified resolution
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                string imagePath = Path.Combine(outputDir, $"Page_{pageNumber}.png");

                using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                {
                    // PngDevice respects the Resolution property
                    PngDevice pngDevice = new PngDevice(imageResolution);
                    pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Exported page {pageNumber} to {imagePath}");
            }
        }
    }
}