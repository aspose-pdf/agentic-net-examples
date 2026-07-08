using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing PDF files
        string inputDir = @"C:\PdfInput";
        // Directory where JPEG images will be saved
        string outputDir = @"C:\JpegOutput";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Enumerate all PDF files in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            // Derive base name without extension for naming output images
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);

            // Open PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Set desired resolution (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);
                // Initialize JPEG device with the resolution
                JpegDevice jpegDevice = new JpegDevice(resolution);

                // Iterate through pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build output file name: <BaseName>_page<Number>.jpeg
                    string jpegPath = Path.Combine(
                        outputDir,
                        $"{baseName}_page{pageNumber}.jpeg");

                    // Convert the page to JPEG and write to file
                    using (FileStream jpegStream = new FileStream(jpegPath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                    }
                }
            }

            Console.WriteLine($"Converted '{pdfPath}' to JPEG images.");
        }
    }
}