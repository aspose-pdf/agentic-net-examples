using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PagesAsJpeg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Instantiate JpegDevice with the required DPI (Resolution) via its constructor.
            // The second argument is the JPEG quality (0‑100). 90 is a good default.
            var jpegDevice = new JpegDevice(new Resolution(150), 90);

            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Prepare output file path for the current page
                string outputPath = Path.Combine(outputFolder, $"Page_{pageNumber}.jpg");

                // Create a file stream for the JPEG output
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the specific page to JPEG and write to the stream
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                }

                Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
            }
        }

        Console.WriteLine("PDF to JPEG conversion completed.");
    }
}
