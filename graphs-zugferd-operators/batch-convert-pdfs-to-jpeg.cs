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

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        // Define image resolution (e.g., 300 DPI)
        Resolution resolution = new Resolution(300);
        // Create a JpegDevice with the desired resolution
        JpegDevice jpegDevice = new JpegDevice(resolution);

        foreach (string pdfPath in pdfFiles)
        {
            // Open the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                string baseFileName = Path.GetFileNameWithoutExtension(pdfPath);

                // Iterate through all pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build output JPEG file name: <source>_page<page>.jpeg
                    string jpegPath = Path.Combine(
                        outputDirectory,
                        $"{baseFileName}_page{pageNumber}.jpeg");

                    // Convert the page to JPEG and write to file
                    using (FileStream jpegStream = new FileStream(jpegPath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                    }

                    Console.WriteLine($"Saved: {jpegPath}");
                }
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}