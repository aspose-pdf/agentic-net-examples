using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing source PDF files
        string inputDir = "InputPdfs";
        // Directory where JPEG images will be saved
        string outputDir = "OutputJpegs";

        // Desired image resolution (dots per inch)
        int dpi = 300;

        // Validate input directory
        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Retrieve all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found in the input directory.");
            return;
        }

        // Create a JpegDevice with the desired resolution
        Resolution resolution = new Resolution(dpi);
        JpegDevice jpegDevice = new JpegDevice(resolution);

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            // Base name without extension – used for custom naming pattern
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);

            // Load PDF document inside a using block (ensures disposal)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    // Custom naming: <BaseName>_page###.jpeg (zero‑padded page number)
                    string jpegPath = Path.Combine(outputDir,
                        $"{baseName}_page{pageNum:D3}.jpeg");

                    // Save each page as a JPEG image
                    using (FileStream jpegStream = new FileStream(jpegPath, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDoc.Pages[pageNum], jpegStream);
                    }
                }
            }

            Console.WriteLine($"Converted PDF: {pdfPath}");
        }

        Console.WriteLine("Batch conversion to JPEG completed.");
    }
}