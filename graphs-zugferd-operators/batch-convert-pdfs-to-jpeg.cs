using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing PDF files
        const string inputDirectory = @"C:\PdfInput";
        // Directory where JPEG images will be saved
        const string outputDirectory = @"C:\JpegOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Define the resolution for the JPEG images (e.g., 300 DPI)
        Resolution resolution = new Resolution(300);
        // Create a JpegDevice with the desired resolution
        JpegDevice jpegDevice = new JpegDevice(resolution);

        foreach (string pdfPath in pdfFiles)
        {
            // Extract the file name without extension for naming output images
            string baseFileName = Path.GetFileNameWithoutExtension(pdfPath);

            // Open the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build the output JPEG file path: e.g., MyDoc_page1.jpeg
                    string jpegPath = Path.Combine(
                        outputDirectory,
                        $"{baseFileName}_page{pageNumber}.jpeg");

                    // Convert the current page to JPEG and write to the file stream
                    using (FileStream jpegStream = new FileStream(jpegPath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                    }
                }
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}