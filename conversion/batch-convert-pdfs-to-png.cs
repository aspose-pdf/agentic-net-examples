using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class BatchPdfToPngConverter
{
    static void Main()
    {
        // Input root folder containing PDFs (including subfolders)
        const string inputRoot = @"C:\InputPdfs";
        // Output root folder where PNGs will be saved, preserving hierarchy
        const string outputRoot = @"C:\OutputPngs";

        if (!Directory.Exists(inputRoot))
        {
            Console.Error.WriteLine($"Input folder not found: {inputRoot}");
            return;
        }

        // Find all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(inputRoot, "*.pdf", SearchOption.AllDirectories);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Set desired image resolution (e.g., 300 DPI)
        Resolution resolution = new Resolution(300);
        // PngDevice does NOT implement IDisposable, so instantiate without using
        PngDevice pngDevice = new PngDevice(resolution);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Compute relative path to maintain folder hierarchy
                string relativePath = Path.GetRelativePath(inputRoot, pdfPath);
                string relativeDir = Path.GetDirectoryName(relativePath) ?? string.Empty;
                string outputDir = Path.Combine(outputRoot, relativeDir);

                // Ensure the output directory exists
                Directory.CreateDirectory(outputDir);

                // Load PDF document
                using (Document pdfDocument = new Document(pdfPath))
                {
                    // Iterate through pages (1‑based indexing)
                    for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
                    {
                        // Build PNG file name: original name + page number
                        string pngFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageNum}.png";
                        string pngPath = Path.Combine(outputDir, pngFileName);

                        // Save page as PNG
                        using (FileStream pngStream = new FileStream(pngPath, FileMode.Create, FileAccess.Write))
                        {
                            pngDevice.Process(pdfDocument.Pages[pageNum], pngStream);
                        }
                    }
                }

                Console.WriteLine($"Converted: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
