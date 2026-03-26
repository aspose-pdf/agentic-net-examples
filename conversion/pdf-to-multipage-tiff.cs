using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Use the directory where the executable resides as the data directory
        string dataDir = AppDomain.CurrentDomain.BaseDirectory;

        // Name of the source PDF file (must exist in the data directory)
        string pdfFile = "sample.pdf"; // <-- replace with your actual PDF name

        // Ensure the data directory exists
        if (!Directory.Exists(dataDir))
        {
            Console.WriteLine($"Data directory not found: {dataDir}");
            return;
        }

        string pdfPath = Path.Combine(dataDir, pdfFile);
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Full path for the output multi‑page TIFF
        string outputTiff = Path.Combine(dataDir, "AllPages.tif");

        // Load the PDF document (1‑based page indexing is handled internally)
        using (Document pdfDocument = new Document(pdfPath))
        {
            // TiffDevice with default settings creates a single multi‑page TIFF
            TiffDevice tiffDevice = new TiffDevice();

            // Convert all pages of the PDF into the TIFF file
            tiffDevice.Process(pdfDocument, outputTiff);
        }

        Console.WriteLine($"TIFF saved to '{outputTiff}'.");
    }
}
