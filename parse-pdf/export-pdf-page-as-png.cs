using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputImg = "page1.png";
        const int dpiX = 300; // horizontal resolution
        const int dpiY = 300; // vertical resolution

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Aspose.Pdf uses 1‑based page indexing
                if (doc.Pages.Count < 1)
                {
                    Console.Error.WriteLine("The PDF contains no pages.");
                    return;
                }

                // Define the desired raster resolution
                var resolution = new Resolution(dpiX, dpiY);

                // PngDevice does NOT implement IDisposable – instantiate directly
                var pngDevice = new PngDevice(resolution) { TransparentBackground = true };

                // Render the first page as a single raster image using a FileStream (which is disposed)
                using (FileStream outStream = new FileStream(outputImg, FileMode.Create, FileAccess.Write))
                {
                    pngDevice.Process(doc.Pages[1], outStream);
                }
            }

            Console.WriteLine($"Page rasterized to '{outputImg}' at {dpiX}×{dpiY} DPI.");
        }
        // Image conversion relies on GDI+; handle the Windows‑only limitation
        catch (TypeInitializationException)
        {
            Console.WriteLine("Raster conversion requires GDI+ and is only supported on Windows platforms.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
