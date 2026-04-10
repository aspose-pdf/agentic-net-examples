using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class BatchPdfToPngConverter
{
    static void Main()
    {
        // Root folder containing PDFs to convert
        const string sourceRoot = @"C:\InputPdfs";
        // Root folder where PNG images will be saved, preserving hierarchy
        const string outputRoot = @"C:\OutputPngs";

        if (!Directory.Exists(sourceRoot))
        {
            Console.Error.WriteLine($"Source directory not found: {sourceRoot}");
            return;
        }

        // Find all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(sourceRoot, "*.pdf", SearchOption.AllDirectories);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            // Compute relative path to preserve folder structure
            string relativePath = Path.GetRelativePath(sourceRoot, pdfPath);
            string relativeDir  = Path.GetDirectoryName(relativePath) ?? string.Empty;

            // Destination directory for this PDF's images
            string destDir = Path.Combine(outputRoot, relativeDir);
            Directory.CreateDirectory(destDir);

            // Open PDF document (deterministic disposal)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Set desired image resolution (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);
                // PNG device for rendering pages
                PngDevice pngDevice = new PngDevice(resolution);

                // Convert each page to a separate PNG file
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    string pngFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageNum}.png";
                    string pngPath = Path.Combine(destDir, pngFileName);

                    using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                    {
                        // Render page to PNG and write to stream
                        pngDevice.Process(pdfDoc.Pages[pageNum], pngStream);
                    }

                    Console.WriteLine($"Saved: {pngPath}");
                }
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
