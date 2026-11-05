using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputImagePath = "page1.jpg";
        const int pageNumber = 1; // 1‑based index of the page to convert

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Ensure the requested page exists (pages are 1‑based)
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // JpegDevice does NOT implement IDisposable; instantiate directly.
            // 300 DPI is the default resolution used by Aspose when no explicit DPI is required.
            JpegDevice jpegDevice = new JpegDevice(300);

            // Render the selected page to the JPEG file.
            jpegDevice.Process(pdfDoc.Pages[pageNumber], outputImagePath);
        }

        Console.WriteLine($"Page {pageNumber} saved as JPEG to '{outputImagePath}'.");
    }
}
