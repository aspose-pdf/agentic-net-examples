using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "OutputImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // JpegDevice does NOT implement IDisposable, so instantiate it directly
                JpegDevice jpegDevice = new JpegDevice();

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                foreach (Page page in pdfDoc.Pages)
                {
                    string outputPath = Path.Combine(outputFolder, $"Page_{page.Number}.jpg");

                    // Save the page as JPEG using the device
                    using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                    {
                        jpegDevice.Process(page, imageStream);
                    }

                    Console.WriteLine($"Saved page {page.Number} as JPEG: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
