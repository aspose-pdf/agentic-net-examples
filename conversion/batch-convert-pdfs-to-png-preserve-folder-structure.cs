using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Root folder containing PDFs to convert
        const string sourceRoot = @"C:\SourcePdfs";
        // Destination root where PNG images will be placed, mirroring the source hierarchy
        const string destRoot = @"C:\ConvertedPngs";

        if (!Directory.Exists(sourceRoot))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceRoot}");
            return;
        }

        // Ensure destination root exists
        Directory.CreateDirectory(destRoot);

        // Recursively process all *.pdf files
        foreach (string pdfPath in Directory.EnumerateFiles(sourceRoot, "*.pdf", SearchOption.AllDirectories))
        {
            // Compute relative path from source root and create matching output directory
            string relativePath = Path.GetRelativePath(sourceRoot, pdfPath);
            string relativeDir = Path.GetDirectoryName(relativePath) ?? string.Empty;
            string outputDir = Path.Combine(destRoot, relativeDir);
            Directory.CreateDirectory(outputDir);

            // Load PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Set desired resolution for PNG output (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);
                PngDevice pngDevice = new PngDevice(resolution);

                // Iterate pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    Page page = pdfDocument.Pages[pageNumber];

                    // Build PNG file name: original PDF name + page number
                    string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
                    string pngFileName = $"{pdfFileName}_page{pageNumber}.png";
                    string pngPath = Path.Combine(outputDir, pngFileName);

                    // Write PNG to file stream
                    using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
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