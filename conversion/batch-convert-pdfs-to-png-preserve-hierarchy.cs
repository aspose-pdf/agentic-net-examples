using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class BatchPdfToPngConverter
{
    static void Main()
    {
        // Input root folder containing PDFs (can be changed as needed)
        const string inputRoot = @"C:\InputPdfs";
        // Output root folder where PNGs will be placed, preserving hierarchy
        const string outputRoot = @"C:\OutputPngs";

        if (!Directory.Exists(inputRoot))
        {
            Console.Error.WriteLine($"Input directory does not exist: {inputRoot}");
            return;
        }

        // Find all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(inputRoot, "*.pdf", SearchOption.AllDirectories);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Create a PNG device with desired resolution (e.g., 300 DPI)
        Resolution resolution = new Resolution(300);
        PngDevice pngDevice = new PngDevice(resolution); // No using – PngDevice does not implement IDisposable

        foreach (string pdfPath in pdfFiles)
        {
            // Compute relative path to maintain folder hierarchy
            string relativePath = Path.GetRelativePath(inputRoot, pdfPath);
            string relativeDir = Path.GetDirectoryName(relativePath) ?? string.Empty;
            string pdfFileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);

            // Destination directory for this PDF's images
            string destDir = Path.Combine(outputRoot, relativeDir, pdfFileNameWithoutExt);
            Directory.CreateDirectory(destDir);

            // Open PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    Page page = pdfDocument.Pages[pageNumber];
                    string pngPath = Path.Combine(destDir, $"page_{pageNumber}.png");

                    // Save page as PNG
                    using (FileStream pngStream = new FileStream(pngPath, FileMode.Create, FileAccess.Write))
                    {
                        pngDevice.Process(page, pngStream);
                    }
                }
            }

            Console.WriteLine($"Converted: {pdfPath}");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
