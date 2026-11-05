using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class BatchPdfToPng
{
    static void Main()
    {
        // Root folders – adjust as needed
        const string inputRoot  = @"InputPdfs";
        const string outputRoot = @"OutputImages";

        if (!Directory.Exists(inputRoot))
        {
            Console.Error.WriteLine($"Input folder not found: {inputRoot}");
            return;
        }

        // Find all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(inputRoot, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfPath in pdfFiles)
        {
            // Compute relative path to preserve folder hierarchy
            string relativePath = Path.GetRelativePath(inputRoot, pdfPath);
            string relativeDir  = Path.GetDirectoryName(relativePath) ?? string.Empty;

            // Destination directory for this PDF's images
            string destDir = Path.Combine(outputRoot, relativeDir);
            Directory.CreateDirectory(destDir);

            // Open PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Define a resolution for the PNG output (you can adjust DPI as needed)
                var resolution = new Resolution(300);
                var pngDevice = new PngDevice(resolution);

                // Iterate pages using 1‑based indexing (Aspose.Pdf rule)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Build PNG file name: originalname_page_1.png, etc.
                    string pngFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page_{pageNum}.png";
                    string pngPath      = Path.Combine(destDir, pngFileName);

                    // Render the selected page to PNG using PngDevice
                    pngDevice.Process(doc.Pages[pageNum], pngPath);
                }
            }

            Console.WriteLine($"Converted: {pdfPath}");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
