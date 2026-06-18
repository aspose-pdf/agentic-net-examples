using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Folder where JPEG images will be saved
        const string outputFolder = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Set resolution to 150 DPI (suitable for web use)
            Resolution resolution = new Resolution(150);

            // JpegDevice constructor with resolution and quality (0‑100).
            // Quality 90 provides good visual quality while keeping file size reasonable.
            // Note: Aspose.Pdf.JpegDevice does not expose a direct Progressive flag;
            // progressive JPEG encoding is handled internally when possible.
            JpegDevice jpegDevice = new JpegDevice(resolution, 90);

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build output file name for each page
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                // Write each page to a JPEG file using a FileStream
                using (FileStream jpegStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the specific page to JPEG and save it to the stream
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to JPEG images.");
    }
}