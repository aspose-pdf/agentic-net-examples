using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPng = "page1.png";
        const int pageNumber = 1;   // 1‑based index
        const int dpi = 300;        // desired resolution

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Validate page number (Aspose.Pdf uses 1‑based indexing)
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine("Invalid page number.");
                    return;
                }

                // Create a concrete PNG device – devices are NOT IDisposable, so no using block
                var pngDevice = new PngDevice(new Resolution(dpi))
                {
                    TransparentBackground = true // optional, keeps background transparent
                };

                // Convert the specified page to PNG
                pngDevice.Process(pdfDoc.Pages[pageNumber], outputPng);
            }

            Console.WriteLine($"Page {pageNumber} saved as PNG at {dpi} DPI to '{outputPng}'.");
        }
        catch (TypeInitializationException)
        {
            // PNG conversion via PngDevice requires GDI+ (Windows only)
            Console.WriteLine("PNG conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
