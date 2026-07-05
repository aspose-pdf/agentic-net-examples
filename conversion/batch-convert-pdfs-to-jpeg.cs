using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class BatchPdfToJpegConverter
{
    static void Main()
    {
        // Directory containing the source PDF files
        const string inputDirectory = @"C:\PdfInput";
        // Directory where JPEG images will be saved
        const string outputDirectory = @"C:\JpegOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

        // Define the resolution for the JPEG images (300 DPI)
        Resolution resolution = new Resolution(300);
        // Create a JpegDevice instance with the desired resolution
        JpegDevice jpegDevice = new JpegDevice(resolution);

        foreach (string pdfPath in pdfFiles)
        {
            // Open each PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Base name without extension for naming the output images
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);

                // Iterate over pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build the output file name using a custom pattern
                    string jpegPath = Path.Combine(
                        outputDirectory,
                        $"{baseName}_page{pageNumber}_out.jpeg");

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