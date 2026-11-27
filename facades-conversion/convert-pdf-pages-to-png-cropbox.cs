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

        // Folder where PNG images will be saved
        const string outputFolder = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (Aspose.Pdf.Document implements IDisposable)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Total number of pages (Aspose.Pdf uses 1‑based indexing)
            int pageCount = pdfDocument.Pages.Count;

            // Define the resolution for the PNG output (you can adjust as needed)
            var resolution = new Resolution(150);

            // Loop through each page, render it to PNG while respecting the CropBox
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Get the current page – the CropBox defined in the PDF will be honoured by the device
                var page = pdfDocument.Pages[pageNumber];

                // Create a PNG device that will render the page
                var pngDevice = new PngDevice(resolution);

                // Build the output file name
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                // Render the page to a PNG file
                using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                {
                    pngDevice.Process(page, imageStream);
                }
            }
        }

        Console.WriteLine($"All pages have been converted to PNG in folder: {outputFolder}");
    }
}
