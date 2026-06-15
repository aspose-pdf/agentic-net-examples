using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Root folder containing the source PDF files
        const string sourceRoot = "InputPdfs";

        // Root folder where the PNG images will be saved, preserving hierarchy
        const string outputRoot = "OutputImages";

        if (!Directory.Exists(sourceRoot))
        {
            Console.Error.WriteLine($"Source folder not found: {sourceRoot}");
            return;
        }

        // Find all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(sourceRoot, "*.pdf", SearchOption.AllDirectories);

        // Define the resolution for the PNG images (e.g., 300 DPI)
        Resolution resolution = new Resolution(300);
        PngDevice pngDevice = new PngDevice(resolution);

        foreach (string pdfPath in pdfFiles)
        {
            // Compute the relative path to recreate the folder structure in the output folder
            string relativePath = Path.GetRelativePath(sourceRoot, pdfPath);
            string relativeDir = Path.GetDirectoryName(relativePath);
            string outputDir = Path.Combine(outputRoot, relativeDir ?? string.Empty);

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDir);

            // Base file name without extension (used for naming PNG files)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);

            try
            {
                // Load the PDF document (wrapped in using for deterministic disposal)
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Iterate pages (1‑based indexing)
                    for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                    {
                        string pngPath = Path.Combine(outputDir, $"{baseName}_page{pageNum}.png");

                        // Convert the page to PNG and write to file
                        using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                        {
                            pngDevice.Process(pdfDoc.Pages[pageNum], pngStream);
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
    }
}