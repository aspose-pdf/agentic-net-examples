using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // JpegDevice resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "PageImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    Page page = pdfDoc.Pages[pageNumber];

                    // Create a JPEG rasterizer with 720 DPI resolution and maximum quality
                    JpegDevice jpegDevice = new JpegDevice(720, 100);

                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.jpg");

                    // Write the image to a file stream
                    using (FileStream imageStream = new FileStream(outputPath, FileMode.Create))
                    {
                        // Process the page and generate the JPEG image
                        jpegDevice.Process(page, imageStream);
                    }

                    Console.WriteLine($"Saved page {pageNumber} as JPEG to '{outputPath}'.");
                }
            }
        }
        catch (TypeInitializationException)
        {
            // JPEG rasterization relies on GDI+ and may fail on non‑Windows platforms
            Console.Error.WriteLine("JPEG conversion requires GDI+ and is only supported on Windows.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}