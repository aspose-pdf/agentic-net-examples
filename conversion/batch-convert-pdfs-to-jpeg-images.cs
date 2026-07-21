using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        const string inputDirectory = @"C:\PdfInput";

        // Directory where JPEG images will be saved
        const string outputDirectory = @"C:\JpegOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            // Use a using block to guarantee disposal of the Document object
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Define the resolution for the output images (e.g., 300 DPI)
                Resolution resolution = new Resolution(300);

                // Initialize the JpegDevice with the desired resolution
                JpegDevice jpegDevice = new JpegDevice(resolution);

                // Iterate through all pages (1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build a custom file name: <pdfname>_page<pageNumber>.jpeg
                    string outputFileName = $"{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageNumber}.jpeg";
                    string outputPath = Path.Combine(outputDirectory, outputFileName);

                    // Convert the current page to JPEG and write to a file stream
                    using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                    }

                    Console.WriteLine($"Saved: {outputPath}");
                }
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}