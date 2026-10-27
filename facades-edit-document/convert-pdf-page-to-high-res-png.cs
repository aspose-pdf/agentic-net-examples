using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pngPath = "page7.png";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Verify that the document has at least 7 pages
            if (pdfDoc.Pages.Count < 7)
            {
                Console.Error.WriteLine("The PDF does not contain a page 7.");
                return;
            }

            // Get the seventh page (Aspose.Pdf pages are 1‑based)
            var page = pdfDoc.Pages[7];

            // Configure the PNG device with a 300 DPI resolution
            var pngDevice = new PngDevice(new Resolution(300));

            // Render the page to a PNG file
            using (FileStream imageStream = new FileStream(pngPath, FileMode.Create))
            {
                pngDevice.Process(page, imageStream);
            }

            Console.WriteLine($"Page 7 successfully saved as high‑resolution PNG: {pngPath}");
        }
    }
}
