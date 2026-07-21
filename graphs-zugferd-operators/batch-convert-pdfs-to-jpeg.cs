using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing PDF files
        string inputDirectory = @"C:\PdfInput";
        // Directory where JPEG images will be saved
        string outputDirectory = @"C:\JpegOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            // Extract the file name without extension for naming output images
            string baseFileName = Path.GetFileNameWithoutExtension(pdfPath);

            // Open the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Set desired resolution (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);
                // Initialize the JPEG device with the resolution
                JpegDevice jpegDevice = new JpegDevice(resolution);

                // Iterate through all pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build the output JPEG file path
                    string jpegPath = Path.Combine(
                        outputDirectory,
                        $"{baseFileName}_page{pageNumber}.jpeg");

                    // Convert the current page to JPEG and write to file
                    using (FileStream jpegStream = new FileStream(jpegPath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                    }
                }
            }

            Console.WriteLine($"Converted '{pdfPath}' to JPEG images.");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}