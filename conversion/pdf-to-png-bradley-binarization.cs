using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputFolder  = "output_images";
        const double bradleyThreshold = 0.5; // value between 0.0 and 1.0

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a PNG device with desired resolution
            Resolution resolution = new Resolution(300);
            PngDevice pngDevice = new PngDevice(resolution);

            // TiffDevice provides the BinarizeBradley method
            TiffDevice binarizer = new TiffDevice();

            // Iterate pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Render page to a PNG in memory
                using (MemoryStream pngStream = new MemoryStream())
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                    pngStream.Position = 0; // reset for reading

                    // Prepare output file path
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}_bradley.png");

                    // Apply Bradley binarization and write result to file
                    using (FileStream outputFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        binarizer.BinarizeBradley(pngStream, outputFile, bradleyThreshold);
                    }

                    Console.WriteLine($"Page {pageNumber} processed: {outputPath}");
                }
            }
        }
    }
}