using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "output_images";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Create the subdirectory where BMP images will be stored
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (Document implements IDisposable, so we can use a using block)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define the resolution for the output images (e.g., 150 DPI)
            Resolution resolution = new Resolution(150);

            // BmpDevice is a concrete device but does NOT implement IDisposable – do NOT wrap it in a using statement
            BmpDevice bmpDevice = new BmpDevice(resolution);

            // Convert each page to a BMP image
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.bmp");
                bmpDevice.Process(pdfDocument.Pages[pageNumber], outputPath);
            }
        }

        Console.WriteLine($"Conversion complete. BMP images saved to: {outputDir}");
    }
}
