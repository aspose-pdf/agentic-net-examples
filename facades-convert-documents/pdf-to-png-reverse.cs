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
            // Set desired image resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            // Create a PNG device with the specified resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Process pages in reverse order (last page to first page)
            for (int pageNumber = pdfDocument.Pages.Count; pageNumber >= 1; pageNumber--)
            {
                string outputImage = $"image{pageNumber}.png";
                using (FileStream pngStream = new FileStream(outputImage, FileMode.Create))
                {
                    pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                }
                Console.WriteLine($"Saved page {pageNumber} as {outputImage}");
            }
        }
    }
}