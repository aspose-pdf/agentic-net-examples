using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ConvertedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (FormPresentationMode is not available in this version of Aspose.Pdf)
        using (Document doc = new Document(inputPdfPath))
        {
            // Convert pages 2 through 4 to JPEG images while preserving form field appearance.
            // JpegDevice renders the page exactly as it appears on screen, including form fields.
            var resolution = new Resolution(150); // DPI – adjust as needed
            var jpegDevice = new JpegDevice(resolution);

            for (int pageNumber = 2; pageNumber <= 4; pageNumber++)
            {
                // Build the output file path
                string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                // Render the specific page to JPEG
                jpegDevice.Process(doc.Pages[pageNumber], outputPath);
                Console.WriteLine($"Page {pageNumber} saved as JPEG: {outputPath}");
            }
        }
    }
}
