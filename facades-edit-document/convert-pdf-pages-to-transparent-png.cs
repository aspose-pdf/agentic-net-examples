using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // PngDevice resides here

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string inputPdf = "input.pdf";
        // Directory where PNG images will be saved
        const string outputDir = "output_images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);
        int pageCount = pdfDocument.Pages.Count;

        // Configure the PNG device – 150 DPI and transparent background
        var resolution = new Resolution(150);
        var pngDevice = new PngDevice(resolution)
        {
            TransparentBackground = true
        };

        // Convert each page to a separate PNG file
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
        {
            string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
            // Create (or overwrite) the file stream for the PNG image
            using (FileStream imageStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Render the current page into the stream
                pngDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
            }
            Console.WriteLine($"Page {pageNumber} saved as {outputPath}");
        }
    }
}
