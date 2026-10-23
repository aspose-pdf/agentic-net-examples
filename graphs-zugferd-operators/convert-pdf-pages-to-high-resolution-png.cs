using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPng = "page1.png";
        const int pageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Verify requested page exists (pages are 1‑based)
                if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                {
                    Console.Error.WriteLine($"Page {pageNumber} out of range (1‑{pdfDoc.Pages.Count}).");
                    return;
                }

                // Set 300 DPI resolution
                Resolution resolution = new Resolution(300);
                // Create PNG device with the desired resolution
                PngDevice pngDevice = new PngDevice(resolution);

                // Convert the specified page to PNG
                using (FileStream outStream = new FileStream(outputPng, FileMode.Create))
                {
                    pngDevice.Process(pdfDoc.Pages[pageNumber], outStream);
                }
            }

            Console.WriteLine($"Page {pageNumber} saved as PNG at '{outputPng}' (300 DPI).");
        }
        catch (TypeInitializationException)
        {
            // Image conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("Image conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}