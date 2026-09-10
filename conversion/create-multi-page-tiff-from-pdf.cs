using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Resolve a concrete data directory relative to the executable location.
        // You can change this to any folder that contains the source PDF.
        string dataDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
        if (!Directory.Exists(dataDir))
        {
            Console.WriteLine($"Data directory does not exist: {dataDir}");
            return;
        }

        // Name of the source PDF file placed inside the data directory.
        const string pdfFileName = "sample.pdf"; // <-- replace with your actual PDF file name
        string pdfPath = Path.Combine(dataDir, pdfFileName);
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Output TIFF file (multi‑page TIFF) will be created in the same data folder.
        string outputTiff = Path.Combine(dataDir, "AllPages.tif");

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create a TiffDevice with default settings (default compression).
            TiffDevice tiffDevice = new TiffDevice();

            // Convert all pages of the PDF into a single multi‑page TIFF file.
            tiffDevice.Process(pdfDocument, outputTiff);
        }

        Console.WriteLine($"Multi‑page TIFF created at: {outputTiff}");
    }
}
