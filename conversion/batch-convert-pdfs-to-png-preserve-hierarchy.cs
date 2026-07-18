using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Root folder containing PDF files (adjust as needed)
        const string sourceRoot = @"C:\InputPdfs";
        // Destination root where PNG images will be placed, mirroring the source hierarchy
        const string destRoot = @"C:\OutputPngs";

        if (!Directory.Exists(sourceRoot))
        {
            Console.Error.WriteLine($"Source directory not found: {sourceRoot}");
            return;
        }

        // Resolve all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(sourceRoot, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfPath in pdfFiles)
        {
            // Compute relative path to preserve folder hierarchy
            string relativePath = Path.GetRelativePath(sourceRoot, pdfPath);
            string relativeDir  = Path.GetDirectoryName(relativePath) ?? string.Empty;

            // Destination folder for this PDF's images
            string outputDir = Path.Combine(destRoot, relativeDir);
            Directory.CreateDirectory(outputDir);

            // Load PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure image resolution (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);
                PngDevice pngDevice = new PngDevice(resolution);

                // Iterate pages using 1‑based indexing
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    string pngPath = Path.Combine(outputDir, $"page{pageNum}.png");

                    // Save each page as a PNG file
                    using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                    {
                        pngDevice.Process(pdfDoc.Pages[pageNum], pngStream);
                    }
                }
            }

            Console.WriteLine($"Converted: {pdfPath} → {outputDir}");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}