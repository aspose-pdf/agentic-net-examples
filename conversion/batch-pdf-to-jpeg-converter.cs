using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class BatchPdfToJpegConverter
{
    static void Main()
    {
        // Directory containing PDF files to convert
        string inputDirectory = @"C:\PdfInput";
        // Directory where JPEG images will be saved
        string outputDirectory = @"C:\JpegOutput";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Verify that the input directory exists; if not, create it and exit gracefully
        if (!Directory.Exists(inputDirectory))
        {
            Console.WriteLine($"Input directory '{inputDirectory}' does not exist. Creating it now.");
            Directory.CreateDirectory(inputDirectory);
            Console.WriteLine("Please copy PDF files into the input directory and run the program again.");
            return; // Stop execution – there is nothing to process yet
        }

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

        if (pdfFiles.Length == 0)
        {
            Console.WriteLine($"No PDF files found in '{inputDirectory}'." );
            return;
        }

        // Resolution for the output JPEG images (300 DPI gives good quality)
        Resolution resolution = new Resolution(300);
        // Initialize the JPEG device with the desired resolution
        JpegDevice jpegDevice = new JpegDevice(resolution);

        foreach (string pdfPath in pdfFiles)
        {
            // Derive a base name from the PDF file (without extension)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);

            // Open the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build a custom file name: <BaseName>_page<Number>.jpeg
                    string jpegFileName = $"{baseName}_page{pageNumber}.jpeg";
                    string jpegFullPath = Path.Combine(outputDirectory, jpegFileName);

                    // Convert the current page to JPEG and write it to a file stream
                    using (FileStream jpegStream = new FileStream(jpegFullPath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                    }

                    Console.WriteLine($"Saved: {jpegFullPath}");
                }
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
