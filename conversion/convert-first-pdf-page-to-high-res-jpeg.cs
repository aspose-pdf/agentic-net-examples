using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "page1.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Verify that the PDF has at least one page
                if (pdfDoc.Pages.Count < 1)
                {
                    Console.Error.WriteLine("PDF contains no pages.");
                    return;
                }

                // Set resolution to 300 DPI
                Resolution resolution = new Resolution(300);

                // JpegDevice converts pages to JPEG images. It does NOT implement IDisposable, so do NOT use a using statement.
                JpegDevice jpegDevice = new JpegDevice(resolution);

                // Convert the first page (1‑based index) to a high‑resolution JPEG
                jpegDevice.Process(pdfDoc.Pages[1], outputPath);
            }

            Console.WriteLine($"First page saved as JPEG to '{outputPath}'.");
        }
        catch (TypeInitializationException)
        {
            // Image conversion relies on GDI+, which is Windows‑only
            Console.WriteLine("Image conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
