using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Create a resolution object (default 300 DPI)
            Resolution resolution = new Resolution(300);

            // Initialize the JPEG device with the resolution
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputFile = $"image{pageNumber}.jpg";
                using (FileStream jpegStream = new FileStream(outputFile, FileMode.Create))
                {
                    // Convert the current page to JPEG and write to the stream
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegStream);
                }
                Console.WriteLine($"Page {pageNumber} saved as {outputFile}");
            }
        }
    }
}