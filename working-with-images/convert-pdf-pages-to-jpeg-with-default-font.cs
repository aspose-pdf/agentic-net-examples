using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "Images";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Create a JPEG device with desired resolution
                JpegDevice jpegDevice = new JpegDevice(new Resolution(300));

                // Set the default font name for missing fonts
                jpegDevice.RenderingOptions.DefaultFontName = "Arial";

                // Define output file name for the current page
                string outputPath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");

                // Convert the page to an image and save it
                jpegDevice.Process(doc.Pages[pageNumber], outputPath);
            }
        }

        Console.WriteLine($"Images saved to '{outputDir}'.");
    }
}