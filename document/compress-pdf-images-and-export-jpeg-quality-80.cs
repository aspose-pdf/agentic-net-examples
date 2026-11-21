using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // Required for JpegDevice, Resolution, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PagesAsImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define a resolution for the output images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // Iterate using 1‑based page indexing (Aspose.Pdf uses 1‑based indexing for Pages collection)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Configure JPEG device with desired quality (80) via constructor
                JpegDevice jpegDevice = new JpegDevice(resolution, 80);

                // Build output file name for the current page
                string outputImagePath = Path.Combine(outputFolder, $"Page_{pageNumber}.jpg");

                // Save the specific page as a JPEG image using a FileStream
                using (FileStream outStream = new FileStream(outputImagePath, FileMode.Create, FileAccess.Write))
                {
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG with quality 80 → {outputImagePath}");
            }
        }

        Console.WriteLine("All pages have been converted to JPEG images.");
    }
}
